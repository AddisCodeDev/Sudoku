using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addiscode.SudokuSolver.Models
{
    public class Location
    {
        public Location()
        {
        }
        
        public Location(int row, int coloumn)
        {
            Row = row;
            Coloumn = coloumn;
        }
        public int Row { get; set; }
        public int Coloumn { get; set; }
    }
}
