using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UrlShortenerApp.Server.Models;

namespace UrlShortenerApp.Server.Context
{
    public class ApplicationDbContext  : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ShortUrl> ShortUrls {  get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ShortUrl>()
                .HasIndex(u => u.ShortCode)
                .IsUnique();

            builder.Entity<ShortUrl>().HasData(
               new ShortUrl { Id = 1, OriginalUrl = "https://google.com", ShortCode = "abc123", CreatedAt = DateTime.UtcNow },
               new ShortUrl { Id = 2, OriginalUrl = "https://microsoft.com", ShortCode = "xyz789", CreatedAt = DateTime.UtcNow }
           );
        }
    }
}
