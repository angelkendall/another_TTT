namespace TicTacToe;

// Internal is the default access modifier in most projects
// It's better to have public. Internal just means the class can only be referenced
// within this namespace TicTacToe. You can also use scoped namespace (no curly braces).
// Just makes things a bit nicer to read/look at.
public class Game
{
    readonly Player player1 = new(Symbol.X);
    readonly Player player2 = new(Symbol.O);

    public void Run()
    {
        int userChoice;

        while (true)
        {
            Console.WriteLine("TIC-TAC-TOE");
            Console.WriteLine("\n[1] Play a single game with a 3x3 Board \n[2] Setup custom game\n");
            Console.Write("Select an option: ");
            string? userInput = Console.ReadLine();

            if (userInput != null)
            {
                try
                {
                    userChoice = int.Parse(userInput);

                    if (userChoice < 1 || userChoice > 2)
                        throw new ArgumentException("Select either 1 or 2.");

                    break;
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        switch (userChoice)
        {
            case 1:
                MainGame(new GameBoard(), 1);
                break;
            case 2:
                (int customBoardSize, int customNumberOfGames) = CustomGameSetup();
                // int customNumberOfRounds = CustomGameSetup().Item2;
                MainGame(new GameBoard(customBoardSize), customNumberOfGames);
                break;
            default:
                Console.WriteLine("Something went wrong.");
                break;
        }
    }

    private (int, int) CustomGameSetup()
    {
        int size = GetUserInput("Enter the board size (e.g., 3 for a 3x3 board):");
        int games = GetUserInput("Enter the number of games you would like to play:");

        return (size, games);
    }

    private int GetUserInput(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message + ' ');
            string? input = Console.ReadLine(); 

            if (input != null)
            {
                try
                {
                    result = int.Parse(input);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        return result;
    }

    // Don't really like how large this is but maybe it is ok? 
    // Looks to be the main game loop? It's perfectly fine that its large
    // So long as it only has one repsonsibility.
    private void MainGame(GameBoard board, int round)
    {
        int currentGame= 0;

        int totalPlayer1Wins = 0;
        int totalPlayer2Wins = 0;
        // While loop or for loop?
        do
        {
            // Reset board at the start or the end of the loop ...?
            board.ResetBoard();

            Player currentPlayer = player1;
            int totalMoves = board.TotalSquares;
            int currentMoves = 0;

            if (round > 1)
                Console.WriteLine($"\nGame {currentGame + 1} of {round}\n");

            // While loop or for loop? Originally had while loop, but then 
            // changed to for loop because... not sure why, actually

            // For loops are great when you want to iterate over a certain number of elements/collection
            // You could have opted for a while loop here and maybe boolean for gameOver?
            // eg while (!gameOver){ do something... }
            // The way I think about this is kind of like if I'm reading this what does totalMoves mean to me?
            // Where as if I read while not game over - ok cool then I sort of know this is my main game loop.
            for (int i = 0; i < totalMoves; i++)
            {
                Console.WriteLine();
                board.DisplayBoard();

                Console.WriteLine($"It is {currentPlayer.Symbol}'s turn.");

                currentPlayer.GetPlayerMove(board);

                board.UpdateBoard(currentPlayer.Move.Item1, currentPlayer.Move.Item2, currentPlayer);

                if (CheckForWin(board, currentPlayer))
                {
                    if (currentPlayer == player1)
                        totalPlayer1Wins++;
                    else
                        totalPlayer2Wins++;

                    board.DisplayBoard();
                    Console.WriteLine($"{currentPlayer.Symbol} wins");
                    break;
                }

                currentPlayer = currentPlayer == player1 ? player2 : player1;
                currentMoves++;
            }

            if (totalMoves == currentMoves)
                Console.WriteLine("The game was a draw.");

            currentGame++;

        } while (currentGame < round);


        if (round > 1)
        {
            Console.WriteLine("\nTotal Wins:");
            Console.WriteLine($"{player1.Symbol} won {totalPlayer1Wins} times.");
            Console.WriteLine($"{player2.Symbol} won {totalPlayer2Wins} times.");
        }

    }

    // Unsure whether the best place to put the win-checking conditions is.
    // Considered GameBoard class, from memory other solutions I looked at had it
    // here in Game so I will put it here for this attempt too 

    // In game makes most sense to me

    // Also maybe this should be split up into smaller methods?? 
    // Hmm I think it's fine -> it's checking for a winner
    private bool CheckForWin(GameBoard board, Player player)
    {
        // Rows and Columns
        for (int row = 0; row < board.BoardSize; row++)
        {
            // Reset after each game 
            bool isWinRow = true;
            bool isWinCol = true;

            for (int col = 0; col < board.BoardSize; col++)
            {
                if (board.Board[col, row] != player.Symbol)
                    isWinCol = false;

                if (board.Board[row, col] != player.Symbol)
                    isWinRow = false;
            }

            if (isWinRow || isWinCol)
                return true;
        }

        // Diagonals 
        bool isWinLeftToRightDiagonal = true;
        bool isWinRightToLeftDiagonal = true;
        for (int i = 0; i < board.BoardSize; i++)
        {
            if (board.Board[i, i] != player.Symbol)
                isWinLeftToRightDiagonal = false;

            if (board.Board[i, board.BoardSize - 1 - i] != player.Symbol)
                isWinRightToLeftDiagonal = false;
        }

        if (isWinLeftToRightDiagonal || isWinRightToLeftDiagonal)
            return true;

        return false;
    }
}