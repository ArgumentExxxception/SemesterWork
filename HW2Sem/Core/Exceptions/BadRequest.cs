namespace HW2Sem.Exceptions;

public class BadRequest: Exception
{
    public string AdditionalInfo { get; set; }
    public string Type { get; set; }
    public string Detail { get; set; }
    public string Title { get; set; }
    public string Info { get; set; }
    
    public BadRequest(string info)
    {
        Type = "bad-request-exception";
        Detail = "Вы отправили некорректные данные";
        Title = "Bad Request Exception";
        AdditionalInfo = "Неккоректный запрос";
        Info = info;
    }
}