using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Luca;

namespace Siemens.Controllers
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
            var result = _viewRenderService.RenderToStringAsync("Ajax/GetDb1", Repo.SiemensRepo.SiemensPlc);
            return Json(result.Result);
        }

        /// <summary>
        /// Scrittura di una determinata variabile, all'interno della classe del repository e successivamente
        /// all'interno del PLC
        /// </summary>
        /// <param name="aj">parametro inviato dal post ajax</param>
        public void Write(AjaxUpater aj)
        {
            if (!Siemens.Repo.SiemensRepo.SiemensPlc.Plc.IsConnected) Siemens.Repo.SiemensRepo.SiemensPlc.Plc.Open();
            //Se è stato passato un modello in post
            if (Request.Method == "POST")
            {
                object a = RebuildTheBlackBox(aj.Content, Repo.SiemensRepo.SiemensPlc.Data[aj.Index].DotNetDataType);
                Repo.SiemensRepo.SiemensPlc.Data[aj.Index].RawContent = a;

                if (Repo.SiemensRepo.SiemensPlc.Data[aj.Index].VariableType == S7.Net.VarType.String)
                {
                    Repo.SiemensRepo.SiemensPlc.Data[aj.Index].Content = a;
                    Repo.SiemensRepo.SiemensPlc.BuildRawString_FromWork(Repo.SiemensRepo.SiemensPlc.Data[aj.Index]);
                }
                else
                {
                    Repo.SiemensRepo.SiemensPlc.Data[aj.Index].RawContent = a;
                }
                //Scrivo a questo punto la variabile nel PLC
                Repo.SiemensRepo.SiemensPlc.WriteSingleVaraible(aj.Index);

            }
        }

        public class AjaxUpater
        {
            public int Index { get; set; }
            public string Content { get; set; }
        }

        public static object RebuildTheBlackBox(string stringFromAjaxPost, Type type)
        {
            try
            {
                if (type == typeof(Int16))
                {
                    return Convert.ToInt16(stringFromAjaxPost);
                }
                if (type == typeof(UInt16))
                {
                    return Convert.ToUInt16(stringFromAjaxPost);
                }
                if (type == typeof(Int32))
                {
                    return Convert.ToInt32(stringFromAjaxPost);
                }
                if (type == typeof(UInt32))
                {
                    return Convert.ToUInt32(stringFromAjaxPost);
                }
                if (type == typeof(Single))
                {
                    return Convert.ToDouble(stringFromAjaxPost);
                }
                if (type == typeof(double))
                {
                    return Convert.ToDouble(stringFromAjaxPost);
                }
                if (type == typeof(string))
                {
                    return Convert.ToString(stringFromAjaxPost);
                }
                else
                {
                    throw new NotImplementedException("Il tipo dato Type passato alla funzione, non è ancora stato implementato");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            //Se non è nessuna del tipo di variabili testate sopra
            return null;
        }
    }
}