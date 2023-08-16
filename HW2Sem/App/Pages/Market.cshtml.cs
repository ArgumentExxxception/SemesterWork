using Core1.Interfaces;
using HW2Sem.Exceptions;
using HW2Sem.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class Market : PageModel
{
    public string Message; 
    private readonly IAuthenticationService _authentication;
    private readonly ICoinHistoryService _coinHistoryService;
    private readonly IBuyingСoinService _buyingСoinService;
    public IReadOnlyList<CoinsDailyInfo>? PriceList { get; set; }
    public GetHistoryResponse? HistoryResponse { get; set; }
    public List<CoinOHLC> CoinOhlc { get; set; }
    public Market(ICoinHistoryService coinHistoryService, IAuthenticationService authenticationService, IBuyingСoinService buyingСoinService)
    {
        _coinHistoryService = coinHistoryService;
        _authentication = authenticationService;
        _buyingСoinService = buyingСoinService;
    }

    public async Task OnGetAsync(string coinName, CancellationToken token)
    {
        var prices = await _coinHistoryService.GetByIdCoinHistory(coinName,token);
        var dailyPrices = await _coinHistoryService.GetCoinsDailyInfoById(coinName, token);
        var ohlcPrices = await _coinHistoryService.GetOhlcInfoById(coinName, token);
        HistoryResponse = prices;
        PriceList = dailyPrices;
        CoinOhlc = ohlcPrices;
    }

    public async Task OnPostAsync(string coinName,double amount, CancellationToken token)
    {
        var prices = await _coinHistoryService.GetByIdCoinHistory(coinName,token);
        var dailyPrices = await _coinHistoryService.GetCoinsDailyInfoById(coinName, token);
        var ohlcPrices = await _coinHistoryService.GetOhlcInfoById(coinName, token);
        HistoryResponse = prices;
        PriceList = dailyPrices;
        CoinOhlc = ohlcPrices;
        if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            try
            {
                var userId = await _authentication.GetUserId(HttpContext.User);
                double currentPrice = PriceList!.Select(p => p.CurrentPrice).FirstOrDefault();
                await _buyingСoinService.BuyCoin(int.Parse(userId),amount, currentPrice, coinName);
                Message = $"Вы успешно купили {coinName} за {currentPrice}";
                return;
            }
            catch (BadRequest ex)
            {
                Message = $"Ошибка! {ex.Info}";
                return;
            }
        }
        Message = "Авторизуйтесь или зарегистрируйтесь";

    }
}