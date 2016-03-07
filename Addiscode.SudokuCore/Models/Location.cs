namespace Addiscode.SudokuCore.Models
{
    public class Location
    {
        public Location()
        {
        }
        
        public Location(int coloumn, int row)
        {
            Row = row;
            Coloumn = coloumn;
        }
        public int Row { get; set; }
        public int Coloumn { get; set; }
    }
}
