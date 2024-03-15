namespace MagicChess.PieceChar;

public class SoulReaper : Piece
{
    public SoulReaper(string name){
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
        Races = Races.Demon;
        Class = PieceClass.Warlock;
        DamageToPlayer = 3;
    }
}
