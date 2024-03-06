public interface IPiece{
    int ID {get;}
    string name {get;}
    Races Races {get;}
    PieceClass Class {get;}
    int Price {get;}
    StarLevel StarLevel {get;}
    int HP {get;}
    int Armor {get;}
}

public interface IMove{
    bool PickTarget();
    bool Move();
}

public interface IPieceAttack{
    int AttackPoint {get;}
    int AttackSpeed {get;}
    int AttackRange {get;}
    AttackType attackType {get;}
}

public enum AttackType{
    Straight,
    WideFront,
    Area
}