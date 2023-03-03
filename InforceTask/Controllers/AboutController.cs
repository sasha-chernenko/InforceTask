using InforceTask.Context;
using InforceTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceTask.Controllers
{
    [Route("[controller]")]
    public class AboutController : Controller
    {
        private readonly URLContext _context;
        
        public AboutController(URLContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var temp = _context.About.FirstOrDefault();
            _context.Update(temp);
            _context.SaveChanges();
            return View(temp);
        }
        [HttpGet]
        [Route("Edit")]
        [Authorize(Roles ="admin")]
        public IActionResult Edit()
        {
            var about = _context.About.FirstOrDefault();
            return View(about);
        }

        [HttpPost]
        [Route("Edit")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(About aboutModel)
        {
            _context.About.FirstOrDefault().Text = aboutModel.Text;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "About");
        }
    }
}
