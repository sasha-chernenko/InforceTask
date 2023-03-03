using InforceTask.Context;
using InforceTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InforceTask.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        
        private readonly URLContext _context;

        public HomeController(URLContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(string? id)
        {
            URL url = _context.Urls.FirstOrDefault(u => u.Short == id);
            if (url == null)
                return NotFound();
            if(!url.Long.StartsWith("https://") && !url.Long.StartsWith("http://"))
                return Redirect("http://"+url.Long);
            return Redirect(url.Long);
        }
    }
}