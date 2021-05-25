using System;

namespace TicTacToeCS
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] defaultArr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int player = 1;
            int chosenTile = 0;
            int lastTurn = 1;
            ConsoleColor[] tileColor = {ConsoleColor.White,
                                        ConsoleColor.White, ConsoleColor.White, ConsoleColor.White,
                                        ConsoleColor.White, ConsoleColor.White, ConsoleColor.White,
                                        ConsoleColor.White, ConsoleColor.White, ConsoleColor.White };

            //Console.WriteLine("TicTacToe game");
            do
            {
                Console.Clear();
                defaultArr.CopyTo(arr, 0);
                player = 1;
                drawBoard();

                do
                {
                    if (player == 1)
                    {
                        Console.WriteLine("Player 1: ");
                        chosenTile = int.Parse(Console.ReadLine());
                        while (arr[chosenTile] == 'x' || arr[chosenTile] == 'o')
                        {
                            Console.WriteLine("Choose another tile: ");
                            chosenTile = int.Parse(Console.ReadLine());
                        }
                        arr[chosenTile] = 'x';
                        tileColor[chosenTile] = ConsoleColor.Green;
                        Console.Clear();
                        drawBoard();
                        lastTurn = 1;
                        player = 2;
                    }
                    else
                    {
                        Console.WriteLine("Player 2: ");
                        Random rand = new Random();
                        chosenTile = rand.Next(1, 9);
                        while (arr[chosenTile] == 'x' || arr[chosenTile] == 'o')
                            chosenTile = rand.Next(1, 9);
                        arr[chosenTile] = 'o';
                        tileColor[chosenTile] = ConsoleColor.Red;
                        Console.Clear();
                        drawBoard();
                        lastTurn = 2;
                        player = 1;
                    }


                } while (checkWin() == 0);

                Console.WriteLine("Press any key to restart or \"q\" to leave...");
            }
            while (Console.ReadKey().KeyChar != 'q');







            void drawBoard()
            {
                //string board = "     |     |     \n" +
                //               "  " + arr[1] + "  |  " + arr[2] + "  |  " + arr[3] + "  \n" +
                //               "_____|_____|_____\n" +
                //               "     |     |     \n" +
                //               "  " + arr[4] + "  |  " + arr[5] + "  |  " + arr[6] + "  \n" +
                //               "_____|_____|_____\n" +
                //               "     |     |     \n" +
                //               "  " + arr[7] + "  |  " + arr[8] + "  |  " + arr[9] + "  \n" +
                //               "_____|_____|_____\n";

                Console.WriteLine(" _________________");
                Console.WriteLine("|     |     |     |");
                printColoredBoard("|  " + arr[1] + "  |  " + arr[2] + "  |  " + arr[3] + "  |"); Console.Write("\n");
                Console.WriteLine("|_____|_____|_____|");
                Console.WriteLine("|     |     |     |");
                printColoredBoard("|  " + arr[4] + "  |  " + arr[5] + "  |  " + arr[6] + "  |"); Console.Write("\n");
                Console.WriteLine("|_____|_____|_____|");
                Console.WriteLine("|     |     |     |");
                printColoredBoard("|  " + arr[7] + "  |  " + arr[8] + "  |  " + arr[9] + "  |"); Console.Write("\n");
                Console.WriteLine("|_____|_____|_____|");




                //Console.WriteLine("     |     |      ");

                //Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
                ////Console.ForegroundColor = tileColor[1];
                ////Console.Write("  {0}  ", arr[1]);
                ////Console.ForegroundColor = ConsoleColor.White;
                ////Console.Write('|');
                ////Console.ForegroundColor = tileColor[2];
                ////Console.Write("  {0}  ", arr[2]);
                ////Console.ForegroundColor = ConsoleColor.White;
                ////Console.Write('|');
                ////Console.ForegroundColor = tileColor[3];
                ////Console.Write("  {0}  \n", arr[3]);

                //Console.WriteLine("_____|_____|_____ ");

                //Console.WriteLine("     |     |      ");

                //Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);

                //Console.WriteLine("_____|_____|_____ ");

                //Console.WriteLine("     |     |      ");

                //Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);

                //Console.WriteLine("     |     |      ");

            }

            int checkWin()
            {
                //first row
                if (arr[1] == arr[2] && arr[2] == arr[3])
                    return 1;

                //second row
                else if (arr[4] == arr[5] && arr[5] == arr[6])
                    return 1;

                //third row
                else if (arr[7] == arr[8] && arr[8] == arr[9])
                    return 1;

                //first column
                else if (arr[1] == arr[4] && arr[4] == arr[7])
                    return 1;

                //second column
                else if (arr[2] == arr[5] && arr[5] == arr[8])
                    return 1;

                //third column
                else if (arr[3] == arr[6] && arr[6] == arr[9])
                    return 1;

                //diagonal /
                else if (arr[1] == arr[5] && arr[5] == arr[9])
                    return 1;

                //diagonal \
                else if (arr[3] == arr[5] && arr[5] == arr[7])
                    return 1;

                //draw
                else if (checkDraw())
                    return 2;

                else return 0;

            }


            bool checkDraw()
            {
                int num = 0;
                foreach (var tile in arr)
                {
                    if (tile == 'x' || tile == 'o') num++;
                }
                return (num == 9) ? true : false;
            }


            void printColoredBoard(string row)
            {
                char[] charRow = row.ToCharArray();
                foreach (char c in charRow)
                {
                    if (c == 'x')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                    }
                    else if (c == 'o')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(c);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(c);
                    }
                }
            }

        }
    }
}


