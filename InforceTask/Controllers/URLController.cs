using InforceTask.Context;
using InforceTask.Models;
using InforceTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InforceTask.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class URLController : Controller
    {
        private readonly URLContext _context;

        public URLController(URLContext context)
        {
            _context = context;
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var urls = await _context.Urls.ToListAsync();
            var sorted =  urls.OrderByDescending(u=>u.Id);
            return Ok(sorted);
        }
        [Route("info/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> URLInfo(int? id)
        {
            if (id == null)
                return NotFound();
            var url = await _context.Urls.FirstOrDefaultAsync(m => m.Id == id);
            if (url == null)
                return NotFound();
            return View(url);
        }

        [HttpPost]
        public async Task<IActionResult> AddUrl(URL url)
        {
            if (ModelState.IsValid)
            {
                URL urlInDB = await _context.Urls.FirstOrDefaultAsync(u => u.Long == url.Long);
                if (urlInDB == null)
                {
                    var username = User.Identity.Name;
                    if (username != null)
                        url.CreatedBy = username;
                    url.CreatedDate = DateTime.Now;
                    if (!_context.Urls.Any())
                    {
                        _context.Add(url);
                        await _context.SaveChangesAsync();
                        var nextId = _context.Urls.Max(u => u.Id);
                        url.Short = Shortener.Shorten(nextId);
                        _context.Update(url);
                    }
                    else
                    {
                        var nextId = _context.Urls.Max(u => u.Id) + 1;
                        url.Short = Shortener.Shorten(nextId);
                        _context.Add(url);
                    }
                    await _context.SaveChangesAsync();
                    return Ok(url);
                }
            }
            return BadRequest();
        }
  
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var user = User.Identity;
            var user1 = await _context.Users.FirstOrDefaultAsync(u => u.Login == user.Name);
            if (user1 != null)
                if (user1.RoleId == 1)
                {
                    foreach (var item in _context.Urls)
                    {
                        _context.Remove(item);
                    }
                    await _context.SaveChangesAsync();
                }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var actualUser = User.Identity;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == actualUser.Name);
            var url = await _context.Urls.FindAsync(id);
            if (user != null)
            {
                if (user.RoleId == 1)
                {
                    _context.Remove(url);
                    await _context.SaveChangesAsync();
                    return Ok(url);
                }
            }
            if (url.CreatedBy == actualUser.Name)
                _context.Remove(url);
            await _context.SaveChangesAsync();
            return Ok(url);
        }

        [AllowAnonymous]
        [Route("table")]
        public IActionResult Table()
        {
            return View();
        }
    }
}
