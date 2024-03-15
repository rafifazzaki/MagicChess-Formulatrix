namespace MagicChess.PieceChar;

public class ShamanOfDesert : Piece
{
    public ShamanOfDesert(string name)
    {
        Name = name;
        Price = 2;
        HP = 4;
        CurrentHP = 4;
        Armor = 0;
        AttackPoint = 3;
        AttackSpeed = 2;
        AttackRange = 2;
        attackType = AttackType.Area;
        // MoveSpeed = 2;
        Races = Races.Cave;
        Class = PieceClass.Mage;
        DamageToPlayer = 3;
    }
}
