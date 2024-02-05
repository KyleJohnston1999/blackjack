using System.ComponentModel;

namespace blackjack;

public class Game : IGame
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

        var Outcome = CalculateOutcome(currentState);


        return new Result {
            Earnings = CalculateEarnings(currentState, Outcome),
            InitialBet = currentState.InitialBid,
            Outcome = Outcome
        };
    }

    public decimal CalculateEarnings(GameState gameState, Outcome outcome) {
        switch (outcome) {
            case Outcome.Win:
                if (gameState.PlayerTotal == 21) {
                    return gameState.CurrentBid * 1.5m;
                }
                return gameState.CurrentBid;
            case Outcome.Loss:
                if (gameState.IsSurrender) return -0.5m * gameState.CurrentBid;
                return -1 * gameState.CurrentBid;
            default:
                return 0;
        }
    }

    public Outcome CalculateOutcome(GameState gameState) {
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
