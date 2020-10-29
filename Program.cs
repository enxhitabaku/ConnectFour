using System;

namespace ConnectFour
{
    class Program
    {

        private const int col = 7;
        private const int row = 6;

        private static string[,] board = new string[col, row];
        private static bool[] colCompleted = new bool[col];

        private static string userTurn;
        private static int choosenCol = 0;
        private static bool win=false;

        static void Main(string[] args)
        {
            Console.WriteLine("          Connect Four" +
                              "\nPlayer One is 'X'" +
                              "\nPlayer Two is 'O'" +
                              "\nPress Enter To Start");
            Console.ReadLine();

            initializeBoard();
            displayBoard();
            firstToPlay();
            play();
        }

        private static void initializeBoard()
        {
            for(int i = 0; i < col; i++)
            {
                colCompleted[i] = false; //Initialize all values to False
               
                for(int j=0;j< row; j++)
                {
                    board[i, j] = " # "; //Set all board values to '#' 
                }
            }
        }

        private static void displayBoard()
        {
            Console.WriteLine("\n\nBoard:"+
                        "\n   C1|C2|C3|C4|C5|C6|C7"+
                        "\n   ---------------------");
            for(int i = 0; i < row; i++)
            {
                Console.Write("R"+(i+1)+"|");//Display row index.Ex: R1| R2| ...

                for(int j = 0; j < col; j++)
                {
                    Console.Write(board[j, i]);//Values to be changed dinamically
                }
                Console.WriteLine();
            }
        }

        private static int userInputToBoardIndex(int input)
        {//Translate the user input [1-8] to the board index [0-7]
            return input-1;
        }

        private static void firstToPlay()
        {
            Random random = new Random();
            int randomFirstPlayer = random.Next(0, 2); //Posssible Values [0-1]

            switch (randomFirstPlayer)
            {
                case 0:
                    userTurn = " O ";
                    Console.WriteLine("'O' Will Start...");
                    break;
                case 1:
                    userTurn = " X ";
                    Console.WriteLine("'X' Will Start...");
                    break;
                default:
                    Console.WriteLine("ERROR OCCURED !");
                    break;
            }

        }

        private static void play()
        {
            do
            {
                if (userTurn == " X ")
                    Console.Write("\nX :  ");
                else
                    Console.Write("\nO :  ");

                try
                {
                    int temp = Int32.Parse(Console.ReadLine());
                    choosenCol = userInputToBoardIndex(temp);
                    

                    if ((choosenCol >= 0 && choosenCol <= col))//Proper Range 
                    {
                        placeIt(choosenCol, userTurn);//Place the token
                       
                        if (winCase(userTurn))
                        {
                            Console.WriteLine("Player '{0}' W O N !", userTurn);
                            playAgain();
                        }
                        else if (!colCompleted[choosenCol]) //Switch User Turn columns are not full 
                        {
                            userTurn = (userTurn == " O " ? " X " : " O ");
                        }
                        
                        if (boardFull())
                        {
                            Console.Write("No More Room For Tokens...\nTIE!");
                            playAgain();
                        }
                        else
                        {
                            if (!win)
                            {
                                Console.Write("Enter a column index to place the token [1-7]");
                            }   
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An Exception has been thrown !\n" + e.Message);
                }

            } while (!win);

        }//End of play()

        private static void placeIt(int colChoosed, string turn)
        {
            int rowPointer = row - 1;//Synced with the array index

            String c = board[colChoosed, rowPointer];

            while ((c == " O " || c == " X ") && rowPointer >= 0)//Check if slot is taken or column is full 
            {
                rowPointer--;//Moving to the next Row (Up) 

                if (rowPointer >= 0)//Check every Row of the Column for a free slot
                    c = board[colChoosed, rowPointer]; //Reset c value

            }//END of While
            if (rowPointer < 0)
                colCompleted[colChoosed] = true; //That column is FULL

            if(!colCompleted[colChoosed]) //If Column in the given index has an Empty Slot 
            {
                board[colChoosed, rowPointer] = turn; //Place token into the empty slot
                displayBoard();
            }
            else
            {
                Console.WriteLine("Column is FULL ! Try Again...");
            } 
            
        }//END of placeIt()

        private static bool boardFull()
        {
            bool full = true;//Initialize to true

            for(int i = 0; i < col; i++)
            {
                if (!colCompleted[i])
                    full = false;//If a single value is false that means the board has still an empty slot
            }

            if (full)//if no empty slots
                displayBoard();

            return full;
        }

        private static bool winCase(string turn)
        {
            bool win = false;

            //Vertical Check
            for(int i = 0; i < col; i++)
            {
                for(int j=0;j<row-3; j++)
                {
                    if (board[i,j]==turn && board[i, j+1] == turn && board[i, j+2] == turn && board[i, j+2] == turn)
                    {
                        Console.WriteLine("\nVertical W I N !");
                        win = true;
                    }
                }
            }

            //Horizontal Check
            for (int i = 0; i < col-3; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (board[i, j] == turn 
                        && board[i+1, j] == turn 
                        && board[i+2, j] == turn 
                        && board[i+3, j] == turn)
                    {
                        Console.WriteLine("\nHorizontal W I N !");
                        win = true;
                    }
                }
            }

            //Verticaly Downward Check
            for (int i = 0; i < col - 3; i++)
            {
                for (int j = 0; j < row-3; j++)
                {
                    if (board[i, j] == turn
                            && board[i+1, j + 1] == turn
                                && board[i+2, j + 2] == turn
                                    && board[i+3, j + 3] == turn)
                    {
                        Console.WriteLine("\nVerticaly Downward W I N !");
                        win = true;
                    }
                }
            }

            //Verticaly Upward Check
            for (int i = 0; i < col - 3; i++)
            {
                for (int j = row-1; j > 3; j--)
                {
                    if (board[i, j] == turn
                            && board[i + 1, j - 1] == turn
                                && board[i + 2, j - 2] == turn
                                    && board[i + 3, j - 3] == turn)
                    {
                        Console.WriteLine("\nVerticaly Upward W I N !");
                        win = true;
                    }
                }
            }

            return win;
        }

        private static void playAgain()
        {
            Console.Write("\nPlay Again ?  (Y/N)");

            string choice = Console.ReadLine().ToUpper();
            Console.WriteLine("\n");//Double new Line

            if (choice == "Y")
            {
                initializeBoard();
                firstToPlay();
                play();

                win = false; //reset value
            }
            else
                win = true;
        }
        
    }
}
