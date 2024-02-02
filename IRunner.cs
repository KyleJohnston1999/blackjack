namespace blackjack;

public interface IRunner
{
    public GameState StartGame(int bet);
    public GameState Hit();
    public GameState Stay();
    public GameState Surrender();
}
