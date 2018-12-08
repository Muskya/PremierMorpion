using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuMorpion
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            
            int x = 0, y = 0;
            bool finish = false;
            const int rows = 3, columns = 3;

            char[][] grid = new char[rows][];
            grid[0] = new char[columns] { '-', '-', '-' };
            grid[1] = new char[columns] { '-', '-', '-' };
            grid[2] = new char[columns] { '-', '-', '-' };

            Console.WriteLine("-- Welcome to the Tic-Tac-Toe --\n    ");
            Console.WriteLine("The grid is a 3x3:\n");

            //Game loop
            do
            {
                game.dispGrid(grid);

                game.playerPlays(grid, x, y);
                game.robotPlays(grid);

            } while (!finish);
        }

        class Game
        {
            public void dispGrid(char[][] grid)
            {
                for (int i = 0; i < grid.Length; i++)
                {
                    //System.Console.Write("Element({0}): ", i);

                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        System.Console.Write("{0}{1}", grid[i][j], j == (grid[i].Length - 1) ? "" : " ");
                    }
                    System.Console.WriteLine();
                }

                Console.WriteLine();
            }

            public void playerPlays(char[][] grid, int x, int y)
            {
                string answer = "";

                Console.WriteLine("-Choose your \'o\' position-");

                do {
                    Console.WriteLine("Which line ?");
                    answer = Console.ReadLine();
                    Int32.TryParse(answer, out x);
                } while (x < 1 || x > 3);

                do {
                    Console.WriteLine("Which column ?");
                    answer = Console.ReadLine();
                    Int32.TryParse(answer, out y);
                } while (y < 1 || y > 3);

                grid[x - 1][y - 1] = 'o';

                Console.WriteLine();
            }

            public void robotPlays(char[][] grid)
            {
                Random random = new Random();
                int row = 0, column = 0;
                bool crossPlaced = false;

                do
                {
                    row = random.Next(0, 3);
                    column = random.Next(0, 3);

                    if(Filled(grid, row, column, 'x') == true)
                    {
                        crossPlaced = true;
                    }

                } while (crossPlaced == false);
            }

            public bool Filled(char[][] grid, int row, int column, char ticcross)
            {
                bool filled = false;
                if (grid[row][column] == 'x' || grid[row][column] == 'o')
                {
                    Console.WriteLine("There already is an element here.");
                    filled = false;
                } else
                {
                    grid[row][column] = ticcross;
                    Console.WriteLine("Case filled");
                    filled = true;
                }

                return filled;
            }
        }

    }


}
