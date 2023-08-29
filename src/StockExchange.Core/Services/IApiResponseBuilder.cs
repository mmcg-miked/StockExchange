using Microsoft.AspNetCore.Mvc;

namespace StockExchange.Core.Services;

public interface IApiResponseBuilder<T> where T : class
{
    ObjectResult GetOkObjectResult(T payload);

    ObjectResult GetErrorObjectResult(string errorMessage, string errorCode);
}
