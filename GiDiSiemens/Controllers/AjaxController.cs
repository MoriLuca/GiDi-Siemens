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
            #warning lettura delle variabili da spostare in un altro punto
            //leggo tutte la variabili
            Repo.SiemensRepo.SiemensMem.ReadAllVariables(Repo.SiemensRepo.PLC);
            var result = _viewRenderService.RenderToStringAsync("Ajax/GetDb1", Repo.SiemensRepo.SiemensMem);
            return Json(result.Result);
        }

        /// <summary>
        /// Scrittura di una determinata variabile, all'interno della classe del repository e successivamente
        /// all'interno del PLC
        /// </summary>
        /// <param name="aj">parametro inviato dal post ajax</param>
        public void Write(AjaxUpater aj)
        {
            //Se è stato passato un modello in post
            if (Request.Method == "POST")
            {

                //Oggetto temporaneo, utilizzato come contenitore per la conversione nel tipo dato adeguato
                object blackBox;
                //Lettura del tipo dato, dall'elemento con index pari a quello richiesto dalla chiamata Ajax
                Type type = Repo.SiemensRepo.SiemensMem.Data[aj.Index].DotNetDataType;
                //Una volta letto il tipo dati, richiamo la funzione che esegue il cast dell'oggetto
                blackBox = Luca.Siemens.StaticFunctions.Functions.RebuildTheBlackBox(aj.Content, type);
                //Convertito nel tipo dati corretto, inserisco il valore nel repository all'index indicato dal post ajax
                Repo.SiemensRepo.SiemensMem.Data[aj.Index].Content = blackBox;
                Repo.SiemensRepo.SiemensMem.Data[aj.Index].BuildRawVariableFromWork();
                //Scrivo a questo punto la variabile nel PLC
                Repo.SiemensRepo.SiemensMem.WriteSingleVaraible(Repo.SiemensRepo.PLC, Repo.SiemensRepo.SiemensMem.Data[aj.Index]);

            }
        }

        public class AjaxUpater
        {
            public int Index { get; set; }
            public string Content { get; set; }
        }
    }
}