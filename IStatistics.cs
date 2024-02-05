namespace blackjack;

public interface IStatistics
{
    public int GetVictoryCount();
    public int GetLossCount();
    public decimal GetEarnings();
    public decimal GetWinRatio(); 
    public decimal GetDrawCount();
    public decimal GetAverageEarningsPerGame();
    public int GetNumberOfGames();

}
