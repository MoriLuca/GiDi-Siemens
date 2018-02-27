using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Siemens.Models;

namespace Siemens.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if(!Siemens.Repo.SiemensRepo.SiemensPlc.Plc.IsConnected) Siemens.Repo.SiemensRepo.SiemensPlc.Plc.Open();
            return View();
        }

        public IActionResult MonitorSiemens()
        {
            if (!Siemens.Repo.SiemensRepo.SiemensPlc.Plc.IsConnected) Siemens.Repo.SiemensRepo.SiemensPlc.Plc.Open();
            ViewData["Message"] = "PLC Siemens s7-1200 - GiDi Automazione";
            return View();
        }

        public IActionResult WriteSiemensPLC()
        {
            if (!Siemens.Repo.SiemensRepo.SiemensPlc.Plc.IsConnected) Siemens.Repo.SiemensRepo.SiemensPlc.Plc.Open();
            ViewData["Message"] = "Scrittura variabili su PLC s7-1200";
            //Leggo dal PLC e compongo la classe work
            Repo.SiemensRepo.SiemensPlc.ReadAllVariables();
            return View(Repo.SiemensRepo.SiemensPlc.Data);
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
