using Prova.Models;
using Prova.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prova.Util
{
    public class LogHelper
    {
        public static void LogCheck(MatrixViewModel matrixVM, int posX, int posY, DateTime time){
            matrixVM.Log.AppendLine(String.Format("L{0},C{1} - {2}", posX, posY, time.ToString("H:mm:ss:ffff")));
        }

        public static void LogComplete(MatrixViewModel matrixVM, EnumFillMatrixType type, int pos)
        {
            if(type == EnumFillMatrixType.Line)
                matrixVM.Log.AppendLine(String.Format("LINHA {0} completa!", pos));
            else if (type == EnumFillMatrixType.Column)
                matrixVM.Log.AppendLine(String.Format("COLUNA {0} completa!", pos));
        }
    }
}