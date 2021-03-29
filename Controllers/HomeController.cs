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

        public IActionResult Index(string nomeRepositorio = null, bool limite = false)
        {
            ViewBag.limite = limite;
            if (!string.IsNullOrEmpty(nomeRepositorio))
            {
                try
                {
                    List<Repositorio> reps = api.Pesquisar(nomeRepositorio);
                    if (reps[0].Equals(null))
                    { 
                        return View();
                    }
                    return View(reps);
                }
                catch
                {
                    return View();
                }

            }
            else
            {
                return View();
            }

        }


        public IActionResult MeusRepositorios()
        {
            try
            {
                return View(api.GetMeusRepositorios());
            }
            catch
            {
                ViewBag.Error = true;
                return RedirectToAction("Index", "Home", new { limite = true }); ;
            }

        }

        public IActionResult Repositorio(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return View(api.GetRepositorioID(id));
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                ViewBag.Error = true;
                return RedirectToAction("Index", "Home", new { limite = true});
            }


        }


        public IActionResult Favoritos()
        {
            TxtData.Init();

            string[] ids = TxtData.LerData();
            List<Repositorio> reps = new List<Repositorio>();
            try
            {
                foreach (string id_string in ids)
                {
                    if (id_string != "" | id_string == null)
                    {
                        reps.Add(api.GetRepositorioID(id_string));
                    }
                }
                ViewBag.reps = reps;
                return View(reps);
            }
            catch
            {

                return RedirectToAction("Index", "Home", new { limite = true });
            }
            return View(reps);



        }


        public IActionResult Adicionar(string id)
        {
            //Salva no data.txt
            TxtData.Adicionar(id);

            return RedirectToAction("Favoritos", "Home", new { });
        }
        public IActionResult Remover(string id)
        {
            //Deleta do data.txt
            bool resultado = TxtData.Remover(id);

            return RedirectToAction("Favoritos", "Home", new { });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
