using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GiDi_SiemensApp.Siemens;
using Siemens.Models;

namespace GiDi_SiemensApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SiemensTag()
        {
            return View(Repo.SiemensPlc);
        }

        public IActionResult MonitorSiemens()
        {
            ViewData["Message"] = "PLC Siemens s7-1200 - GiDi Automazione";
            return View();
        }

        public IActionResult WriteSiemensPLC()
        {
            ViewData["Message"] = "Scrittura variabili su PLC s7-1200";
            //Leggo dal PLC e compongo la classe work
            return View(Repo.SiemensPlc.Data);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
