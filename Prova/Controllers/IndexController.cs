using Prova.Models;
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
        Random random = new Random();

        public ActionResult Index()
        {
            matrixVM = new MatrixViewModel();
            TempData["matrixVM"] = matrixVM;
            TempData.Keep("matrixVM");

            return View(matrixVM);
        }

        private void PrintMatrix()
        {
            System.Diagnostics.Debug.WriteLine("Print do Array");
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrixVM.Matrix.GetLength(1); j++)
                {
                    System.Diagnostics.Debug.Write(String.Format(" {0} |", matrixVM.Matrix[i, j].IsChecked));
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        public JsonResult PopulateMatrix()
        {
            GetMatrix();
            var tuple = GetNextTuple();
            if (!IsComplete())
            {
                if (!matrixVM.Matrix[tuple.Item1, tuple.Item2].Checked)
                    matrixVM.Matrix[tuple.Item1, tuple.Item2].Checked = true; 
            }
            return DrawTable();
        }

        private bool IsComplete()
        {
            //PrintMatrix();
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrixVM.Matrix.GetLength(1); j++)
                {
                    if (!matrixVM.Matrix[i, j].Checked)
                        return false;
                }
                if (matrixVM.PartialComplete.ContainsKey(i))
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("LINHA COMPLETA = {0}", i));
                    matrixVM.PartialComplete.Add(i, String.Format("LINHA COMPLETA = {0}", i));
                }
            }

            return true;
        }

        private Tuple<int, int> GetNextTuple()
        {
            Tuple<int, int> tuple = null;

            if (matrixVM.PossibleValues.Count > 0)
            {
                int randomPosition = random.Next(0, matrixVM.PossibleValues.Count);
                tuple = matrixVM.PossibleValues.ElementAt(randomPosition);
                matrixVM.PossibleValues.RemoveAt(randomPosition);
            }

            return tuple;
        }

        public JsonResult DrawTable()
        {
            matrixVM.Html = new StringBuilder();
            matrixVM.Log = new StringBuilder();

            matrixVM.Html.Append("<table>");
            BuildHeader(matrixVM.Html);
            
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                matrixVM.Html.Append("<tbody><tr " +
                    (IsCompleteLine(i) ? "class='complete'" : "") + 
                    "><td> " + i + "</td>");
                for (int j = 0; j < matrixVM.Matrix.GetLength(1); j++)
                {
                    matrixVM.Html.Append("<td " + 
                        (IsCompleteColumn(j) ? "class='complete'" : "") + 
                        ">" + matrixVM.Matrix[i, j].IsChecked + "</td>");
                    matrixVM.Log.AppendLine(String.Format("Marcado em linha {0} e coluna {1}", i, j));
                    
                }
                matrixVM.Html.Append("</tr>");
            }
            matrixVM.Html.Append("</tbody></table>");

            var result = new { Html = matrixVM.Html.ToString(), Log = matrixVM.Log.ToString(), Complete = IsComplete().ToString() };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private bool IsCompleteLine(int line)
        {
            for (int j = 0; j < matrixVM.Matrix.GetLength(1); j++)
            {
                if (!matrixVM.Matrix[line, j].Checked)
                    return false;
            }
            return true;
        }

        private bool IsCompleteColumn(int column)
        {
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                if (!matrixVM.Matrix[i, column].Checked)
                    return false;
            }
            return true;
        }

        private void BuildHeader(StringBuilder stringBuilder)
        {
            matrixVM.Html.Append("<thead><th></th>");
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                matrixVM.Html.Append("<th> " + i + "</th>");
            }
            matrixVM.Html.Append("</thead>");
        }

        private void GetMatrix()
        {
            matrixVM = TempData["matrixVM"] as MatrixViewModel;
            TempData["matrixVM"] = matrixVM;
            TempData.Keep("matrixVM");
        }
    }
}
