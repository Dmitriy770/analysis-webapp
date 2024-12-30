using Common.Web.Authorization.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Common.Web.Authorization.Extensions;

public static class MvcOptionsExtensions
{
    public static void AddAuthorizationFilter(this MvcOptions options)
    {
        options.Filters.AddService<AuthorizationFilter>();
    }
}