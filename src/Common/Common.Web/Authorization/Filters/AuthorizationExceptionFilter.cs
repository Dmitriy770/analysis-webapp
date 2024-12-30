using Common.Web.ExceptionFilters;
using Microsoft.Extensions.Logging;

namespace Common.Web.Authorization.Filters;

public sealed class AuthorizationExceptionFilter(
    ILogger<AuthorizationExceptionFilter> logger)
    : BaseExceptionFilter(logger)
{
    protected override ErrorResponse? HandleException(System.Exception exception)
    {
        throw new NotImplementedException();
    }
}