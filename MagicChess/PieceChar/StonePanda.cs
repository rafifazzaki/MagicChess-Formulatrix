namespace MagicChess.PieceChar;

public class StonePanda : Piece
{
    public StonePanda(string name)
    {
        Name = name;
        Price = 2;
        HP = 6;
        CurrentHP = 6;
        Armor = 3;
        AttackPoint = 2;
        AttackSpeed = 2;
        AttackRange = 2;
        attackType = AttackType.WideFront;
        // MoveSpeed = 2;
        Races = Races.Pandaman;
        Class = PieceClass.Mage;
        DamageToPlayer = 3;
    }
}
