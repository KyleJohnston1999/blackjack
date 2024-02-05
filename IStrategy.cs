namespace blackjack;

public interface IStrategy
{
    Move GetMove(GameState gameState);

    int GetInitialBet();
}
