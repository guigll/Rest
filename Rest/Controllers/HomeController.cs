using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rest.Models;

namespace Rest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Adicionar()
        {
            ViewBag.Title = "Adicionar";

            return View();
        }

        public ActionResult Salvar(Pessoa p)
        {
            if (p != null)
            {
                new PessoasController().Post(p);
            }
            return View();

        }
    }
}
