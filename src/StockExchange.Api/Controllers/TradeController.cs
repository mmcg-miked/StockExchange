using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;
using StockExchange.Api.Interfaces;
using StockExchange.Core.Filters;
using StockExchange.Core.Models;
using StockExchange.Infrastructure.Models;

namespace StockExchange.Api.Controllers;

[Route("api/v1/trades")]
[ApiController]
public class TradeController : ControllerBase
{
    private readonly ILogger<TradeController> _logger;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ITradeApiService _tradeApiService;

    public TradeController(
        ILogger<TradeController> logger,
        IHttpContextAccessor contextAccessor,
        ITradeApiService tradeApiService)
    {
        _logger = logger;
        _contextAccessor = contextAccessor;
        _tradeApiService = tradeApiService;
    }

    [HttpPost]
    //[Authorize("Api.Reader")]
    [ValidateModel]
    [ProducesResponseType(typeof(ApiResponse<ICollection<string>>), Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), Status500InternalServerError)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ICollection<string>>>> Post([FromBody] TradeTransaction trade)
    {
        _logger.LogInformation($"Received trade notification request: {trade.Symbol} {trade.Shares}@{trade.Price}. BrokerId: {trade.BrokerId}");

        return await _tradeApiService.LoadTradeAsync(trade);
    }
}
