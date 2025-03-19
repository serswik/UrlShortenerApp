using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Server.Models;
using UrlShortenerApp.Server.Services;
using UrlShortenerApp.Server.Context;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortenerApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IShortUrlService _shortUrlService;

        public UrlsController(ApplicationDbContext context, IShortUrlService shortUrlService)
        {
            _context = context;
            _shortUrlService = shortUrlService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUrls()
        {
            var urls = await _context.ShortUrls.ToListAsync();
            return Ok(urls);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUrl([FromBody] ShortUrl model)
        {
            if (model == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid URL data.");
            }

            var existingUrl = await _context.ShortUrls
                .FirstOrDefaultAsync(u => u.OriginalUrl == model.OriginalUrl);

            if (existingUrl != null)
            {
                return Ok(new { shortCode = existingUrl.ShortCode });
            }

            model.ShortCode = _shortUrlService.GenerateShortUrl(model.OriginalUrl);
            model.CreatedBy = User.Identity.Name;
            model.CreatedAt = DateTime.UtcNow;

            _context.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUrls), new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUrlDetails(int id)
        {
            var url = await _context.ShortUrls.FindAsync(id);
            if (url == null)
            {
                return NotFound();
            }

            return Ok(url);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            var url = await _context.ShortUrls.FindAsync(id);
            if (url == null)
            {
                return NotFound();
            }

            if (url.CreatedBy != User.Identity.Name && !User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            _context.ShortUrls.Remove(url);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
