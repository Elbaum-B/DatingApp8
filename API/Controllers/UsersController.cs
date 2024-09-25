using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{


    // [HttpGet]
    // public ActionResult<IEnumerable<AppUser>> GetUsers(){
    //     var users = _context.Users.ToList();
    //     return users;
    // }
     
   
    [HttpGet("{username}")] // /api/users/username
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
       
        if(user == null){
            return NotFound(); 
        }
        return user;
    }


    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberemberUpdateDto, IMapper mapper){
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(username == null) return BadRequest("No username found in token");

            var user = await userRepository.GetUserByUserNameAsync(username);
            if(user == null) return BadRequest("Could not fond user");
            mapper.Map(memberemberUpdateDto, user);
            if(await userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update the user") ;
    }
}
