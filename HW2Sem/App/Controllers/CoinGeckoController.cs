using Core1.Interfaces;
using HW2Sem.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoinGeckoController: ControllerBase
{
    private readonly ICoinGeckoApiClient _geckoApiClient;
    private readonly ICoinHistoryService _coinHistoryService;
    
    public CoinGeckoController(ICoinGeckoApiClient geckoApiClient, ICoinHistoryService coinHistoryService)
    {
        _geckoApiClient = geckoApiClient;
        _coinHistoryService = coinHistoryService;
    }
    
    /// <summary>
    /// Получение списка коинов с ID, именем и символом
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Список коинов с Id,именем и символом</response>
    /// <response code="500">Ошибка сервера</response>
    [HttpGet("/coins")]
    public async Task<ActionResult<HttpContent>> GetAllCoinsPrice()
    {
        var result = await _geckoApiClient.GetAllCoinsPrice();
        if (result == null)
            return NotFound();
            
        return Ok(result);
    }
    
    /// <summary>
    /// Получение изменения цены коина в виде: время(UNIX),цена
    /// </summary>
    /// <returns>
    /// successful operation
    /// "prices": [date,price],
    /// [secondDate,secondPrice]
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="500">Ошибка сервера</response>
    [HttpGet("/coinhistory/{id}")]
    public async Task<ActionResult<GetHistoryResponse>> GetCoinHistory(string id, CancellationToken token)
    {
        var result = await _coinHistoryService.GetByIdCoinHistory(id, token);
        if (result == null)
            return NotFound();
        
        return Ok(result);
    }
    
    /// <summary>
    /// OHLC коинов
    /// </summary>
    /// <returns>
    /// successful operation
    /// [1594382400000(time),
    /// 1.1 (open),
    /// 2.2 (high),
    /// 3.3 (low),
    /// 4.4 (close)]
    /// </returns>
    /// <response code="200"></response>
    /// <response code="500">Ошибка сервера</response>
    [HttpGet("coinOhlc/{id}")]
    public async Task<ActionResult<IReadOnlyList<CoinOHLC>>> GetCoinOhlc(string id, CancellationToken token)
    {
        var result = await _coinHistoryService.GetOhlcInfoById(id, token);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}