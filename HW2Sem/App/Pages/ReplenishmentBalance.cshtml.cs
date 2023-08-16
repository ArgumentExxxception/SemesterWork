using Core1.Interfaces;
using HW2Sem.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

[Authorize]
public class ReplenishmentBalance : PageModel
{
    public string Message { get; set; }
    private readonly ILogger<ReplenishmentBalance> _logger;
    private readonly IReplenishmentOfTheBalanceService _replenishmentOfTheBalanceService;
    private readonly IAuthenticationService _authenticationService;
    
    public ReplenishmentBalance(IReplenishmentOfTheBalanceService replenishmentOfTheBalanceService, IAuthenticationService authenticationService, ILogger<ReplenishmentBalance> logger)
    {
        _replenishmentOfTheBalanceService = replenishmentOfTheBalanceService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public void OnGet()
    {
        
    }
    
    
    public async Task OnPost(int sum)
    {
        var userId = await _authenticationService.GetUserId(HttpContext.User);
        if (sum == 0)
        {
            Message = "Нельзя пополнить счет на 0 рублей";
        }

        try
        {
            var result = await _replenishmentOfTheBalanceService.ReplenishmentBalance(int.Parse(userId), sum);
            Message = $"Вы успешно пополнили счет на {sum} рублей. На балансе {result.Balance} рублей";
        }
        catch (BadRequest ex)
        {
            Message = $"Error! Info: {ex.Info}";
        }
        catch (Exception exception)
        {
            Message = $"Error! Info: {exception.Message}";
        }
    }
}