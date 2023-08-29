using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;
using StockExchange.Api.Models;
using StockExchange.Api.Interfaces;
using StockExchange.Core.Models;

namespace StockExchange.Api.Controllers;

[Route("api/v1/stocks")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IStockApiService _stockApiService;

    public StockController(
        ILogger<StockController> logger,
        IHttpContextAccessor contextAccessor,
        IStockApiService stockApiService)
    {
        _logger = logger;
        _contextAccessor = contextAccessor;
        _stockApiService = stockApiService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<ICollection<StockValue>>), Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), Status500InternalServerError)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ICollection<StockValue>>>> Get([FromQuery] List<string> symbols)
    {
        _logger.LogInformation($"Received request to get stock values {string.Join(",", symbols)}");

        return await _stockApiService.GetStocksAsync(symbols);
    }
}
