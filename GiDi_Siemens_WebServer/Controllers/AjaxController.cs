using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luca;
using GiDi_SiemensApp.Siemens;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GiDi_SiemensApp.Controllers
{
    public class AjaxController : Controller
    {
        private readonly IViewRenderService _viewRenderService;

        public AjaxController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        public JsonResult GetDb1(int a)
        {
            try
            {
                var result = _viewRenderService.RenderToStringAsync("/Views/Home/GetDb1.cshtml", Repo.SiemensPlc);
                return Json(result.Result);
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }

        }

        /// <summary>
        /// Scrittura di una determinata variabile, all'interno della classe del repository e successivamente
        /// all'interno del PLC
        /// </summary>
        /// <param name="aj">parametro inviato dal post ajax</param>
        public void Write(AjaxUpater aj)
        {
            if (!Repo.SiemensPlc.Plc.IsConnected) Repo.SiemensPlc.Plc.Open();
            //Se è stato passato un modello in post
            if (Request.Method == "POST")
            {
                object a = Luca.Siemens.Functons.RebuildTheBlackBox(aj.Content, Repo.SiemensPlc.Data[aj.Index].DotNetDataType);
                Repo.SiemensPlc.Data[aj.Index].RawContent = a;

                if (Repo.SiemensPlc.Data[aj.Index].VariableType == S7.Net.VarType.String)
                {
                    Repo.SiemensPlc.Data[aj.Index].Content = a;
                    Repo.SiemensPlc.BuildRawString_FromWork(Repo.SiemensPlc.Data[aj.Index]);
                }
                else
                {
                    Repo.SiemensPlc.Data[aj.Index].RawContent = a;
                }
                //Scrivo a questo punto la variabile nel PLC
                Repo.SiemensPlc.WriteSingleVaraible(aj.Index);
            }
        }

        public class AjaxUpater
        {
            public int Index { get; set; }
            public string Content { get; set; }
        }


    }
}