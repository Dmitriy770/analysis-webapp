namespace Uis.Common.ExceptionFilter;

public record ErrorResponse(
    int StatusCode,
    int ErrorCode,
    string ErrorMessage);