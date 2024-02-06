namespace blackjack;

class Program
{
    static readonly string[] Strategies = new []
    {
        "Basic",
        "Semi",
        "Table"
    };
    static void Main(string[] args)
    {
        
        string Strategy = ProvideMenu();
        switch (Strategy) {
            case "Basic":
                StartGame(new BasicStrategy());
                break;
            case "Semi":
                StartGame(new SemiStrategy());
                break;
            case "Table":
                StartGame(new TableStrategy());
                break;
        }
    }

    static void StartGame(IStrategy strategy) {
        IRunner runner = new Runner();
        
        Game game = new Game(runner, strategy);
        GameManager gameManager = new GameManager(game);
        gameManager.RunSimulation(1000);
        PrintStatistics(gameManager);
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
        // Console.WriteLine($"{} was chosen as an option");  
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
