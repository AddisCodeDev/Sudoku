using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuSolver.Models;

namespace Addiscode.SudokuSolver
{
    public class SudokuSolver
    {
        private int boardSize = 0;
        private int innerBoardSize = 0;
        public int maxNumberOfSolutions = 50;

        public SudokuSolver()
        {
        }
        public SudokuSolver(int requiredNumberOfSolutions)
        {
            maxNumberOfSolutions = requiredNumberOfSolutions;
        }

        public SudokuSolutionResponse Solve(SudokuProblemDTO problemDto)
        {
            var status = new ResponseStatus();
            var solvedBoards = new List<int[,]>();
            try
            {
                solvedBoards = SolveSudokuBoard(problemDto.SudokuBoard);
            }
            catch (Exception e)
            {
                status.AddErrors(e);
            }
            return new SudokuSolutionResponse { SolvedBoards = solvedBoards, ResponseStatus = status };
        }

        internal List<Location> GetUnfilledBoardLocations(int[,] board)
        {
            var unfilledLocations = new List<Location>();

            //go throught the board and see where the value is 0 or is unfilled
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == 0)
                        unfilledLocations.Add(new Location { Coloumn = i, Row = j });
                }
            }
            return unfilledLocations;
        }

        internal List<int> GetPosiiblePlaceValues(Location location, int[,] board)
        {
            var possibleValues = new List<int>();

            //populate all possible values
            for (int i = 0; i < boardSize; i++)
            {
                possibleValues.Add(i + 1);
            }

            //go throught the column values and remove unecessary values from possible values
            for (int i = 0; i < boardSize; i++)
            {
                possibleValues.Remove(board[i, location.Row]);
            }

            //go throught the riws values and remove unecessary values from possible values
            for (int i = 0; i < boardSize; i++)
            {
                possibleValues.Remove(board[location.Coloumn, i]);
            }

            //go throught the inner ring and remove any necessary values from possible values
            var innerBlockStartingLocation = new Location
            {
                Coloumn = ((int)(location.Coloumn / innerBoardSize)) * 3,
                Row = ((int)(location.Row / innerBoardSize)) * 3,
            };
            for (int i = 0; i < innerBoardSize; i++)
            {
                for (int j = 0; j < innerBoardSize; j++)
                {
                    possibleValues.Remove(board[innerBlockStartingLocation.Coloumn + i, innerBlockStartingLocation.Row + j]);
                }
            }

            return possibleValues;
        }

        internal List<int[,]> SolveSudokuBoard(int[,] startingBoard)
        {
            //setup the board size
            boardSize = GetSudokuBoardSize(startingBoard);

            //get all the unfilled locations
            var unfilledLocations = GetUnfilledBoardLocations(startingBoard);

            var solvedBoards = new List<int[,]>();

            //start populating the solution tree
            AddLocationsSolutionsToList(unfilledLocations, startingBoard, solvedBoards, 0);
            return solvedBoards;
        }

        internal void AddLocationsSolutionsToList(List<Location> unfilledLocations, int[,] currentBoard, List<int[,]> solvedBoards, int currentLocationCount)
        {
            //check if the maximum number of solutions is reached
            if (solvedBoards.Count >= maxNumberOfSolutions)
                return;

            var currentLocation = unfilledLocations[currentLocationCount];
            var possibleValues = GetPosiiblePlaceValues(unfilledLocations[currentLocationCount], currentBoard);
            foreach (var possibleValue in possibleValues)
            {
                //create an updated board with the new value
                var updatedBoard = CopyBoard(currentBoard);
                updatedBoard[currentLocation.Coloumn, currentLocation.Row] = possibleValue;


                //check to see if this is the last location
                if (currentLocationCount < unfilledLocations.Count - 1)
                {
                    //check to see if the next location has possible Values
                    var nextLocationsPossibleValues = GetPosiiblePlaceValues(unfilledLocations[currentLocationCount + 1], updatedBoard);
                    if (nextLocationsPossibleValues.Any())
                        //add the next locations solution to the tree
                        AddLocationsSolutionsToList(unfilledLocations, updatedBoard, solvedBoards, currentLocationCount + 1);
                }
                else
                {
                    solvedBoards.Add(updatedBoard);
                }

            }
        }


        internal int GetSudokuBoardSize(int[,] board)
        {
            var boardSizeSquerRoot = Math.Sqrt(board.GetLength(0));
            innerBoardSize = (int)boardSizeSquerRoot;
            //check to see if the board is a proper sudoku board (row number == coloumn number)
            if (board.GetLength(0) != board.GetLength(1) || ((int)(boardSizeSquerRoot * 10) != innerBoardSize * 10))
                throw new Exception("The board is not a proper sudoku board");
            return board.GetLength(0);
        }

        internal int[,] CopyBoard(int[,] board)
        {
            //save a copy of the board in a new memory location
            var copyBoard = new int[boardSize, boardSize];

            //copy over all the values from the previous board
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    copyBoard[i, j] = board[i, j];
                }
            }
            return copyBoard;
        }
    }
}
