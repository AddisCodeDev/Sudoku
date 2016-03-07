using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuCore;
using Addiscode.SudokuCore.Models;
using Addiscode.SudokuGenerator.Models;
using Addiscode.SudokuSolver;
using Addiscode.SudokuSolver.Models;

namespace Addiscode.SudokuGenerator
{
    public class SudokuBoardGenerator
    {
        private int[,] originalBoard;
        private SudokuBoardInfo boardInfo;
        private List<Location> unfilledLocations;
        private List<Location> usedConnectedLocations;
        public int maxNumberOfSolutions = 1;

        public SudokuBoardGenerator()
        {
            unfilledLocations = new List<Location>();
            usedConnectedLocations = new List<Location>();
        }

        public SudokuGeneratorResponse GenerateSudokuBoard(SudokuGeneratorDTO generatorDTO)
        {
            throw new NotImplementedException();
        }

        public int[,] GenerateSudokuBoardWithDifficultySetting(int[,] board, DifficultySetting difficultySetting)
        {
            var updatedBoard = board;
            
            //setup the board info
            boardInfo = CommonMethods.GetSudokuBoardSize(board);

            //setup each difficulty situation that is required
            foreach (var difficultySituation in difficultySetting.DifficultySituations)
            {
                updatedBoard = AddDifficultySitiuationToBoard(updatedBoard, difficultySituation);
            }
            return updatedBoard;
        }

        internal int[,] AddDifficultySitiuationToBoard(int[,] board, DifficultySituation difficultySituation)
        {
            var updatedBoard = board;

            //go through the board and generete this difficulty situation
            for (int i = 0; i < difficultySituation.MinCountofSituation; i++)
            {
                //get a random location from which to remove value
                var randomLocation = GetLocationToRemoveValueFrom();

                //make the location have the provided number of possible values
                updatedBoard = RemoveLocationsWithPossibleValueCount(updatedBoard, randomLocation,
                    difficultySituation.MaxNumberOfPossibleValues);
            }
            return updatedBoard;
        }

        internal int[,] RemoveLocationsWithPossibleValueCount(int[,] board, Location location, int possibleValuCount)
        {
            //random generator
            var randomizer = new Random();

            //create a copy of the board
            var updatedBoard = CommonMethods.CopyBoard(board, boardInfo.BoardSize);

            //get the locations that are connected to the random location
            var connectedLocations = GetConnectedLocations(location);
            updatedBoard[location.Coloumn, location.Row] = 0;


            //get the possible values
            var locationPossibleValue = SudokuBoardSolver.GetPosiiblePlaceValues(location, updatedBoard,
                boardInfo.BoardSize, boardInfo.InnerBoardSize);
            while (locationPossibleValue.Count() < possibleValuCount)
            {
                //get the value to be removed
                var valueToBeRemoved = (int)(randomizer.NextDouble()*(boardInfo.BoardSize - 1))+1;

                //remove the value from the board
                RemoveValueFromBoardAtLocations(updatedBoard, connectedLocations.ColoumnConnections, valueToBeRemoved);
                RemoveValueFromBoardAtLocations(updatedBoard, connectedLocations.RowConnections, valueToBeRemoved);
                RemoveValueFromBoardAtLocations(updatedBoard, connectedLocations.InnerBlockConnections, valueToBeRemoved);

                //get the possible values
                locationPossibleValue = SudokuBoardSolver.GetPosiiblePlaceValues(location, updatedBoard,
                boardInfo.BoardSize, boardInfo.InnerBoardSize);
            }

            //add the connected locations to the used connected locations list
            usedConnectedLocations.AddRange(connectedLocations.ColoumnConnections);
            usedConnectedLocations.AddRange(connectedLocations.RowConnections);
            usedConnectedLocations.AddRange(connectedLocations.InnerBlockConnections);

            return updatedBoard;
        }
        
        internal ConnectedLocations GetConnectedLocations(Location location)
        {
            var connectedLocations = new ConnectedLocations();

            //add the connected columns
            for (int i = 0; i < boardInfo.BoardSize; i++)
            {
                connectedLocations.ColoumnConnections.Add(new Location(i, location.Row));
            }

            //add the connected rows
            for (int i = 0; i < boardInfo.BoardSize; i++)
            {
                connectedLocations.RowConnections.Add(new Location(location.Coloumn, i));
            }

            //add the connected inner board locations
            var innerBlockStartingLocation = new Location
            {
                Coloumn = ((int)(location.Coloumn / boardInfo.InnerBoardSize)) * boardInfo.InnerBoardSize,
                Row = ((int)(location.Row / boardInfo.InnerBoardSize)) * boardInfo.InnerBoardSize,
            };
            for (int i = 0; i < boardInfo.InnerBoardSize; i++)
            {
                for (int j = 0; j < boardInfo.InnerBoardSize; j++)
                {
                    connectedLocations.InnerBlockConnections.Add(new Location(innerBlockStartingLocation.Coloumn + i, innerBlockStartingLocation.Row + j));
                }
            }

            return connectedLocations;
        }

        internal Location GetLocationToRemoveValueFrom()
        {
            var randomRow = 0;
            var randomColoumn = 0;

            //get a randomized location that has not been used
            var randomizer = new Random();
            randomRow = (int)(randomizer.NextDouble() * (boardInfo.BoardSize - 1));
            randomColoumn = (int)(randomizer.NextDouble() * (boardInfo.BoardSize - 1));

            var randomLocationTryCount = 0;
            while (true)
            {
                //check to see if the randomized location is suitable
                if (!unfilledLocations.Any(location => location.Coloumn == randomColoumn && location.Row == randomRow))
                    if (randomLocationTryCount >= (int)(boardInfo.BoardSize / 2) ||
                    !usedConnectedLocations.Any(location => location.Coloumn == randomColoumn && location.Row == randomRow))
                        return new Location(randomColoumn, randomRow);

                //if the picked random location is unsuitable try again and increase the count
                randomRow = (int)(randomizer.NextDouble() * (boardInfo.BoardSize - 1));
                randomColoumn = (int)(randomizer.NextDouble() * (boardInfo.BoardSize - 1));
                randomLocationTryCount++;
            }

        }

        internal void RemoveValueFromBoardAtLocations(int[,] board, List<Location> locations, int value)
        {
            foreach (var location in locations)
            {
                if (board[location.Coloumn, location.Row] == value)
                    board[location.Coloumn, location.Row] = 0;
            }
        }
    }
}
