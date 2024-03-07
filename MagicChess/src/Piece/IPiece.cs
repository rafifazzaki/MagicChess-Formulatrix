public interface IPiece : IPieceAttack, IPosition, IMove{
    string Name {get;}
    Races Races {get;}
    PieceClass Class {get;}
    int Price {get;}
    StarLevel StarLevel {get;}
    int HP {get;}
    int Armor {get;}
    public int GetDamage();
}