using System.Net.Mime;
using AutoMapper;
using Domain.Interfaces;
using Interface.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Support.Models;

namespace Application.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequest)
    {
        var user = mapper.Map<UserRequestDTO, User>(userRequest);
        var result = await authService.SignUp(user.Username, user.Password);
        return Ok(result);
    }
    
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] UserRequestDTO userRequest)
    {
        var user = mapper.Map<UserRequestDTO, User>(userRequest);
        var token = await authService.SignIn(user.Username, user.Password);
        Response.Cookies.Append("Token", token , 
            new CookieOptions
            {
                HttpOnly = true, 
                SameSite = SameSiteMode.Strict, 
                Secure = true
            });
        return Ok(token);
    }
}