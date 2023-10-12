using MyNumsWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyNumsWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IMyNumsRepo repo = new MyNumsRepo();
            ViewBag.Nums = repo.GetNumbers();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}