﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_6
{
	public enum Mark { None, O, X };

	public class OxoBoard
	{

        public int width;
        public int height;

        public Mark[,] board;

        public string[] wins;

		// Constructor. Perform any necessary data initialisation here.
		// Uncomment the optional parameters if attempting the stretch goal -- keep the default values to avoid breaking unit tests.
		public OxoBoard(/* int width = 3, int height = 3, int inARow = 3 */)
		{
            width = 3;   //Set width and height of board, with a default value of 3x3
            height = 3;  //
            board = new Mark[width, height]; //Initialise the board

            wins = GenerateWins(3);

            // Set all the board spaces to None
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    board[i, j] = Mark.None;
                }
            }
		}

        public OxoBoard(int size)
        {
            width = height = size;
            board = new Mark[width, height]; //Initialise the board

            wins = GenerateWins(size);

            // Set all the board spaces to None
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    board[i, j] = Mark.None;
                }
            }
        }

        public string[] GenerateWins(int size)
        {//generate all the win states. As this board is square, they will be horizontal (1 per line), vertical (1 per column) and 2 diagonals
            List<string> ret = new List<string>();
            for (int i = 0; i < size; i++)
            {
                string row = "";
                for (int j = 0; j < size; j++)
                {
                    row += i.ToString() + j;
                }
                ret.Add(row);
            }

            for (int i = 0; i < size; i++)
            {
                string column = "";
                for (int j = 0; j < size; j++)
                {
                    column += j.ToString() + i;
                }
                ret.Add(column);
            }

            string diag1 = "";
            for (int i = 0; i < size; i++)
            {
                diag1 += i.ToString() + i;
            }
            ret.Add(diag1);

            string diag2 = "";
            for (int i = 0; i < size; i++)
            {
                diag2 += i.ToString() + (size-i-1);
            }
            ret.Add(diag2);

            return ret.ToArray();
        }

		// Return the contents of the specified square.
		public Mark GetSquare(int x, int y)
		{
            return board[x, y];
		}

		// If the specified square is currently empty, fill it with mark and return true.
		// If the square is not empty, leave it as-is and return False.
		public bool SetSquare(int x, int y, Mark mark)
		{
            if(board[x, y] == Mark.None)
            {
                board[x, y] = mark;
                return true;
            }
            else
            {
                return false;
            }
            
		}

		// If there are still empty squares on the board, return false.
		// If there are no empty squares, return true.
		public bool IsBoardFull()
		{
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (board[i,j] == Mark.None)
                    {
                        return false; //if any of them are none, then the board must not be full
                    }
                }
            }
            return true; //if we are still executing, we havent found an empty square, so it must be full
        }

		// If a player has three in a row, return Mark.O or Mark.X depending on which player.
		// Otherwise, return Mark.None.
		public Mark GetWinner()
		{//contains a list of coordinates for every possible win state, for example, "001020" means (0,0),(1,0),(2,0)            
            foreach (string winState in wins)
            {//if all the needed places are the same, and one of them isnt none, then someone must have won, so we can just return the first square
                if (board[winState[0] - '0', winState[1] - '0'] == board[winState[2] - '0', winState[3] - '0'] && board[winState[2] - '0', winState[3] - '0']  == board[winState[4] - '0', winState[5] - '0'] && board[winState[4] - '0', winState[5] - '0'] != Mark.None)
                {
                    return board[winState[0] - '0', winState[1] - '0'];
                }

            }//if we havent retured yet, nobody has won, so return none
            return Mark.None;
		}

		// Display the current board state in the terminal. You should only need to edit this if you are attempting the stretch goal.
		public void PrintBoard()
		{
			for (int y = 0; y < height; y++)
			{
                if (y > 0)// Replaced characters with proper box-drawing characters (https://en.wikipedia.org/wiki/Box-drawing_character). These are brilliant, I recommend using them a lot
                {
                    Console.Write("─");
                    for (int x = 0; x < width - 1; x++)
                    {
                        Console.Write("─┼──");
                    }
                    Console.WriteLine("");
                }
                   

				for (int x = 0; x < width; x++)
				{
					if (x > 0)
						Console.Write(" │ ");

					switch (GetSquare(x, y))
					{
						case Mark.None:
							Console.Write(" "); break;
						case Mark.O:
							Console.Write("O"); break;
						case Mark.X:
							Console.Write("X"); break;
					}
				}

				Console.WriteLine();
			}
		}
	}
}

