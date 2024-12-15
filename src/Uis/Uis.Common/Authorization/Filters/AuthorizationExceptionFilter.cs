using Microsoft.Extensions.Logging;
using Uis.Common.ExceptionFilter;

namespace Uis.Common.Authorization.Filters;

public sealed class AuthorizationExceptionFilter(
    ILogger<AuthorizationExceptionFilter> logger)
    : BaseExceptionFilter(logger)
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        throw new NotImplementedException();
    }
}