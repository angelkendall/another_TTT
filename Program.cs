namespace TicTacToe
{
    class Program
    {
        static void Main()
        {
            Game game = new();
            game.Run();
        }
    }
    // I'd move the Enum to the class where it's being implmented
    public enum Symbol
    {
        Empty,
        X,
        O
    }
}