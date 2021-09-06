using CuestionarioWeb.BL;
using CuestionarioWeb.EN;
using CuestionarioWeb.EN.LoginView;
using CuestionarioWeb.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CuestionarioWeb.UI.Controllers
{
    public class HomeController : Controller
    {
       
        private UsuarioBL _usuarioBL;
        
        private const string key = "%$DUJH238";

        public HomeController(Contexto context)
        {
            _usuarioBL = new UsuarioBL(context);
          
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            string cookie = Request.Cookies[key];
            ResponseModel rm = _usuarioBL.Verify(cookie);
            ViewData["User"] = rm;
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
