namespace UrlShortenerApp.Server.Services
{
    public interface IShortUrlService
    {
        string GenerateShortUrl(string originalUrl);
    }
}
