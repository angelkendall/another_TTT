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
    public enum Symbol
    {
        Empty,
        X,
        O
    }

}