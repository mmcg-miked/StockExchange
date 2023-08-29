namespace StockExchange.Core.Models;

public record ApiErrorResponse(
    string ErrorCode,
    string Id,
    string Message);
