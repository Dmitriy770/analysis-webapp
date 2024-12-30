namespace Common.Web.ExceptionFilters;

public record ErrorResponse(
    int StatusCode,
    int ErrorCode,
    string ErrorMessage,
    string? StackTrace);