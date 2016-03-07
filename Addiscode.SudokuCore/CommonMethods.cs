using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuCore.Models;

namespace Addiscode.SudokuCore
{
    public static class CommonMethods
    {

        public static int[,] CopyBoard(int[,] board, int boardSize)
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


        public static  SudokuBoardInfo GetSudokuBoardSize(int[,] board)
        {
            var boardInfo = new SudokuBoardInfo();

            //get the board size
            boardInfo.BoardSize = board.GetLength(0);

            //get the inner board size
            var boardSizeSquerRoot = Math.Sqrt(board.GetLength(0));
            boardInfo.InnerBoardSize = (int)boardSizeSquerRoot;

            //check to see if the board is a proper sudoku board (row number == coloumn number)
            if (board.GetLength(0) != board.GetLength(1) || ((int)(boardSizeSquerRoot * 10) != boardInfo.InnerBoardSize * 10))
                throw new Exception("The board is not a proper sudoku board");

            return boardInfo;
        }
    }
}
