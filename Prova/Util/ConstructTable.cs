using Prova.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Prova.Util
{
    public static class ConstructTable
    {
        public static void BuildHeader(MatrixViewModel matrixVM)
        {
            matrixVM.Html.Append("<thead><th class='black'></th>");
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                matrixVM.Html.Append("<th> " + (i + 1) + "</th>");
            }
            matrixVM.Html.Append("</thead>");
        }
    }
}