using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Server.Models;

namespace UrlShortenerApp.Server.Context
{
    public class ApplicationDbContext  : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls {  get; set; }
    }
}
