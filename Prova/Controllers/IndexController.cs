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
                    matrixVM.Matrix[tuple.Item1, tuple.Item2].Checked = true;

            return DrawTable();
        }        

        public JsonResult DrawTable()
        {
            matrixVM.Html = new StringBuilder();
            matrixVM.Log = new StringBuilder();

            matrixVM.Html.Append("<table>");
            ConstructTable.BuildHeader(matrixVM);
            
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                matrixVM.Html.Append("<tbody><tr " + (matrixVM.IsCompleteLine(i, matrixVM.Log) ? "class='complete'" : "") + "><td> " + i + "</td>");

                for (int j = 0; j < matrixVM.Matrix.GetLength(1); j++)
                {
                    matrixVM.Html.Append("<td " + (matrixVM.IsCompleteColumn(j, matrixVM.Log) ? "class='complete'" : "") + ">" + matrixVM.Matrix[i, j].IsChecked + "</td>");
                    matrixVM.Log.AppendLine(String.Format("Marcado em linha {0} e coluna {1}", i, j));                    
                }

                matrixVM.Html.Append("</tr>");
            }
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
