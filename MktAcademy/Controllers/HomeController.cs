﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MktAcademy.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            //HomeIndexViewModel model = new HomeIndexViewModel();
            //return View(model.CreateModel(search));
           return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}