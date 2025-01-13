using System.Net.Mime;
using Application.Filters;
using AutoMapper;
using Domain.Interfaces;
using Interface.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Support.Models;

namespace Application.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [CustomAuthorize("ADMIN", "TESTER")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllAsync();
        var usersResponse = mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDTO>>(users);
        return Ok(usersResponse);
    }
    
    [HttpGet("{id}")]
    [CustomAuthorize("ADMIN", "TESTER", "DEFAULT")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        var userResponse = mapper.Map<User, UserResponseDTO>(user);
        return Ok(userResponse);
    }
    
    [HttpPatch("update-password/{id}")]
    [CustomAuthorize("ADMIN", "TESTER", "DEFAULT")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] string password)
    {
        await userService.UpdatePassword(id, password);
        return Ok("Password updated successfully");
    }
    
}