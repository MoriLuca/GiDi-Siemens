using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GiDiSiemens.Models;

namespace GiDiSiemens.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Siemens()
        {
            ViewData["Message"] = "PLC Siemens s7-1200 - GiDi Automazione";
            //Leggo dal PLC e compongo la classe work
            Repo.SiemensRepo.SiemensWork.ReadAllVariables();
            return View(Repo.SiemensRepo.SiemensWork);
        }

        public IActionResult WriteSiemensPLC()
        {
            ViewData["Message"] = "Scrittura variabili su PLC s7-1200";
            //Leggo dal PLC e compongo la classe work
            Repo.SiemensRepo.SiemensWork.ReadAllVariables();
            return View(Repo.SiemensRepo.SiemensWork.Data);
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
