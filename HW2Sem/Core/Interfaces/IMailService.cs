using HW2Sem.Models;

namespace Core1.Interfaces;

public interface IMailService
{
    Task SendWelcomeEmailAsync(WelcomeRequest request);
}