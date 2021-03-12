using System;
using System.Collections.Generic;
using System.Linq;
using Xerris_Battleship.Enumerators;
using Xerris_Battleship.Helpers;
using Xerris_Battleship.Model;

namespace Xerris_Battleship
{
    class Program
    {
        public static GameLevel level;
        static void Main(string[] args)
        {
            bool stop = false;
            while (!stop)
            {
                StartGame();
                Console.WriteLine("PRESS ANY KEY TO PLAY IT AGAIN OR ESC TO CLOSE");
                var key = Console.ReadKey();
                stop = 27 == (int)key.KeyChar;
            }
        }

        private static void StartGame()
        {
            Console.Clear();

            PrintHeader();

            LevelSelection();

            Console.Clear();

            PrintHeader();

            Ship shipOne = CreatePlayer("PLAYER 1");

            Console.Clear();

            Ship shipTwo = CreatePlayer("PLAYER 2");

            level = GameLevel.Easy;

            while (!shipOne.IsSunk() && !shipTwo.IsSunk())
            {

                var shot = string.Empty;
                while (!shot.IsValiShot())
                {
                    Console.Clear();
                    var source = shipOne.Shots.Count > shipTwo.Shots.Count ? shipTwo : shipOne;
                    var target = shipOne.Shots.Count > shipTwo.Shots.Count ? shipOne : shipTwo;
                    PrintBoard(source);
                    Console.WriteLine("\n\n\n\nENTER A SHOT POSITION:");
                    shot = Console.ReadLine();
                    if (shot.IsValiShot())
                    {


                        var positionShot = new Position(int.Parse(shot[1].ToString()), shot[0]);

                        if (source.IsReShot(positionShot))
                        {
                            Console.WriteLine("YOU ALREADY TRIED IT");
                            Console.WriteLine("PRESS ANY KEY TO TRY IT AGAIN");

                        }
                        else
                        {
                            if (target.CheckHit(positionShot))
                            {
                                source.AddTry(positionShot, true);
                                Console.WriteLine("HIT!!!");
                            }
                            else
                            {
                                source.AddTry(positionShot, false);
                                Console.WriteLine("MISS!!");
                            }

                            Console.WriteLine("PRESS ANY KEY TO CONTINUE");
                        }

                    }
                    else
                    {
                        Console.WriteLine("INVALID SHOT!!! TRY IT AGAIN!!");
                    }

                    Console.ReadKey();

                }
            }

            Console.Clear();
            var winner = shipOne.IsSunk() ? shipTwo : shipOne;
            Console.WriteLine($"{winner.PlayerName} IS THE WINNER !!!");
            Console.WriteLine($"{winner.PlayerName} YOU SUNK MY BATTLESHIP!!!");
        }

        private static Ship CreatePlayer(string playerName)
        {

            Ship ship;
            List<Position> positions;
            string position = string.Empty;
            do
            {
                ship = null;
                positions = new List<Position>();
                Console.Clear();
                PrintHeader();
                if (!string.IsNullOrEmpty(position) || (ship != null  &&  !ship.IsValid()))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t*Position Invalid!!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine($"\t\t{playerName} please enter your ship position:");
                Console.WriteLine("\t\t**\tVertical   i.e. A1-B1-C1");
                Console.WriteLine("\t\t**\tHorizontal i.e. A1-A2-A3");
               
                position = Console.ReadLine();
                if (!position.IsValidPosition())
                    continue;

                position.Split('-').ToList().ForEach(f => positions.Add(new Position(int.Parse(f[1].ToString()), f[0])));

                ship = new Ship(playerName, positions, level);


            } while (ship == null || !ship.IsValid());

            return ship;
        }

        private static void PrintBoard(Ship ship)
        {
            PrintHeader();

            Console.Write($"\t\t\t\t{ship.PlayerName}");
            Console.Write($"\n\t\t\tTRIES {ship.Shots.Count}\t\tON TARGET {ship.OnTarget.Count}");

            for (int i = 0; i <= 7; i++)
            {
                Console.WriteLine(Environment.NewLine);
                for (int j = 0; j <= 9; j++)
                {
                    if (i == 0 && j > 0)
                    {
                        Console.Write(" \t" + (char)(64 + j));
                    }
                    else if (j == 0 && i > 0)
                    {
                        Console.Write($"{i}");
                    }

                    if (ship.OnTarget.Any(a => a.Horizontal.Equals((char)(64 + j)) && a.Vertical == i))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\tO");
                    }
                    else if (ship.Shots.Any(a => a.Horizontal.Equals((char)(64 + j)) && a.Vertical == i))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\tX");
                    }
                    else if (i > 0 && j > 0)
                        Console.Write("\t_");

                    Console.ForegroundColor = ConsoleColor.White;

                }

            }
        }

        static void PrintHeader()
        {
            Console.WriteLine("***************************** BATTLESHIP *****************************\n\n");
        }

        static void LevelSelection()
        {
            ConsoleKeyInfo? userInput = null;
            do
            {
                Console.Clear();
                PrintHeader();
                if (userInput.HasValue)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t*Please enter a valid option");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("\t\tPlease enter a game level\n");
                Console.WriteLine("\t\t[1] - Easy (ship will be sunk with only one hit)");
                Console.WriteLine("\t\t[2] - Medium (ship will be sunk with two hits)");
                Console.WriteLine("\t\t[3] - Hard (ship will be sunk with three hits)");

               
                userInput = Console.ReadKey();

            } while (!char.IsDigit(userInput.Value.KeyChar) || int.Parse(userInput.Value.KeyChar.ToString()) <= 0 || int.Parse(userInput.Value.KeyChar.ToString()) > 3);

            level = (GameLevel)int.Parse(userInput.Value.KeyChar.ToString());
        }
    }
}
