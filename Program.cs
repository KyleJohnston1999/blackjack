namespace blackjack;

class Program
{
    static readonly string[] Strategies = new []
    {
        "Basic",
        "Semi",
        "Table",
        "All",
        "End"
    };
    static void Main(string[] args)
    {
        MenuSelection();
    }

    static void MenuSelection() {
        string Strategy = ProvideMenu();
        switch (Strategy) {
            case "Basic":
                StartGame(new List<IStrategy>() { new BasicStrategy() } );
                break;
            case "Semi":
                StartGame(new List<IStrategy>() { new SemiStrategy() });
                break;
            case "Table":
                StartGame(new List<IStrategy>() { new TableStrategy() });
                break;
            case "All":
                StartGame(new List<IStrategy>() { new BasicStrategy(), new SemiStrategy(), new TableStrategy() });
                break;
            case "End":
                System.Environment.Exit(0);
                break;
        }
    }

    static void StartGame(List<IStrategy> strategies) {
        IRunner runner = new Runner();
        foreach (var strategy in strategies)
        {
            Game game = new Game(runner, strategy);
            GameManager gameManager = new GameManager(game);
            gameManager.RunSimulation(1000);
            Console.WriteLine("Statistics for " + strategy.GetType());
            PrintStatistics(gameManager);
            Console.WriteLine();
        }
        Console.WriteLine("Press any key to return to menu");
        Console.ReadLine();
        MenuSelection();
    }

    static string ProvideMenu() {
        int selectedLineIndex = 0;
        ConsoleKey pressedKey;
        do
        {
            UpdateMenu(selectedLineIndex);
            pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < Strategies.Length)
                selectedLineIndex++;

            else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                selectedLineIndex--;

        } while (pressedKey != ConsoleKey.Enter);
        return Strategies[selectedLineIndex];
    }

    static void UpdateMenu(int index)
    {
        Console.Clear();
        Console.WriteLine("Choose your strategy:");
        foreach (var strategy in Strategies)
        {
            bool isSelected = strategy == Strategies[index];
            Console.WriteLine($"{(isSelected ? "> " : "  ")}{strategy}");
        }
        Console.WriteLine();
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
