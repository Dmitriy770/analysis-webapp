﻿using System.Net;
using System.Text.Json;
using Common.Web.Authorization.Exceptions;
using Common.Web.Authorization.Extensions;
using Common.Web.ExceptionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Uis.Application.Commands;
using Uis.Domain.Exceptions;

namespace Uis.Api.Filters.Public;

public sealed class AuthorizationFilter(
    ISender sender,
    ILogger<AuthorizationFilter> logger)
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
            logger.LogInformation("Start work filter");
            foreach (var (key, value) in context.HttpContext.Request.Cookies)
            {
                logger.LogInformation($"cookie: {key}: {value}");
            }
            var sessionId = context.GetSessionId();
            var newSession = await sender.Send(new ValidateSession(sessionId));
            context.AddSessionToHeader(newSession.SessionId);
            context.AddSessionCookie(sessionId);
        }
        catch (Exception exception) when(exception is SessionExpiredException or SessionNotFoundException or SessionExpiredException or SessionIdNotFoundException)
        {
            var response = new ErrorResponse(
                StatusCode: (int)HttpStatusCode.Unauthorized,
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
}