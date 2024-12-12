﻿using System.Net;
using Uis.Common.ExceptionFilter;
using Uis.Domain.Exceptions;

namespace Uis.Api.Filters.Internal;

public sealed class UserControllerExceptionFilter : BaseExceptionFilter
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        return exception switch
        {
            UserNotFoundException => new ErrorResponse(
                StatusCode: (int)HttpStatusCode.NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message),
            
            SessionNotFoundException => new ErrorResponse(
                StatusCode: (int)HttpStatusCode.NotFound,
                ErrorCode: 0,
                ErrorMessage: exception.Message),
            
            _ => null
        };
    }
}