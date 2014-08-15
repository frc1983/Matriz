using Prova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prova.ViewModel
{
    public class MatrixViewModel
    {
        public Cell[,] matrix;
        public Dictionary<int, String> partialComplete; 

        public MatrixViewModel()
        {
            Init();
        }

        private void Init()
        {
            matrix = new Cell[10, 10];
            partialComplete = new Dictionary<int, string>();

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    matrix[i, j] = new Cell(i, j);
        }
    }
}