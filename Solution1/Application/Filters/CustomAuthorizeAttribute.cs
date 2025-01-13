
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Support.Models;

namespace Application.Filters;

public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly string[] _roles;

    public CustomAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"] as User;
        var roleType = context.HttpContext.Items["Role"] as string;
        if (user == null || !_roles.Contains(roleType))
        {
           
            context.Result = new ForbidResult();
        }

    }
}