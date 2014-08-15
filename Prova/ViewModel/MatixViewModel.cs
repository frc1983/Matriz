using Prova.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Prova.ViewModel
{
    public class MatrixViewModel
    {
        public Cell[,] Matrix;
        public Dictionary<int, String> PartialComplete;
        public StringBuilder Html { get; set; }
        public StringBuilder Log { get; set; }
        public List<Tuple<int, int>> PossibleValues { get; set; }

        public MatrixViewModel()
        {
            Init();
        }

        private void Init()
        {
            Matrix = new Cell[10, 10];
            PartialComplete = new Dictionary<int, string>();
            PossibleValues = new List<Tuple<int, int>>();

            for (int i = 0; i < Matrix.GetLength(0); i++)
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = new Cell(i, j);
                    PossibleValues.Add(new Tuple<int, int>(i, j));
                }
        }
    }
}