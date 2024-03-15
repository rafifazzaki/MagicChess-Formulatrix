public interface IPiece : IPieceAttack{
    string Name {get;}
    string CharName { get; }
    Races Races {get;}
    PieceClass Class {get;}
    int Price {get;}
    StarLevel StarLevel {get;}
    int HP {get;}
    
    int Armor {get;}
    bool IsAssigned {get;}
    int CurrentHP {get;}
    public (int, int) CurrentPosition {get;}
    public bool SetAssignedToTrue();
    public bool SetPosition((int, int) position);
    public bool ResetAssigned();
    public int GetDamage(int damage);
    public bool ResetCurrentHP();
    
}