using Core1.Interfaces;
using HW2Sem.Exceptions;
using HW2Sem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

[Authorize]
public class CoinsSelling : PageModel
{
    public string Message;
    private readonly ICoinHistoryService _coinHistoryService;
    private readonly IBuyingСoinService _buyingСoinService;
    private readonly IAuthenticationService _authenticationService;
    public IReadOnlyList<CoinsDailyInfo>? PriceList { get; set; }
    public CoinsSelling(IBuyingСoinService buyingСoinService, IAuthenticationService authenticationService, ICoinHistoryService coinHistoryService)
    {
        _buyingСoinService = buyingСoinService;
        _authenticationService = authenticationService;
        _coinHistoryService = coinHistoryService;
    }

    public void OnGet()
    {
    }

    public async Task OnPostAsync(double amount,string coinName, CancellationToken token)
    {
        var userId = await _authenticationService.GetUserId(HttpContext.User);
        var dailyPrices = await _coinHistoryService.GetCoinsDailyInfoById(coinName, token);
        PriceList = dailyPrices;
        double currentPrice = PriceList!.Select(p => p.CurrentPrice).FirstOrDefault();
        if (userId != null)
        {
            try
            {
                var result = await _buyingСoinService.CoinsSell(int.Parse(userId),coinName, amount, currentPrice);
                Message = $"Вы успешно продали {result.Count} {result.CoinName}";
            }
            catch (BadRequest ex)
            {
                Message = $"Ошибка! {ex.Info}";
            }
        }
    }
}