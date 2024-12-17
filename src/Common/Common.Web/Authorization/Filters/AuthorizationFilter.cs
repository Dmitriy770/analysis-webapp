using System.Net;
using System.Text.Json;
using Common.Web.Authorization.Exceptions;
using Common.Web.Authorization.Extensions;
using Common.Web.Authorization.Filters.Api;
using Common.Web.ExceptionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Web.Authorization.Filters;

internal sealed class AuthorizationFilter(
    IUserServiceClient userServiceClient)
    : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (!context.IsAuthorizationRequired())
        {
            return;
        }

        try
        {
            var sessionId = context.GetSessionId();
            var session = await userServiceClient.GetSessionAsync(sessionId);
            Console.WriteLine("user id " + session.UserId);

            context.AddSessionCookie(session.SessionId);
            context.HttpContext.AddSessionId(session.SessionId);
            context.HttpContext.AddUserId(session.UserId);
        }
        catch (Exception exception) when (exception is SessionIdNotFoundException)
        {
            AddErrorResult(context, exception);
        }
        catch (Refit.ApiException exception) when (exception.StatusCode == HttpStatusCode.Unauthorized)
        {
            AddErrorResult(context, exception);
        }
    }

    private void AddErrorResult(AuthorizationFilterContext context, Exception exception)
    {
        var response = new ErrorResponse(
            StatusCode: StatusCodes.Status401Unauthorized,
            ErrorCode: 0,
            ErrorMessage: exception.Message,
            StackTrace: exception.StackTrace);
            
        context.Result = new ContentResult
        {
            StatusCode = response.StatusCode,
            Content = JsonSerializer.Serialize(response),
            ContentType = "application/json"
        };
    }
}