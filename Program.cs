//Carson Pond Final Project 7/27/2026
using System.Diagnostics;
using System.Runtime.InteropServices;

RunTests();

//Creating and saving board
char[,] board =
{
    { '1', '2', '3', },
    { '4', '5', '6', } ,
    { '7', '8', '9', }
};


//keeping score "If we're keeping track we might as well win" JayDee Barr
int wins = 0;
int losses = 0;
int ties = 0;
string scoreFile = "score.txt";
if (File.Exists(scoreFile))
{
    string[] lines = File.ReadAllLines(scoreFile);
    if (lines.Length >= 3)
    {
        int.TryParse(lines[0], out wins);
        int.TryParse(lines[1], out losses);
        int.TryParse(lines[2], out ties);
    }
}
Console.Clear();
Console.WriteLine("===========================");
Console.WriteLine("        TIC-TAC-TOE");
Console.WriteLine("===========================");
Console.WriteLine($"Current Record - Wins: {wins} | Losses: {losses} | Ties: {ties}");
Console.WriteLine("Press any key to being.");
Console.ReadKey();

//The big loop(main game loop)
bool keepPlaying = true;
while (keepPlaying)
{
ResetBoard(board);    

bool gameOver = false;
while (!gameOver)
{
    Console.Clear();
    
    PrintBoard(board); //getting board on the screen
    
    PlayerTurn(board); //player takes turn

    if(CheckForWin(board, 'X')) //if player wins
    {
        Console.WriteLine("Thinking...");
        System.Threading.Thread.Sleep(1500);
        Console.Clear();
        PrintBoard(board);
        Console.WriteLine("Congrats!! You win!!");
        wins++; //adding to total wins
        gameOver = true;
        break;
    }
    if (IsBoardFull(board)) //if there is a tie
    {
        Console.WriteLine("Thinking...");
        System.Threading.Thread.Sleep(1500);       
        Console.Clear();
        PrintBoard(board);
        Console.WriteLine("It's a tie, maybe try it again and see what happens.");
        ties++; //adding to total ties
        gameOver = true;
        break;
    }

    Console.WriteLine("Thinking..."); //"Pause for dramatic effect" - Gru
    System.Threading.Thread.Sleep(1500);

    ComputerTurn(board);

    if (CheckForWin(board, 'O')) //did the robot win??
    {
        Console.WriteLine("Thinking...");
        System.Threading.Thread.Sleep(1500);
        Console.Clear();
        PrintBoard(board);
        Console.WriteLine("You lost!! ... really dude? It's tic tac toe, you couldn't at least tie??");
        losses++; //adding to total losses :(
        gameOver = true;
        break;
    }
        if (IsBoardFull(board))
    {
        Console.WriteLine("Thinking...");
        System.Threading.Thread.Sleep(1500);
        Console.Clear();
        PrintBoard(board);
        Console.WriteLine("It's a tie, maybe try it again and see what happens.");
        ties++; //adding to total ties 
        gameOver = true;
        break;
    }

}
//play again? Yay or Nay
Console.Write("Do you wanna play again? (y/n): ");
string response = Console.ReadLine()?.ToLower() ?? "";
if (response != "y" && response != "yes" && response != "yay")
    {
        keepPlaying = false;
        Console.Clear();

        string[] linesToSave = {wins.ToString(), losses.ToString(), ties.ToString()};
        File.WriteAllLines(scoreFile, linesToSave);

        Console.WriteLine($"Total record to date - Wins {wins} | Losses: {losses} | Ties: {ties}");
        Console.WriteLine("Bye!! Thanks for playing :)");
    }

}



// METHODS!!!!


//printing board
static void PrintBoard(char[,] b)
{
    Console.WriteLine();
    Console.WriteLine($" {b[0,0]} | {b[0,1]} | {b[0,2]} ");
    Console.WriteLine("---|---|---");
    Console.WriteLine($" {b[1,0]} | {b[1,1]} | {b[1,2]} ");
    Console.WriteLine("---|---|---");
    Console.WriteLine($" {b[2,0]} | {b[2,1]} | {b[2,2]} ");
    Console.WriteLine();  
}

//player turn
static void PlayerTurn(char[,] b)
{
    bool validMove = false;
    while(!validMove)
    {
        Console.Write("Your turn! Pick an avaible spot 1-9: ");
        string input = Console.ReadLine()?.Trim() ?? "";

        if (input != null && input.Length == 1 && input[0] >= '0' && input[0] <= '9')
        {
            char choice = input[0];
            int row = 0;
            int col = 0;

            switch (choice)
            {
                case '1': row = 0; col = 0; break;
                case '2': row = 0; col = 1; break;
                case '3': row = 0; col = 2; break;
                case '4': row = 1; col = 0; break;
                case '5': row = 1; col = 1; break;
                case '6': row = 1; col = 2; break;
                case '7': row = 2; col = 0; break;
                case '8': row = 2; col = 1; break;
                case '9': row = 2; col = 2; break;
            }

            if (b[row, col] != 'X' && b[row, col] != 'O')
            {
                b[row,col] = 'X';
                validMove = true;
            }
            else
            {
                Console.WriteLine("That spot is already taken! Please try again!");
            }
        }
        else
        {
            Console.WriteLine("Invalid entry! Please input a single number between 1 and 9: ");
        }
    }
}

//COMPUTER!!! make the player lose
static void ComputerTurn(char[,] grid)
{
    if (WinOrBlock(grid, 'O')) return;
    if (WinOrBlock(grid, 'X')) return;

    if (grid[1, 1] == '5')
    {
        grid[1, 1] = 'O';
        return;
    }

    List<(int row, int col)> openSpots = new List<(int row, int col)>();

    for (int r = 0; r < 3; r++)
    {
        for (int c = 0; c < 3; c++)
        {
            if (grid[r, c] != 'X' && grid [r, c] != 'O')
            {
                openSpots.Add((r, c));
            }
        }
    }

    if (openSpots.Count > 0)
    {
        int randomIndex = Random.Shared.Next(openSpots.Count);
        var (row, col) = openSpots[randomIndex];
        grid[row, col] = 'O';
    }

}

//More computer strategy, for the win or lack thereof
static bool WinOrBlock(char[,] grid, char symbol)
{
    for (int r = 0; r < 3; r++)
    {
        for (int c = 0; c < 3; c++)
        {
            if (grid [r, c] != 'X' && grid[r, c] != 'O')
            {
                char temp = grid[r, c];
                grid[r, c] = symbol;

                if (CheckForWin(grid, symbol))
                {
                    grid[r, c] = 'O';
                    return true;
                }

                grid[r,c] = temp;
            }
        }
    }
    return false;
}

//did I win?
static bool CheckForWin(char[,] b, char player)
{
    if (b[0,0] == player && b[0,1] == player && b[0,2] == player) return true;
    if (b[1,0] == player && b[1,1] == player && b[1,2] == player) return true;
    if (b[2,0] == player && b[2,1] == player && b[2,2] == player) return true;
    if (b[0,0] == player && b[1,0] == player && b[2,0] == player) return true;
    if (b[0,1] == player && b[1,1] == player && b[2,1] == player) return true;
    if (b[0,2] == player && b[1,2] == player && b[2,2] == player) return true;
    if (b[0,0] == player && b[1,1] == player && b[2,2] == player) return true;
    if (b[0,2] == player && b[1,1] == player && b[2,0] == player) return true;

    return false;
}


//tie??
static bool IsBoardFull(char[,] b)
{
    foreach (char cell in b)
    {
        if (cell >= '1' && cell <= '9')
        {
            return false;
        }
    }
    return true;
}

//Reset
static void ResetBoard(char[,] b)
{
    char number = '1';
    for (int r = 0; r< 3; r++)
    {
        for (int c = 0; c < 3; c++)
        {
            b[r, c] = number++;
        }
    }
}

//TESTS
static void RunTests()
{
    // Test 1: Horizontal Win for X
    char[,] horizontalWin = {
        { 'X', 'X', 'X' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };
    Debug.Assert(CheckForWin(horizontalWin, 'X') == true, "Test 1 Failed: Horizontal win not picked up.");

    // Test 2: Diagonal Win for the big O
    char[,] diagonalWin = {
        { 'O', '2', '3' },
        { '4', 'O', '6' },
        { '7', '8', 'O' }
    };
    Debug.Assert(CheckForWin(diagonalWin, 'O') == true, "Test 2 Failed: Diagonal win not picked up.");

    // Test 3: Empty board, no win yet
    char[,] emptyBoard = {
        { '1', '2', '3' },
        { '4', '5', '6' },
        { '7', '8', '9' }
    };
    Debug.Assert(CheckForWin(emptyBoard, 'X') == false, "Test 3 Failed: false win declared on empty board.");
    

    // Test 4: Full board, tie ballgame
    char[,] fullBoard = {
        { 'X', 'O', 'X' },
        { 'X', 'O', 'O' },
        { 'O', 'X', 'X' }
    };
    Debug.Assert(IsBoardFull(fullBoard) == true, "Test 4 Failed: Didn't pick up full board.");

    // Test 5: Partly full board, tie ballgame
    char[,] halfFullBoard = {
        { 'X', 'O', 'X' },
        { 'X', '5', 'O' },
        { 'O', 'X', 'X' }
    };
    Debug.Assert(IsBoardFull(halfFullBoard) == false, "Test 5 Failed: Inaccurately picked up full board.");
   
}