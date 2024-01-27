namespace TicTacToe;

public class GameBoard
{
    // I'd actually use the init here because we only want to set the board size when we create the class
    // This would prevent someone from resizing the board from another class which wouldn't be possible anyway
    // because it only has a getter. But still good practice I think.
    public int BoardSize { get; init; }
    public int TotalSquares { get; }
    public Symbol[,] Board { get; }

    // Default board size is 3x3
    public GameBoard() : this(3) { }

    // Custom board size
    public GameBoard(int boardSize)
    {
        BoardSize = boardSize;
        Board = new Symbol[BoardSize, BoardSize];
        TotalSquares = BoardSize * BoardSize;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Board[i, j] = Symbol.Empty;
            }
        }
    }

    // It seems the player has dependancies to the board and the board
    // has dependancy on the player. I'd consider wrapping in such a way that the relationships only go one way?
    // For instance player -> board -> game
    public bool UpdateBoard(int row, int col, Player player)
    {
        // These are being double checked - once in the input and once here 
        // If I remove them then I assume that the row and col will always be a valid move?
        // And if I keep them, they're being double checked?

        // One way to think about this is do we always expect we're getting passed valid data?
        // is row, col, and board always going to be safe? Is there every a time that player could be null
        // or rol/col might be an index not within the array? If the answer is yes, then this is fine to me.

        //if (!(CheckPositionEmpty(row, col)) || !(CheckPositionInRange(row, col)))
        //return false;

        Board[row, col] = player.Symbol;
        return true;
    }

    // Originally had these two position checking methods as private and had a CheckMoveIsValid method that called both and was
    // used in UpdateBoard 
    // but removed it and made these two private so that they could be called in the Player class?

    // The way I think about it is that you don't want methods exposed outside the class unless they're being called
    // by some other class (which is fine) so long as it's loosely coupled.
    public bool CheckPositionEmpty(int row, int col)
    {
        if (Board[row, col] != Symbol.Empty)
            return false;

        return true;
    }

    public bool CheckPositionInRange(int row, int col)
    {
        if (row < 0 || row > BoardSize || col < 0 || col > BoardSize || row == BoardSize || col == BoardSize)
            return false;

        return true;
    }

    // Visual Studio suggests to mark this as static, but is that really necessary? 
    // Static still confuses me, I think 

    // Static members belong to the class itself and not any particular instance
    // I can give you an example which helped me understand it better
    private char ConvertToChar(Symbol symbol) => symbol switch { Symbol.X => 'X', Symbol.O => 'O', Symbol.Empty => ' ', _ => ' ' };

    public void DisplayBoard()
    {
        // This prevents the scrolling/duplicate boards in the console window by clearing the previous
        Console.Clear();

        string rowDividingLine = (string.Concat(Enumerable.Repeat("+---", BoardSize)));

        Console.WriteLine(rowDividingLine + '+');

        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Console.Write($"| {ConvertToChar(Board[i, j])} ");

                if (j + 1 == BoardSize)
                    Console.Write("|");
            }

            Console.Write("\n");
            Console.WriteLine(rowDividingLine + '+');
        }
    }
}
