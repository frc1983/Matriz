using Prova.Models;
using Prova.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
            PopulateMatrix();

            return View(matrixVM);
        }

        private void PrintMatrix()
        {
            System.Diagnostics.Debug.WriteLine("Print do Array");
            for (int i = 0; i < matrixVM.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrixVM.matrix.GetLength(1); j++)
                {
                    System.Diagnostics.Debug.Write(String.Format(" {0} |", matrixVM.matrix[i, j].IsChecked));
                }
                System.Diagnostics.Debug.WriteLine("");
            }
        }

        private void PopulateMatrix()
        {
            int randomX = random.Next(0, 10);
            int randomY = random.Next(0, 10);

            if (!IsComplete())
            {
                if (!matrixVM.matrix[randomX, randomY].Checked)
                    matrixVM.matrix[randomX, randomY].Checked = true;
                
                PopulateMatrix();
            }
        }

        private bool IsComplete()
        {
            //PrintMatrix();
            for (int i = 0; i < matrixVM.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrixVM.matrix.GetLength(1); j++)
                {
                    if (!matrixVM.matrix[i, j].Checked)
                        return false;
                }
                if (matrixVM.partialComplete.ContainsKey(i))
                {
                    System.Diagnostics.Debug.WriteLine(String.Format("LINHA COMPLETA = {0}", i));
                    matrixVM.partialComplete.Add(i, String.Format("LINHA COMPLETA = {0}", i));
                }
            }

            return true;
        }
    }
}
