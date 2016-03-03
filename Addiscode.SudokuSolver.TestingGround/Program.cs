using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuSolver.Models;

namespace Addiscode.SudokuSolver.TestingGround
{
    class Program
    {
        static void Main(string[] args)
        {
            var sudokuSolver = new SudokuSolver(100);
            //setup the board
            int[,] board = new int[9, 9];
            //board[3, 0] = 1;
            //board[4, 0] = 8;
            //board[8, 0] = 9;
            //board[6, 1] = 4;
            //board[8, 1] = 7;
            //board[2, 2] = 7;
            //board[7, 2] = 2;
            //board[8, 2] = 8;
            //board[5, 3] = 6;
            //board[8, 3] = 5;
            //board[0, 4] = 8;
            //board[1, 4] = 3;
            //board[3, 4] = 9;
            //board[4, 4] = 1;
            //board[5, 4] = 4;
            //board[7, 4] = 7;
            //board[8, 4] = 2;
            //board[0, 5] = 4;
            //board[3, 5] = 5;
            //board[0, 6] = 7;
            //board[1, 6] = 5;
            //board[6, 6] = 2;
            //board[0, 7] = 2;
            //board[2, 7] = 4;
            //board[0, 8] = 3;
            //board[4, 8] = 5;
            //board[5, 8] = 9;
            var solutionBoards = sudokuSolver.Solve(new SudokuProblemDTO {SudokuBoard = board});
        }
    }
}
