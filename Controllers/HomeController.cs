using Avonale_ApiGit.Models;
using Avonale_ApiGit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Avonale_ApiGit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiGit api = new ApiGit();



        public IActionResult Index(string termoPesquisa = null)
        {
            
            ViewData["pesquisa"] = !String.IsNullOrEmpty(termoPesquisa) ? termoPesquisa : "";

            if (!string.IsNullOrEmpty(termoPesquisa))
            {
                return View(api.Pesquisar(termoPesquisa));
            }
            return View();
        }


        public IActionResult MeusRepositorios()
        {
            return View(api.GetMeusRepositorios());
        }

        public IActionResult Repositorio()
        {
            return View();
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
