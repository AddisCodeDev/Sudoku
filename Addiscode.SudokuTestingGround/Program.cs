using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Addiscode.SudokuGenerator;
using Addiscode.SudokuSolver;
using Addiscode.SudokuSolver.Models;

namespace Addiscode.SudokuTestingGround
{
    class Program
    {
        static void Main(string[] args)
        {

            //sudoku solver testing
            var sudokuSolver = new SudokuBoardSolver(100);
            //setup the board
            int[,] board = new int[9, 9];
            board[3, 0] = 1;
            board[4, 0] = 8;
            board[8, 0] = 9;
            board[6, 1] = 4;
            board[8, 1] = 7;
            board[2, 2] = 7;
            board[7, 2] = 2;
            board[8, 2] = 8;
            board[5, 3] = 6;
            board[8, 3] = 5;
            board[0, 4] = 8;
            board[1, 4] = 3;
            board[3, 4] = 9;
            board[4, 4] = 1;
            board[5, 4] = 4;
            board[7, 4] = 7;
            board[8, 4] = 2;
            board[0, 5] = 4;
            board[3, 5] = 5;
            board[0, 6] = 7;
            board[1, 6] = 5;
            board[6, 6] = 2;
            board[0, 7] = 2;
            board[2, 7] = 4;
            board[0, 8] = 3;
            board[4, 8] = 5;
            board[5, 8] = 9;
            var solutionBoards = sudokuSolver.Solve(new SudokuProblemDTO { SudokuBoard = board });

            //Console.WriteLine("Intial Board");
            //Console.WriteLine();
            //PrintBoard(board);
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine("Solved Board");
            //Console.WriteLine();
            //PrintBoard(solutionBoards.SolvedBoards.First());
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();


            //sudoku generator testing
            var solvedBoard = solutionBoards.SolvedBoards.First();
            var sudokuGenerator = new SudokuBoardGenerator();
            var unsolvedBoard = sudokuGenerator.GenerateSudokuBoardWithDifficultySetting(solvedBoard,
                BoardPresetDifficulty_9X9.DifficultySetting);


            Console.WriteLine("Solved Board");
            Console.WriteLine();
            PrintBoard(solvedBoard);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Unsolved Board");
            Console.WriteLine();
            PrintBoard(unsolvedBoard);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();



            Console.ReadLine();
        }

        static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                        Console.Write("   ");
                    else
                        Console.Write("  " + board[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
