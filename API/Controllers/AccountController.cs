﻿using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")] //account/register   
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
        if(await UserExists(registerDto.Username)) 
            return BadRequest("User Name Is Taken");
        using var hmac = new HMACSHA512();
        var appUser = new AppUser{
            UserName = registerDto.Username.ToLower(),
            PaswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(appUser);
        await context.SaveChangesAsync();
        return new UserDto{
            Username = appUser.UserName,
            Token = tokenService.CreateToken(appUser)
        };
    }

    private async Task<bool> UserExists(string username){
        return await context.Users.AnyAsync(x =>x.UserName.ToLower() == username.ToLower());
    }
    [HttpPost("login")] 
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if(user == null) return Unauthorized("Invalid User");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for(var i = 0; i < computedHash.Length; i ++){
            if(computedHash[i] != user.PaswordHash[i]) return Unauthorized("Invalid Password");
        }
        return new UserDto{
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }
}
