namespace TicTacToe
{
    internal class GameBoard
    {
        public int BoardSize { get; }
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

        public bool UpdateBoard(int row, int col, Player player)
        {
            // These are being double checked - once in the input and once here 
            // If I remove them then I assume that the row and col will always be a valid move?
            // And if I keep them, they're being double checked?

            //if (!(CheckPositionEmpty(row, col)) || !(CheckPositionInRange(row, col)))
            //return false;

            Board[row, col] = player.Symbol;
            return true;
        }

        // Originally had these two position checking methods as private and had a CheckMoveIsValid method that called both and was
        // used in UpdateBoard 
        // but removed it and made these two private so that they could be called in the Player class?
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
        private char ConvertToChar(Symbol symbol) => symbol switch { Symbol.X => 'X', Symbol.O => 'O', Symbol.Empty => ' ', _ => ' ' };

        public void DisplayBoard()
        {
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
}
