using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;
using StockExchange.Core.Models;

namespace StockExchange.Core.Services;

public class ApiResponseBuilder<T> : IApiResponseBuilder<T> where T : class
{
    public ObjectResult GetOkObjectResult(T payload)
    {
        return new OkObjectResult(new ApiResponse<T>(
                    payload,
                    new Links(""))); // add links to support paging
    }

    public ObjectResult GetErrorObjectResult(string errorMessage, string errorCode)
    {
        return new ObjectResult(new ApiErrorResponse(
                Status500InternalServerError.ToString(), errorCode, errorMessage))
        {
            StatusCode = Status500InternalServerError
        };
    }
}
