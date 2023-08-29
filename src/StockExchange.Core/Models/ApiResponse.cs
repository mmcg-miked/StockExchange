namespace StockExchange.Core.Models;

public record ApiResponse<T>
{
    public ApiResponse(T payload, Links links)
    {
        Data = payload;
        Links = links;
    }

    public T Data { get; }

    public Links Links { get; }
}

public record Links(
    string Self,
    string First = "",
    string Last = "",
    string Prev = "",
    string Next = "");
