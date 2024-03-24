using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
namespace MagicChess;

/// <summary>
/// AutoChess Game Controller
/// </summary>
public interface IAutoChessGameController
{
    int BattleRound { get; }
    
    /// <summary>
    /// Check if the game is already ended
    /// </summary>
    bool IsGameEnded {get;}

    /// <summary>
    /// Get the rule of the game such as how many exp and gold do the players get at a certain levels
    /// </summary>
    /// <returns></returns>
    public IRule GetRule();

    /// <summary>
    /// get current player of the game
    /// </summary>
    /// <returns></returns>
    public IPlayer GetCurrentPlayer();

    /// <summary>
    /// returns all of the player that was inside the turn
    /// </summary>
    /// <returns></returns>
    public IPlayer[] GetPlayersTurn();

    /// <summary>
    /// get BattleStore
    /// </summary>
    /// <returns></returns>
    public IBattleStore GetStore();

    /// <summary>
    /// Get BattleArena
    /// </summary>
    /// <returns></returns>
    public IBattleArena GetArena();

        /// <summary>
    /// Get All player's Data
    /// </summary>
    /// <returns></returns>
    public Dictionary<IPlayer, IPlayerData> GetPlayersData();


    /// <summary>
    /// Get battleLogger
    /// </summary>
    /// <returns>
    /// BattleLogger which has: players and pieces that attacking, and whos being attacked (piece and player)
    /// and what piece that has died
    /// </returns>
    public IBattleLogger GetBattleLogger();



    /// <summary>
    /// get specifics player data using IPlayer parameter
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IPlayerData GetPlayerData(IPlayer player);

    /// <summary>
    /// Get current player data
    /// </summary>
    /// <returns></returns>
    public IPlayerData GetCurrentPlayerData();

    /// <summary>
    /// Get all assigned pieces from a player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerable<IPiece> GetPlayerAssignedPieces(IPlayer player);

    /// <summary>
    /// get all unassigned pieces from a player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerable<IPiece> GetPlayerUnassignedPieces(IPlayer player);

    /// <summary>
    /// get how max a player can assign according to rule
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public int CurrentMaxAssignPiece();


    /// <summary>
    /// get logger
    /// </summary>
    /// <returns></returns>
    public ILogger<GameController>? GetLogger();

    /// <summary>
    /// checks if any of the players die
    /// </summary>
    /// <returns></returns>
    public bool IsAnyPlayerDie();

    /// <summary>
    /// check if there is any player die, return player who wins
    /// </summary>
    /// <returns></returns>
    public IPlayer GetWinner();

    /// <summary>
    /// Get player's pieces from an index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IPiece GetPlayerPiece(int index);


    /// <summary>
    /// GetPieces from the store
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IPiece> GetPieces();


    /// <summary>
    /// set current player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool SetCurrentPlayer(IPlayer player);


    /// <summary>
    /// get piece on store according to var: PieceToShow
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IPiece> PieceOnStore();

    /// <summary>
    /// contains logic for the next turn
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool NextTurn(IPlayer player);

    /// <summary>
    /// for setting if the game has ended
    /// </summary>
    /// <returns></returns>
    public bool SetGameEnded();

    /// <summary>
    /// buy pieces from the store
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <returns></returns>
    public bool BuyPiece(IPlayer player, IPiece piece);

    /// <summary>
    /// Check if the player has maxed their levels
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsLevelMaxed(IPlayer player);

    /// <summary>
    /// for checking if the player can level up
    /// </summary>
    /// <param name="player"></param>
    /// <returns>
    /// how much exp needed to level up
    /// </returns>
    public int RuleToLevelUp(IPlayer player);

    /// <summary>
    /// Buy level's if the player met the requirement
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool BuyLevel(IPlayer player);

    /// <summary>
    /// give gold and exp to all players in playerData
    /// </summary>
    /// <param name="gold"></param>
    /// <param name="exp"></param>
    /// <returns></returns>
    public bool GiveGoldAndExp(int gold, int exp);

    /// <summary>
    /// AutoAttack will looping from player 1 piece's attacking each pieces of the player 2,
    /// then vice versa
    /// </summary>
    /// <param name="pieceLog"></param>
    /// <returns></returns>
    public bool AutoAttack(ref IBattleLogger pieceLog);

    /// <summary>
    /// remove dead pieces if any of the pieces current hp is 0 or less
    /// </summary>
    /// <returns></returns>
    public bool RemoveDeadPieces();

}
