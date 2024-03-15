namespace MagicChess.PieceChar;

public class Unicorn : Piece
{
    public Unicorn(string name)
    {
        Name = name;
        Price = 1;
        HP = 5;
        CurrentHP = 5;
        Armor = 3;
        AttackPoint = 2;
        AttackSpeed = 2;
        AttackRange = 2;
        attackType = AttackType.Straight;
        // MoveSpeed = 2;
        Races = Races.Feathered;
        Class = PieceClass.Hunter;
        DamageToPlayer = 3;
    }
}
