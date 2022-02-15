using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            if (HttpContext.Session.GetInt32("Count") == null)
            {
                HttpContext.Session.SetInt32("Count", 1);
            }
            ViewBag.Count = HttpContext.Session.GetInt32("Count");

            // Passcode newPass = new Passcode();
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string RandPass = new string(Enumerable.Repeat(chars, 14)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            ViewBag.newPass = RandPass;
            return View("Index");
        }

        [HttpPost("/generate")]
        public IActionResult GeneratePasscode()
        {
            if (HttpContext.Session.GetInt32("Count") != null)
            {
                int? Counter = HttpContext.Session.GetInt32("Count");
                Counter++;
                HttpContext.Session.SetInt32("Count", (int)Counter++);
                return RedirectToAction ("Index");
            }
            // HttpContext.Session.SetInt32("Count", 1);
            return View("Index");

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
