using Uis.Common.ExceptionFilter;

namespace Uis.Common.Authorization.Filters;

public sealed class AuthorizationExceptionFilter : BaseExceptionFilter
{
    protected override ErrorResponse? HandleException(Exception exception)
    {
        throw new NotImplementedException();
    }
}