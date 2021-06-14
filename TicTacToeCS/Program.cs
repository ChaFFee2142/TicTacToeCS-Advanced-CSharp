//Volodymyr Boiko 48666

//Jest możliwość wyboru strony
//Jednakowo zważone ruchy wybierają się komputerem losowo, więc jest bardziej podobny do człowieka

using System;


namespace TicTacToeCS
{
    enum CellState
    {
        X,
        O,
        _
    }

    enum PlayerType
    {
        Me,
        AI
    }





    class Board
    {
        //  -------  Board Properties  -----------  //
        public Cell[,] Cells { get; set; }
        public PlayerType CurrentPlayer { get; set; }
        public PlayerType LastPlayer { get; set; }
        public bool[,] EmptyCells { get; set; }
        public int FirstMove { get; set; }
        public Cell LastMove { get; set; }
        public int LastScore { get; set; }
        public string[] phrases { get; set; }


        public Board(int firstMove)                                                      //Main Board constructor
        {
            LastMove = new Cell();
            phrases = new string[] {
            "Get that!",
            "You'll never gonna beat me!",
            "How about that?",
            "Hah!"
            };
            FirstMove = firstMove;
            Cells = new Cell[3, 3];
            EmptyCells = new bool[3, 3];
            if (FirstMove == 1)
            {
                CurrentPlayer = PlayerType.Me;
            }
            else if (firstMove == 2)
            {
                CurrentPlayer = PlayerType.AI;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cells[i, j] = new Cell(i, j);
                    EmptyCells[i, j] = new bool();
                    EmptyCells[i, j] = true;
                }
            }

            DrawBoard();
        }                                                   


        public Board(Board originalObject)                                               //Constructor for Board cloning
        {                                                                                
            Cells = new Cell[3, 3];
            EmptyCells = new bool[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Cells[i, j] = new Cell(originalObject.Cells[i, j]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    EmptyCells[i, j] = originalObject.EmptyCells[i, j];
                }
            }

            CurrentPlayer = originalObject.CurrentPlayer;
            LastPlayer = originalObject.LastPlayer;
            FirstMove = originalObject.FirstMove;
            phrases = originalObject.phrases;
        }                                            
                                                                                         


        public void Step(int x, int y, bool shouldDraw)                                  // Step, automatic player change
        {                                                                                // and drawing the board
            var cell = Cells[x, y];                                                      
            if (FirstMove == 1)
            {
                if (CurrentPlayer == PlayerType.Me)
                {
                    cell.CellState = CellState.X;
                    LastMove = cell;
                    CurrentPlayer = PlayerType.AI;
                    LastPlayer = PlayerType.Me;
                }
                else
                {
                    cell.CellState = CellState.O;
                    LastMove = cell;
                    CurrentPlayer = PlayerType.Me;
                    LastPlayer = PlayerType.AI;
                }
            }
            else if (FirstMove == 2)
            {
                if (CurrentPlayer == PlayerType.Me)
                {
                    cell.CellState = CellState.O;
                    LastMove = cell;
                    CurrentPlayer = PlayerType.AI;
                    LastPlayer = PlayerType.Me;
                }
                else
                {
                    cell.CellState = CellState.X;
                    LastMove = cell;
                    CurrentPlayer = PlayerType.Me;
                    LastPlayer = PlayerType.AI;
                }
            }


            EmptyCells[x, y] = false;

            if (shouldDraw) DrawBoard();
        }

        private void DrawBoard()                                                         // Drawing the Board method
        {                                                                                // AI phrases logic
            string[,] cellStrings = new string[3, 3];                                    
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Cells[i, j].CellState == CellState._)
                    {
                        cellStrings[i, j] = (i).ToString() + (j).ToString();
                    }
                    else
                    {
                        cellStrings[i, j] = Cells[i, j].CellState.ToString() + " ";
                    }
                }
            }

            Console.Clear();
            Console.WriteLine(" ____________________");
            Console.WriteLine("|      |      |      |");
            printColoredBoard("|  " + cellStrings[0, 0] + "  |  " + cellStrings[0, 1] + "  |  " + cellStrings[0, 2] + "  |"); Console.Write("\n");
            Console.WriteLine("|______|______|______|");
            Console.WriteLine("|      |      |      |");
            printColoredBoard("|  " + cellStrings[1, 0] + "  |  " + cellStrings[1, 1] + "  |  " + cellStrings[1, 2] + "  |"); Console.Write("\n");
            Console.WriteLine("|______|______|______|");
            Console.WriteLine("|      |      |      |");
            printColoredBoard("|  " + cellStrings[2, 0] + "  |  " + cellStrings[2, 1] + "  |  " + cellStrings[2, 2] + "  |"); Console.Write("\n");
            Console.WriteLine("|______|______|______|\n");
            Random rand = new Random();
                                                                                         //
            if (LastPlayer == PlayerType.AI)                                             // AI Phrases logic
            {                                                                            //
                if (LastScore > 5)
                {
                    Console.WriteLine("My victory is close!\n");
                }
                else if (LastMove.X == 1 && LastMove.Y == 1)
                {
                    Console.WriteLine("Center is mine!\n");
                }
                else
                {
                    int phraseNum = rand.Next(4);
                    Console.WriteLine(phrases[phraseNum] + "\n");
                }
            }

            void printColoredBoard(string row)
            {
                char[] charRow = row.ToCharArray();
                foreach (char c in charRow)
                {
                    if (c == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(c);
                    }
                    else if (c == 'O')
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

        public int checkWin()
        {
            //first row
            if (Cells[0, 0].CellState == Cells[0, 1].CellState && Cells[0, 1].CellState == Cells[0, 2].CellState
                && Cells[0, 0].CellState != CellState._)
                return 1;

            //second row
            else if ((Cells[1, 0].CellState == Cells[1, 1].CellState) && Cells[1, 1].CellState == Cells[1, 2].CellState
                && Cells[1, 0].CellState != CellState._)
                return 1;

            //third row
            else if (Cells[2, 0].CellState == Cells[2, 1].CellState && Cells[2, 1].CellState == Cells[2, 2].CellState
                && Cells[2, 0].CellState != CellState._)
                return 1;

            //first column
            else if (Cells[0, 0].CellState == Cells[1, 0].CellState && Cells[1, 0].CellState == Cells[2, 0].CellState
                && Cells[0, 0].CellState != CellState._)
                return 1;

            //second column
            else if (Cells[0, 1].CellState == Cells[1, 1].CellState && Cells[1, 1].CellState == Cells[2, 1].CellState
                && Cells[0, 1].CellState != CellState._)
                return 1;

            //third column
            else if (Cells[0, 2].CellState == Cells[1, 2].CellState && Cells[1, 2].CellState == Cells[2, 2].CellState
                && Cells[0, 2].CellState != CellState._)
                return 1;

            //diagonal \
            else if (Cells[0, 0].CellState == Cells[1, 1].CellState && Cells[1, 1].CellState == Cells[2, 2].CellState
                && Cells[0, 0].CellState != CellState._)
                return 1;

            //diagonal /
            else if (Cells[2, 0].CellState == Cells[1, 1].CellState && Cells[1, 1].CellState == Cells[0, 2].CellState
                && Cells[2, 0].CellState != CellState._)
                return 1;

            //draw
            else if (checkDraw())
                return 2;

            else return 0;

        }
        public bool checkDraw()
        {
            int num = 0;
            foreach (var tile in EmptyCells)
            {
                if (tile == false) num++;
            }
            return (num == 9) ? true : false;
        }
    }                                                                                     

    class Cell
    {
        public CellState CellState { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Cell() {    }
        public Cell(int x, int y)
        {
            CellState = CellState._;
            X = x;
            Y = y;
        }
        public Cell(Cell originalCell)                                                   // Constructor for cell cloning
        {
            CellState = originalCell.CellState;
            X = originalCell.X;
            Y = originalCell.Y;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose who moves first: \n{1} - You\n{2} - AI ");
            int firstMove;
            while (!int.TryParse(Console.ReadLine(), out firstMove) || (firstMove != 1 && firstMove != 2))
            {
                Console.WriteLine("Enter {1} or {2}");
            }
            Random rand = new Random();
            Board board = new Board(firstMove);


            int enemyX;
            int enemyY;
            do
            {
                                                                                              
                if (board.CurrentPlayer == PlayerType.Me)                                     // Player's move
                {                                                                             
                    Input(board);

                }
                else                                                                          // AI's move
                {
                    int bestScore = -10000;
                    int score;
                    Cell bestMove = new Cell();
                    Cell move = new Cell();
                    Console.WriteLine("Please wait. AI is thinking...");

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Board minimaxBoard = new Board(board);
                            if (minimaxBoard.EmptyCells[i, j] == true)
                            {
                                move.X = i; move.Y = j;
                                score = minimax(minimaxBoard, isMaximizing: true, move, 1);

                                if (score == bestScore && rand.Next(1, 4) == 2)
                                {
                                    bestScore = score;
                                    bestMove.X = i;
                                    bestMove.Y = j;
                                    board.LastScore = score;
                                }
                                if (score > bestScore)
                                {
                                    bestScore = score;
                                    bestMove.X = i;
                                    bestMove.Y = j;
                                    board.LastScore = score;
                                }
                            }
                        }
                    }

                    board.Step(bestMove.X, bestMove.Y, true);
                }

                if (board.checkWin() == 1)
                {
                    Console.WriteLine("Player {" + (board.CurrentPlayer == PlayerType.Me ? PlayerType.AI : PlayerType.Me) + "} wins! Press any key to restart...");
                    Console.ReadKey();
                    board = new Board(board.FirstMove);
                }
                else if (board.checkWin() == 2)
                {
                    Console.WriteLine("Draw! Press any key to restart...");
                    Console.ReadKey();
                    board = new Board(board.FirstMove);
                }
            } while (true);

            void Input(Board board)
            {
                string xy;
                do
                {
                    do
                    {
                        Console.Write("\nChoose your move: \n");
                        xy = Console.ReadLine();
                    } while (xy != "00" && xy != "01" && xy != "02" &&
                             xy != "10" && xy != "11" && xy != "12" &&
                             xy != "20" && xy != "21" && xy != "22");
                    if (board.EmptyCells[int.Parse(xy[0].ToString()), int.Parse(xy[1].ToString())] == false)
                    {
                        Console.WriteLine("Choose another tile..");
                    }

                } while (board.EmptyCells[int.Parse(xy[0].ToString()), int.Parse(xy[1].ToString())] == false);
                board.Step(int.Parse(xy[0].ToString()), int.Parse(xy[1].ToString()), true);

            }

            int minimax(Board minimaxBoard, bool isMaximizing, Cell move, int depth)
            {
                int minimizer = 100;
                int maximizer = -100;
                minimaxBoard.Step(move.X, move.Y, false);
                int score = 0;
                if (minimaxBoard.checkWin() == 1)
                {
                    if (isMaximizing)
                    {
                        return 1;
                    }
                    else if (!isMaximizing)
                    {
                        return -1;

                    }
                }
                else if (minimaxBoard.checkWin() == 2)
                {
                    return 0;
                }
                else
                {
                    isMaximizing = !isMaximizing;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Board nextMinimaxBoard = new Board(minimaxBoard);

                            if (nextMinimaxBoard.EmptyCells[i, j] == true)
                            {

                                move.X = i; move.Y = j;
                                score = minimax(nextMinimaxBoard, isMaximizing, move, depth++);


                                if (isMaximizing && score > maximizer)
                                {
                                    maximizer = score;
                                }
                                else if (!isMaximizing && score < minimizer)
                                {
                                    minimizer = score;
                                }
                            }
                        }
                    }
                }

                if (isMaximizing)
                {
                    return maximizer;
                }
                else return minimizer;
            }

        }
    }
}


