﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace UrlShortenerApp.Server.Models
{
    public class ShortUrl
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OriginalUrl { get; set; } = string.Empty;

        [Required]
        public string ShortCode { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}
