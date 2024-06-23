using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")] // /api/users
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    // [HttpGet]
    // public ActionResult<IEnumerable<AppUser>> GetUsers(){
    //     var users = _context.Users.ToList();
    //     return users;
    // }

    [HttpGet("{id}")] // /api/users/2
    public async Task<ActionResult<AppUser>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }



    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }
}
