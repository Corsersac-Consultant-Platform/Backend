using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Security.Interfaces;

namespace Application.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;


    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ITokenService tokenService, IUserService userService, IRoleService roleService)
    {
        //attrubute allow anonymus
        var allowAnonymous = await IsAllowAnonymousAsync(context);

        if (allowAnonymous)
        {
            await _next(context);
            return;
        }

        //If token exists in cookies
        if (!context.Request.Cookies.TryGetValue("Token", out var token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is missing");
            return;
        }

        //validate token
        var userId = await tokenService.ValidateToken(token);

        if (userId is null or 0)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Token is corrupted");
            return;
        }

        var user = await userService.GetByIdAsync(userId.Value);
        if (user == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("User does not exist");
            return;
        }
        var role = await roleService.GetByIdAsync(user.RoleId);
        context.Items["User"] = user;
        context.Items["Role"] = role?.Type;
        await _next(context);
    }

    private Task<bool> IsAllowAnonymousAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null) return Task.FromResult(false);

        var allowAnonymous = endpoint.Metadata.GetMetadata<IAllowAnonymous>() != null;

        if (!allowAnonymous)
        {
            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor != null)
                allowAnonymous = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true)
                    .Any(attr => attr.GetType() == typeof(AllowAnonymousAttribute));
        }

        return Task.FromResult(allowAnonymous);
    }
}