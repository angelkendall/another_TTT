namespace TicTacToe
{
    internal class Player(Symbol symbol)
    {
        public Symbol Symbol { get; set; } = symbol;
        public (int, int) Move { get; private set; }

        // I don't know if it is better to pass in the board
        // or to make the required GameBoard methods 'static' so they can
        // be accessed uisng the class? 

        // Static just means they belong to the class as a whole and not a particular instance
        // Given that there isn't much inheritance going on in this project it doesn't matter so much.

        // Should this be split up into a "get move" and "validate move"? 
        // I feel like validating if the move is in range can be done just by passing in the board size 
        // in the Game class and could be done here, but validating whether the square is empty needs the board
        // itself ...

        // Yes you could. Personally I'd have h 
        public void GetPlayerMove(GameBoard board)
        {
            int row, col;
            int maxRange = board.BoardSize;

            while (true)
            {
                Console.Write($"Enter row and column number (1-{maxRange}), separated by a comma): ");

                // I actually don't really properly understand null checking yet 
                // This is sort of a guess but I'm pretty sure there's better ways

                // It's generally pretty unlikely a user will enter null -> the most common time you will get null reference
                // exceptions is when a class hasn't been instantiated or the return type from another method is null.
                // Either way it's good practice to check.

                // The squiggly line warning is just C# warnings that the value could be null
                // If you to do the .csproj file you will see a <Nullable> property which you could set to 'disable' if you wanted to.
                string[]? input = Console.ReadLine()?.Split(',');

                // If the input were null here then we would never enter this check
                // our program would crash and we woudn't know why.

                if (input != null)
                {
                    try
                    {
                        row = int.Parse(input[0]) - 1;
                        col = int.Parse(input[1]) - 1;

                        if (!board.CheckPositionInRange(row, col))
                            throw new ArgumentException($"Invalid input. Row and column numbers must be between 0 and {maxRange}.");

                        if (!board.CheckPositionEmpty(row, col))
                            throw new ArgumentException("Invalid input. Square not empty.");

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }

            Move = (row, col);
        }

    }

}