namespace MagicChess;

public interface IAutoChessGameController
{
    int BattleRound { get; }
    IBattleArena arena { get; }
    BattleStore store { get; }
    Dictionary<IPlayer, IPlayerData> PlayersData { get; }
    public IPlayer CurrentPlayer {get;}
    bool IsGameEnded {get;}

}
