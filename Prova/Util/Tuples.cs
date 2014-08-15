using Prova.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prova.Util
{
    public class Tuples
    {
        private static Random random = new Random();

        public static Tuple<int, int> GetNextTuple(MatrixViewModel matrixVM)
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
    }
}