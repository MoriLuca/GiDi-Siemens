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

        public AjaxController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public JsonResult GetDb1()
        {
            Luca.L_Siemens.ReadPlcAndComposeSiemensWorkFromSiemensDB();
            var result = _viewRenderService.RenderToStringAsync("Ajax/GetDb1", Repo.SiemensRepo.SiemensWork);
            return Json(result.Result);
        }

        [HttpPost]
        public void WriteSiemensPLC([FromBody] AjaxUpater aj)
        {
            //Se è stato passato un modello in post
            if (Request.Method == "POST")
            {
                object sandbox;
                //Scrivo la classe work sul PLC
                Type type = Repo.SiemensRepo.SiemensWork.Data[aj.Index].DotNetDataType;
                if (type == typeof(Int16))
                {
                    sandbox = Convert.ToInt16(aj.Content);
                    Repo.SiemensRepo.SiemensWork.Data[aj.Index].Content = sandbox;
                }
                Luca.L_Siemens.WriteSingleVaraible(Repo.SiemensRepo.SiemensWork.Data[aj.Index]);
            }
        }

        public class AjaxUpater
        {
            public int Index { get; set; }
            public string Content{ get; set; }
        }
    }
}