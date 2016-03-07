using System.Collections.Generic;
using Addiscode.SudokuGenerator.Models;

namespace Addiscode.SudokuGenerator
{
    public class BoardPresetDifficulty_9X9
    {
        public static DifficultySetting DifficultySetting = new DifficultySetting
        {
            DifficultySituations = new List<DifficultySituation>
            {
                new DifficultySituation { MinCountofSituation = 2, MaxNumberOfPossibleValues = 3}
            }
        };
    }
}
