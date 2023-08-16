using Core1.Interfaces;
using HW2Sem.DTO;
using HW2Sem.Exceptions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class Register : PageModel
{
    public string Message { get; set; }
    private IRegisterService _registerService;

    public Register(IRegisterService registerService)
    {
        _registerService = registerService;
    }

    public void OnGet()
    {
        
    }
    public async Task OnPostAsync(string email,string password,string username)
    {
        var userRegisterDto = new RegisterUserDto
        {
            Password = password,
            Email = email,
            Username = username
        };
        try
        {
            var newUser = await _registerService.RegisterUser(userRegisterDto);
            Message = "Вы успешно зарегистрировались!";
        }
        catch(BadRequest badRequest)
        {
            Message = $"Ошибка! {badRequest.Info}";
        }
        catch (Exception exception)
        {
            Message = $"Ошибка!{exception.Message}";
        }
    }
    
}