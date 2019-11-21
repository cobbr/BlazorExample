using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorExample.Models;

namespace BlazorExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet, Route(""), Route("Home"), Route("Home/Index")]
        public IActionResult Index()
        {
            IEnumerable<Listener> listeners = _context.Listeners.ToList();
            foreach (Listener l in listeners)
            {
                l.ListenerType = _context.ListenerTypes.First(LT => LT.Id == l.ListenerTypeId);
            }
            return View(listeners);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
