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

            Console.WriteLine("-- Bienvenue dans le morpion de Muska ! --    ");
            Console.WriteLine("La grille sera composee de 3 lignes et 3 colonnes:\n");
     
            Console.WriteLine("-Choisissez la position de votre \'o\'-");

            //Game loop
            do
            {
                game.dispGrid(grid);

                game.playerPlays(grid, x, y);
                if (game.playerWin(grid))
                    finish = true;

                game.robotPlays(grid);
                if (game.robotWin(grid))
                    finish = true;

            } while (!finish);

            game.dispGrid(grid);
            Console.WriteLine("Merci d'avoir joue.");
        }

        class Game
        {
            public void dispGrid(char[][] grid)
            {
                for (int i = 0; i < grid.Length; i++) {

                    //System.Console.Write("Element({0}): ", i);

                    for (int j = 0; j < grid[i].Length; j++) {
                        System.Console.Write("{0}{1}", grid[i][j], j == (grid[i].Length - 1) ? "" : " ");
                    }
                    System.Console.WriteLine();
                }

                Console.WriteLine();
            }

            //Tour du joueur
            public void playerPlays(char[][] grid, int x, int y)
            {
                //Entrée utilisateur en string, convertie en int
                string answer = "";
                bool ticPlaced = false;

                do
                {
                    //Choix de la ligne
                    do
                    {
                        Console.WriteLine("Ligne ?");
                        answer = Console.ReadLine();
                        Int32.TryParse(answer, out x); //Conversion du numéro de la ligne en int pour les conditions
                    } while (x < 1 || x > 3); //Répète tant que la ligne est inexistante

                    //Choix de la colonne
                    do
                    {
                        Console.WriteLine("Colonne ?");
                        answer = Console.ReadLine();
                        Int32.TryParse(answer, out y); //Conversion du numéro de la ligne en int pour les conditions
                    } while (y < 1 || y > 3); //Répète tant que la ligne est inexistante

                    if (noSpace(grid, x - 1, y - 1))
                    {
                        Console.WriteLine("Pas de place sur cette case.");
                        ticPlaced = false;
                    }
                    else
                    {
                        ticPlaced = true;
                        grid[x - 1][y - 1] = 'o'; //Remplissage de la case
                    }

                } while (!ticPlaced);

                Console.WriteLine();
            }

            //Fonction de vérification de l'espace - True: plus de place | False: place disponible
            public bool noSpace(char[][] grid, int x, int y)
            {
                bool noSpace = false; //Booléen de retour

                //Si la case donnée en paramètre est déjà remplie (par x ou o)
                if (grid[x][y] == 'o' || grid[x][y] == 'x') {
                    noSpace = true; //Retourne qu'il n'y a plus de place (actions en conséquence...)
                } else { noSpace = false; } 

                return noSpace;
            }

            public bool playerWin(char[][] grid)
            {
                bool playerWin = false;

                if (   (grid[0][0] == 'o' && grid[0][1] == 'o' && grid[0][2] == 'o')//Ligne 0
                    || (grid[1][0] == 'o' && grid[1][1] == 'o' && grid[1][2] == 'o')//Ligne 1
                    || (grid[2][0] == 'o' && grid[2][1] == 'o' && grid[2][2] == 'o')//Ligne 2

                    || (grid[0][0] == 'o' && grid[1][1] == 'o' && grid[2][2] == 'o')//Diagonale 0,0
                    || (grid[2][0] == 'o' && grid[1][1] == 'o' && grid[0][2] == 'o')//Diagonale 2,0

                    || (grid[0][0] == 'o' && grid[1][0] == 'o' && grid[2][0] == 'o')//Colonne 0
                    || (grid[0][1] == 'o' && grid[1][1] == 'o' && grid[2][1] == 'o')//Colonne 1
                    || (grid[0][2] == 'o' && grid[1][2] == 'o' && grid[2][2] == 'o')   ) //Colonne 2
                {
                    Console.WriteLine("Vous avez gagne !");
                    playerWin = true;
                } else
                {
                    playerWin = false;
                }

                return playerWin;
            }
            public bool robotWin(char[][] grid)
            {
                bool robotWin = false;

                if ((grid[0][0] == 'x' && grid[0][1] == 'x' && grid[0][2] == 'x')//Ligne 0
                    || (grid[1][0] == 'x' && grid[1][1] == 'x' && grid[1][2] == 'x')//Ligne 1
                    || (grid[2][0] == 'x' && grid[2][1] == 'x' && grid[2][2] == 'x')//Ligne 2

                    || (grid[0][0] == 'x' && grid[1][1] == 'x' && grid[2][2] == 'x')//Diagonale 0,0
                    || (grid[2][0] == 'x' && grid[1][1] == 'x' && grid[0][2] == 'x')//Diagonale 2,0

                    || (grid[0][0] == 'x' && grid[1][0] == 'x' && grid[2][0] == 'x')
                    || (grid[0][1] == 'x' && grid[1][1] == 'x' && grid[2][1] == 'x')
                    || (grid[0][2] == 'x' && grid[1][2] == 'x' && grid[2][2] == 'x'))
                {
                    Console.WriteLine("Vous avez perdu !");
                    robotWin = true;
                }
                else
                {
                    robotWin = false;
                }

                return robotWin;
            }

            //Tour du robot. (AI)
            public void robotPlays(char[][] grid)
            {
                Random random = new Random();
                int row = 0, column = 0; //Pour le placement de la croix du robot
                bool crossPlaced = false; 

                do
                {
                    //Génère des aléatoires entre 0 et 3 (lignes/colonnes)
                    row = random.Next(0, 3);
                    column = random.Next(0, 3);


                    if(FillRobot(grid, row, column, 'x') == true)
                    {
                        crossPlaced = true;
                    }

                } while (crossPlaced == false);
            }

            public bool FillRobot(char[][] grid, int row, int column, char tictoe) //Tictoe utilisé pour remplir la case
            {
                bool filled = false;
                if (grid[row][column] == 'x' || grid[row][column] == 'o')
                {
                    //Console.WriteLine("There already is an element here.");
                    filled = false;
                }
                else
                {
                    grid[row][column] = tictoe;
                    //Console.WriteLine("Case filled");
                    filled = true;
                }

                return filled;
            }
        }

    }


}
