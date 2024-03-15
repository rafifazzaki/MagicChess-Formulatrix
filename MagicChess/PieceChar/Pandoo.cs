namespace MagicChess.PieceChar;

public class Pandoo : Piece
{
    public Pandoo(string name)
    {
        Name = name;
        Price = 1;
        HP = 3;
        CurrentHP = 3;
        Armor = 0;
        AttackPoint = 2;
        AttackSpeed = 2;
        AttackRange = 2;
        attackType = AttackType.Area;
        // MoveSpeed = 2;
        Races = Races.Pandaman;
        Class = PieceClass.Mage;
        DamageToPlayer = 3;
    }
}
