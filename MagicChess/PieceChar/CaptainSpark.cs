namespace MagicChess.PieceChar;

public class CaptainSpark : Piece
{
    public CaptainSpark(string name)
    {
        Name = name;
        Price = 1;
        HP = 5;
        CurrentHP = 5;
        Armor = 0;
        AttackPoint = 1;
        AttackSpeed = 2;
        AttackRange = 2;
        attackType = AttackType.Straight;
        // MoveSpeed = 2;
        Races = Races.Human;
        Class = PieceClass.Hunter;
        DamageToPlayer = 3;
    }
}
