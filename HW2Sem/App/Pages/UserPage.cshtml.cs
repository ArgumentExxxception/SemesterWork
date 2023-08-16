using Core1.Interfaces;
using HW2Sem.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class UserPage : PageModel
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;
    public List<Purchase> Purchases { get; set; }
    public List<UserCoins>? UserCoinsList { get; set; }
    public List<User> User { get; set; }
    

    public UserPage(IAuthenticationService authenticationService, IUserService userService)
    {
        _authenticationService = authenticationService;
        _userService = userService;
    }

    public async Task OnGet()
    {
        var userId = await _authenticationService.GetUserId(HttpContext.User);
        var user = await _userService.GetUserById(int.Parse(userId));
        var userPurchase = await _userService.GetUserPurchaseById(int.Parse(userId));
        var userCoins = await _userService.GetUserCoinsById(int.Parse(userId));
        Purchases = userPurchase;
        UserCoinsList = userCoins;
        User = user;
    }
}