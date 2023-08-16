namespace HW2Sem.Exceptions;

public class UnauthorizedException: Exception
{
    public string AdditionalInfo { get; set; }
    public string Type { get; set; }
    public string Detail { get; set; }
    public string Title { get; set; }
    public string Instance { get; set; }

    public UnauthorizedException(string instance)
    {
        Type = "unauthorized-exception";
        Detail = "При авторизации произошла ошибка";
        Title = "Unauthorized Exception";
        AdditionalInfo = "Неверный логин или пароль";
        Instance = instance;
    }
}