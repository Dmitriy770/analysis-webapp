﻿using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Uis.Common.ExceptionFilter;

public abstract class BaseExceptionFilter : IExceptionFilter
{
    protected abstract ErrorResponse? HandleException(Exception exception);
    
    public void OnException(ExceptionContext context)
    {
        if (HandleException(context.Exception) is not { } response)
        {
            response = new ErrorResponse(
                StatusCode: (int)HttpStatusCode.InternalServerError,
                ErrorCode: 0,
                ErrorMessage: context.Exception.Message,
                StackTrace: context.Exception.StackTrace);
        }

        context.Result = new ContentResult
        {
            StatusCode = response.StatusCode,
            Content = JsonSerializer.Serialize(response),
            ContentType = "application/json"
        };
        context.ExceptionHandled = true;
    }
}