namespace blackjack;

class Program
{
    static void Main(string[] args)
    {
        IRunner runner = new Runner();
        IStrategy strategy = new BasicStrategy();

        Game game = new Game(runner, strategy);
        GameManager gameManager = new GameManager(game);
        gameManager.RunSimulation(1000);
        PrintStatistics(gameManager);
    }

    private static void PrintStatistics(IStatistics statistics) {
        Console.WriteLine("Earnings: " + statistics.GetEarnings());
        Console.WriteLine("Earnings avg: " + statistics.GetAverageEarningsPerGame());
        Console.WriteLine("Number of wins: " + statistics.GetVictoryCount());
        Console.WriteLine("Number of losses: " + statistics.GetLossCount());
        Console.WriteLine("Number of draws: " + statistics.GetDrawCount());
        Console.WriteLine("Win ratio: " + statistics.GetWinRatio() + "%");
    }
}
