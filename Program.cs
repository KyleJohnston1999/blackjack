namespace blackjack;

class Program
{
    static void Main(string[] args)
    {
        IRunner runner = new Runner();
        IStrategy strategy = new Strategy();

        Game game = new Game(runner, strategy);

        Result result = game.Play();

        Console.WriteLine(result);

    }
}
