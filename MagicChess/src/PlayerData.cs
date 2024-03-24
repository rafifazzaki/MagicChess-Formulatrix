namespace MagicChess;

public class PlayerData : IPlayerData
{
    public List<IPiece> pieces;// { get; } //sama, method baru aja (ienumerable)
    public int HP { get; private set; }
    public int Gold { get; private set; }
    public int Exp { get; private set; }
    public int Level { get; private set; }
    public int MaxAssign { get; private set; }

    public List<IPiece> GetPieces(){
        return pieces;
    }

    public PlayerData()
    {
        pieces = new();
        HP = 10;
        Gold = 2;
        Exp = 0;
        Level = 1;
        MaxAssign = 3;
    }
    public int GetDamage(int damage)
    {
        HP -= damage;
        return HP;
    }

    public int AddGold(int gold)
    {
        Gold += gold;
        return Gold;
    }
    public bool RemoveGold(int price)
    {
        if(price < 0){
            return false;
        }
        Gold -= price;
        return true;
    }
    public bool AddPiece(IPiece piece)
    {
        if(piece == null){
            return false;
        }
        pieces.Add(piece);
        return true;
    }
    public bool IncreaseLevel()
    {
        if(Level == null){
            return false;
        }
        Level += 1;
        return true;
    }

    public bool IncreaseExp(int exp)
    {
        if(exp < 0){
            return false;
        }
        Exp += exp;
        return true;
    }
    public bool SetCurrentMaxAssign(int maxAssign)
    {
        if(maxAssign < 0){
            return false;
        }
        MaxAssign = maxAssign;
        return true;
    }
}

