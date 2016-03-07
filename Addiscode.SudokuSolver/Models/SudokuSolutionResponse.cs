using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuCore;

namespace Addiscode.SudokuSolver.Models
{
    public class SudokuSolutionResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<int[,]> SolvedBoards { get; set; }
    }
}
