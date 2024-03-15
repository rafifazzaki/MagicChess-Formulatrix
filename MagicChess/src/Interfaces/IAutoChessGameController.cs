namespace MagicChess;

public interface IAutoChessGameController
{
    int BattleRound { get; }
    bool IsGameEnded {get;}

    public IRule GetRule();
    public IPlayer GetCurrentPlayer();

    public IPlayer[] GetPlayersTurn();

    public IBattleStore GetStore();

    public IBattleArena GetArena();
    public Dictionary<IPlayer, IPlayerData> GetPlayersData();

}
