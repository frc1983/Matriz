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
            PossibleValues = new List<Tuple<int, int>>();

            for (int i = 0; i < Matrix.GetLength(0); i++)
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = new Cell(i, j);
                    PossibleValues.Add(new Tuple<int, int>(i, j));
                }
        }

        public bool IsCompleteLine(int line)
        {
            for (int j = 0; j < this.Matrix.GetLength(1); j++)
            {
                if (!this.Matrix[line, j].Checked)
                    return false;
            }
            return true;
        }

        public bool IsCompleteColumn(int column)
        {
            for (int i = 0; i < this.Matrix.GetLength(0); i++)
            {
                if (!this.Matrix[i, column].Checked)
                    return false;
            }
            return true;
        }

        public bool IsComplete()
        {
            for (int i = 0; i < this.Matrix.GetLength(0); i++)
                for (int j = 0; j < this.Matrix.GetLength(1); j++)
                    if (!this.Matrix[i, j].Checked)
                        return false;

            return true;
        }
    }
}