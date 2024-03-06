public interface IPiece{
    int ID {get;}
    string name {get;}
    Races Races {get;}
    PieceClass Class {get;}
    int Price {get;}
    StarLevel StarLevel {get;}
    DamageToPlayer damageToPlayer {get;}
    int HP {get;}
    int Armor {get;}
    public int GetDamage();
}