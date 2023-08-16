namespace Core1.Interfaces;

public interface ICoinGeckoApiClient
{
    public Task<HttpContent> GetAllCoinsPrice();
}