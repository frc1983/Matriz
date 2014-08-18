using Prova.Models;
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

        public static void BuildBody(MatrixViewModel matrixVM)
        {
            for (int i = 0; i < matrixVM.Matrix.GetLength(0); i++)
            {
                matrixVM.Html.Append("<tr " + (matrixVM.IsCompleteLine(i, matrixVM.Log) ? "class='complete'" : "") + "><td class='lightGray'> " + (i + 1) + "</td>");
                if (matrixVM.IsCompleteLine(i, matrixVM.Log) && !matrixVM.CompleteLines.Contains(i))
                {
                    matrixVM.CompleteLines.Add(i);
                    LogHelper.LogComplete(matrixVM, EnumFillMatrixType.Line, i + 1);
                }

                for (int j = 0; j < matrixVM.Matrix.GetLength(1); j++)
                {
                    matrixVM.Html.Append("<td " + (matrixVM.IsCompleteColumn(j, matrixVM.Log) ? "class='complete'" : "") + ">" + matrixVM.Matrix[i, j].IsChecked + "</td>");
                    if (matrixVM.IsCompleteColumn(j, matrixVM.Log) && !matrixVM.CompleteColumns.Contains(j))
                    {
                        matrixVM.CompleteColumns.Add(j);
                        LogHelper.LogComplete(matrixVM, EnumFillMatrixType.Column, j + 1);
                    }
                }

                matrixVM.Html.Append("</tr>");
            }
        }
    }
}