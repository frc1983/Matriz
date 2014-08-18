using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prova.Models
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Boolean Checked { get; set; }
        public String IsChecked { get { return (Checked ? "X" : ""); } }

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Checked = false;
        }
    }
}
