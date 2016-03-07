using System;
using System.Collections.Generic;
using System.Linq;
using Addiscode.SudokuCore;
using Addiscode.SudokuCore.Models;
using Addiscode.SudokuSolver.Models;

namespace Addiscode.SudokuSolver
{
    public class SudokuBoardSolver
    {
        private SudokuBoardInfo boardInfo;
        public int maxNumberOfSolutions = 50;

        public SudokuBoardSolver()
        {
        }
        public SudokuBoardSolver(int requiredNumberOfSolutions)
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
            for (int i = 0; i < boardInfo.BoardSize; i++)
            {
                for (int j = 0; j < boardInfo.BoardSize; j++)
                {
                    if (board[i, j] == 0)
                        unfilledLocations.Add(new Location { Coloumn = i, Row = j });
                }
            }

            //shuffle the unfilled location

            return unfilledLocations;
        }

        public static List<int> GetPosiiblePlaceValues(Location location, int[,] board, int boardSize, int innerBoardSize)
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
                Coloumn = ((int)(location.Coloumn / innerBoardSize)) * innerBoardSize,
                Row = ((int)(location.Row / innerBoardSize)) * innerBoardSize,
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
            boardInfo = CommonMethods.GetSudokuBoardSize(startingBoard);

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
            var possibleValues = GetPosiiblePlaceValues(unfilledLocations[currentLocationCount], currentBoard, boardInfo.BoardSize, boardInfo.InnerBoardSize);
            foreach (var possibleValue in possibleValues)
            {
                //create an updated board with the new value
                var updatedBoard = CommonMethods.CopyBoard(currentBoard, boardInfo.BoardSize);
                updatedBoard[currentLocation.Coloumn, currentLocation.Row] = possibleValue;


                //check to see if this is the last location
                if (currentLocationCount < unfilledLocations.Count - 1)
                {
                    //check to see if the next location has possible Values
                    var nextLocationsPossibleValues = GetPosiiblePlaceValues(unfilledLocations[currentLocationCount + 1], updatedBoard, boardInfo.BoardSize, boardInfo.InnerBoardSize);
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



    }
}
