using Core1.Interfaces;
using HW2Sem.DTO;
using HW2Sem.Exceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class Authorize : PageModel
{
    public string Message;
    private IAuthenticationService _authenticationService;

    public Authorize(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public void OnGet()
    {
        
    }
    public async Task OnPostAsync(string email, string password)
    {
        var loginUserDto = new UserDto
        {
            Email = email,
            Password = password
        };
        try
        {
            await _authenticationService.UserAuthenticationAsync(loginUserDto);
            Message = "Вы успешно вошли в аккаунт";
        }
        catch (BadRequest badRequest)
        {
            Message = $"Ошибка! {badRequest.Message}";
        }
        catch (Exception exception)
        {
            Message = $"Ошибка! {exception.Message}";
        }
    }
}