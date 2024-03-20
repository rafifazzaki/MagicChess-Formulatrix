namespace MagicChess;

/// <summary>
/// this class is to saves player's data
/// </summary>
public interface IPlayerData
{
    /// <summary>
    /// player's current HP
    /// </summary>
    public int HP { get; }

    /// <summary>
    /// player's current gold
    /// </summary>
    public int Gold { get; }

    /// <summary>
    /// player's current exp
    /// </summary>
    public int Exp { get; }
    /// <summary>
    /// player's current level
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// maximum assign can players do
    /// </summary>
    public int MaxAssign { get; }

    /// <summary>
    /// get pieces that player has
    /// </summary>
    /// <returns></returns>
    public List<IPiece> GetPieces();

    /// <summary>
    /// damage player
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int GetDamage(int damage);

    /// <summary>
    /// remove add from player
    /// </summary>
    /// <param name="gold"></param>
    /// <returns></returns>
    public int AddGold(int gold);

    /// <summary>
    /// remove gold from player
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public bool RemoveGold(int price);

    /// <summary>
    /// add piece to player
    /// </summary>
    /// <param name="piece"></param>
    /// <returns></returns>
    public bool AddPiece(IPiece piece);

    /// <summary>
    /// increase player
    /// </summary>
    /// <returns></returns>
    public bool IncreaseLevel();

    /// <summary>
    /// increase exp for the player
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    public bool IncreaseExp(int exp);

    /// <summary>
    /// setting maximum assign piece for the player
    /// </summary>
    /// <param name="maxAssign"></param>
    /// <returns></returns>
    public bool SetCurrentMaxAssign(int maxAssign);
}