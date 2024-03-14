
public class Piece : IPiece
{
    public string Name { get; protected set; }
    public Races Races { get; protected set; }
    public PieceClass Class { get; protected set; }
    public int Price { get; protected set; }
    public StarLevel StarLevel { get; protected set; }
    public int HP { get; protected set; }
    public int Armor { get; protected set; }
    public int CurrentHP { get; protected set; }
    public (int, int) CurrentPosition {get; protected set;}
    public int AttackPoint { get; protected set; }
    public int AttackSpeed { get; protected set; }
    public int AttackRange { get; protected set; }
    public AttackType attackType { get; protected set; }
    public int DamageToPlayer {get; protected set;}
    public bool IsAssigned {get; private set;}
    public int MoveSpeed { get; protected set; }

    private (int, int) initialPosition = (-1, -1);

    public Piece(){
        CurrentHP = HP;
        CurrentPosition = initialPosition;
        IsAssigned = false;
    }

    // public Piece(string Name, int Price, int HP, int Armor, int AttackPoint,int AttackSpeed,
    // int AttackRange, AttackType attackType , int MoveSpeed, Races races, PieceClass pieceClass){
    //     this.Name = Name;
    //     this.Price = Price;
    //     this.HP = HP;
    //     this.Armor = Armor;
    //     this.AttackPoint = AttackPoint;
    //     this.AttackSpeed = AttackSpeed;
    //     this.AttackRange = AttackRange;
    //     this.MoveSpeed = MoveSpeed;
    //     this.attackType = attackType;
    //     this.Races = races;
    //     this.Class = pieceClass;
    //     StarLevel = StarLevel.One;
    //     CurrentHP = HP;
    // }

    public bool SetAssignedToTrue(){
        IsAssigned = true;
        return true;
    }

    public bool SetPosition((int, int) position){
        CurrentPosition = position;
        return true;
    }

    public bool ResetAssigned(){
        IsAssigned = false;
        CurrentPosition = initialPosition;
        return true;
    }



    public int GetDamage(int damage)
    {
        CurrentHP -= damage;
        return CurrentHP;
    }

    public bool ResetCurrentHP(){
        CurrentHP = HP;
        return true;
    }

    public bool MoveToLocation()
    {
        throw new NotImplementedException();
    }

    public bool PickTarget()
    {
        throw new NotImplementedException();
    }
}

