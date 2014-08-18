using Prova.Models;
using Prova.Util;
using Prova.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Prova.Controllers
{
    public class IndexController : Controller
    {
        private MatrixViewModel matrixVM;  

        public ActionResult Index()
        {
            matrixVM = new MatrixViewModel();
            matrixVM.Html = new StringBuilder();
            matrixVM.Log = new StringBuilder();

            TempData["matrixVM"] = matrixVM;
            TempData.Keep("matrixVM");

            return View(matrixVM);
        }

        public JsonResult PopulateMatrix()
        {
            GetMatrix();
            var tuple = Tuples.GetNextTuple(matrixVM);

            if (!matrixVM.IsComplete(matrixVM.Log))
                if (!matrixVM.Matrix[tuple.Item1, tuple.Item2].Checked)
                {
                    matrixVM.Matrix[tuple.Item1, tuple.Item2].Checked = true;
                    LogHelper.LogCheck(matrixVM, tuple.Item1 + 1, tuple.Item2 + 1, DateTime.Now);
                }

            return DrawTable(true);
        }        

        public JsonResult DrawTable(bool? update)
        {
            if(!update.HasValue)
                GetMatrix();

            matrixVM.Html = new StringBuilder();
            matrixVM.Html.Append("<table>");
            ConstructTable.BuildHeader(matrixVM);
            matrixVM.Html.Append("<tbody>");
            ConstructTable.BuildBody(matrixVM);            
            matrixVM.Html.Append("</tbody></table>");

            var result = new { Html = matrixVM.Html.ToString(), Log = matrixVM.Log.ToString(), Complete = matrixVM.IsComplete(matrixVM.Log).ToString() };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private void GetMatrix()
        {
            matrixVM = TempData["matrixVM"] as MatrixViewModel;
            TempData["matrixVM"] = matrixVM;
            TempData.Keep("matrixVM");
        }
    }
}
