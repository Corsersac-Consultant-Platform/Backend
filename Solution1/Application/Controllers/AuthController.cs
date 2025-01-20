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
                SameSite = SameSiteMode.Strict, 
                Secure = true
            });
        return Ok(token);
    }
    
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken()
    {
        var token = Request.Cookies["Token"];
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is missing");
        }
        var newToken = await authService.RefreshToken(token);
        Response.Cookies.Append("Token", newToken , 
            new CookieOptions
            {
                SameSite = SameSiteMode.Strict, 
                Secure = true
            });
        return Ok(newToken);
    }
    
    [HttpPost("sign-out")]
    [AllowAnonymous]
    public async Task<IActionResult> SignOut()
    {
        Response.Cookies.Delete("Token");
        return Ok("Signed out successfully");
    }
}