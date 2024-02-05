namespace blackjack;

public class GameManager : IStatistics
{
    private readonly IGame game;
    private List<Result> results = new List<Result>();

    public GameManager(IGame game)
    {
        this.game = game;
    }

    public void RunSimulation(int NumberOfGames) {
        for (int i = 0; i < NumberOfGames; i++)
        {
            results.Add(game.Play());
        }
    }

    public decimal GetAverageEarningsPerGame()
    {
        return GetEarnings() / results.Count;
    }

    public decimal GetEarnings()
    {
        return results.Select(x => x.Earnings).Sum();
    }

    public int GetLossCount()
    {
        return results.Where(x => x.Outcome == Outcome.Loss).Count();
    }

    public int GetNumberOfGames()
    {
        return results.Count;
    }

    public int GetVictoryCount()
    {
        return results.Where(x => x.Outcome == Outcome.Win).Count();
    }

    public decimal GetWinRatio()
    {
        return Math.Round((decimal)GetVictoryCount() / (decimal)(results.Count) * 100);
    }

    public decimal GetDrawCount()
    {
        return results.Where(x => x.Outcome == Outcome.Draw).Count();
    }
}
