using System;
using System.Linq;
using System.Text;
using UrlShortenerApp.Server.Context;
using UrlShortenerApp.Server.Models;

namespace UrlShortenerApp.Server.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly ApplicationDbContext _context;
        private const int ShortUrlLength = 6;
        private const string Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new();

        public ShortUrlService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateShortUrl(string originalUrl)
        {
            var existingUrl = _context.ShortUrls.FirstOrDefault(u => u.OriginalUrl == originalUrl);
            if (existingUrl != null)
            {
                return existingUrl.ShortCode;
            }

            string shortCode;
            do
            {
                shortCode = GenerateRandomCode();
            } while (_context.ShortUrls.Any(u => u.ShortCode == shortCode));

            return shortCode;
        }

        private string GenerateRandomCode()
        {
            var sb = new StringBuilder(ShortUrlLength);
            for (int i = 0; i < ShortUrlLength; i++)
            {
                sb.Append(Characters[Random.Next(Characters.Length)]);
            }
            return sb.ToString();
        }
    }
}
