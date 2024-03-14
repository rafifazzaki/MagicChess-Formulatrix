namespace MagicChess;

public interface IPlayerData
{
    public List<IPiece> Pieces { get; }
    public int HP { get; }
    public int Gold { get; }
    public int Exp { get; }
    public int Level { get; }
    public int MaxAssign { get; }

    public int GetDamage(int damage);

    public int AddGold(int gold);
    public bool RemoveGold(int price);
    public bool AddPiece(IPiece piece);
    public bool IncreaseLevel();

    public bool IncreaseExp(int exp);
    public bool SetCurrentMaxAssign(int maxAssign);
}