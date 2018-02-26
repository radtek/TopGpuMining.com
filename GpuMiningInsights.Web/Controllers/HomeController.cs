﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using GpuMiningInsights.Core;
using OpenQA.Selenium;

namespace GpuMiningInsights.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult Index(bool isLoad = true)
        {
            var results = Insighter.GetInsights();
            HttpContext.Application["Insights"] = results;
            return View(results);
        }
        public ActionResult Index()
        {
            List<GPU> results = HttpContext.Application["Insights"] as List<GPU>;
            if (results == null)
            {
                //results = Insighter.GetInsights();
                HttpContext.Application["Insights"] = results;

            }
            return View(results);
        }
        [HttpPost]
        public ActionResult PushData(List<GPU> gpus)
        {
            HttpContext.Application["Insights"] = gpus;
            return Json(true);
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