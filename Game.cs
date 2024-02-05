using System.ComponentModel;

namespace blackjack;

public class Game
{
    private readonly IRunner runner;
    private readonly IStrategy strategy;

    public Game(IRunner runner, IStrategy strategy)
    {
        this.runner = runner;
        this.strategy = strategy;
    }

    public Result Play() {

        GameState initialState = runner.StartGame(strategy.GetInitialBet());

        GameState currentState = initialState;
        while (!currentState.Done) {
            Move nextMove = strategy.GetMove(currentState);
            currentState = TakeAction(nextMove);           
        }

        return new Result {
            Earnings = currentState.CurrentBid,
            InitialBet = currentState.InitialBid,
            Outcome = GetOutcome(currentState)
        };
    }

    public Outcome GetOutcome(GameState gameState) {
        if (gameState.IsStay) {
            if (gameState.DealerTotal > 21 || gameState.DealerTotal > gameState.PlayerTotal) return Outcome.Win;
            if (gameState.DealerTotal == gameState.PlayerTotal) return Outcome.Draw;
            return Outcome.Loss;
        }
        return Outcome.Loss;
    }

    public GameState TakeAction(Move move) {
        switch (move) {
            case Move.Hit: 
                return runner.Hit();
            case Move.Stay:
                return runner.Stay();
            case Move.Surrender:
                return runner.Surrender();
            case Move.DoubleDown:
                return runner.Stay(); //TODO: add DD
            default:
                return runner.Stay();
        }
    }
}
