
using MagicChess;

public class RedAxe : Piece
{
    // public RedAxe()
    // {
    //     Name = "RedAxe";
    //     Price = 1;
    //     HP = 7;
    //     CurrentHP = 7;
    //     Armor = 1;
    //     AttackPoint = 3;
    //     AttackSpeed = 2;
    //     AttackRange = 2;
    //     attackType = AttackType.WideFront;
    //     MoveSpeed = 2;
    //     Races = Races.Cave;
    //     Class = PieceClass.Warrior;
    // }

    public RedAxe(string name)
    {
        Name = name;
        Price = 1;
        HP = 7;
        CurrentHP = 7;
        Armor = 1;
        AttackPoint = 2;
        AttackSpeed = 2;
        AttackRange = 2;
        attackType = AttackType.WideFront;
        Races = Races.Cave;
        Class = PieceClass.Warrior;
        DamageToPlayer = 3;
    }
}
