using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuSolver.Models;

namespace Addiscode.SudokuGenerator.Models
{
    public class DifficultySituation
    {
        public int MinCountofSituation { get; set; }
        public int MaxNumberOfPossibleValues { get; set; }
    }

    public class DifficultySetting
    {
        public List<DifficultySituation> DifficultySituations { get; set; } 
    }

}
