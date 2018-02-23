using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luca;

namespace GiDiSiemens.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IViewRenderService _viewRenderService;

        public JsonResult GetDb1()
        {
            Luca.L_Siemens.ReadPlcAndComposeSiemensWorkFromSiemensDB();
            var result = _viewRenderService.RenderToStringAsync("Service/Read", Repo.SiemensRepo.SiemensWork);
            return Json(result.Result);
        }
    }
}