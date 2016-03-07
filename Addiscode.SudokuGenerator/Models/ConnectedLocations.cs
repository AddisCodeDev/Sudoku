using System.Collections.Generic;
using Addiscode.SudokuCore.Models;

namespace Addiscode.SudokuGenerator.Models
{
    public class ConnectedLocations
    {
        public ConnectedLocations()
        {
            ColoumnConnections = new List<Location>();
            RowConnections = new List<Location>();
            InnerBlockConnections = new List<Location>();
        }
        public List<Location> ColoumnConnections { get; set; }
        public List<Location> RowConnections { get; set; }
        public List<Location> InnerBlockConnections { get; set; }

    }
}
