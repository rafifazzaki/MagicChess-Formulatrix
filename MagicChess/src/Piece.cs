
public class Piece : IPiece
{
    public string Name { get; set; }
    public Races Races { get; set; }
    public PieceClass Class { get; set; }
    public int Price { get; set; }
    public StarLevel StarLevel { get; set; }
    public int HP { get; set; }
    public int Armor { get; set; }
    public int CurrentHP { get; set; }
    public (int, int) CurrentPosition {get; protected set;} = (-1, -1);
    public int AttackPoint { get; set; }
    public int AttackSpeed { get; set; }
    public int AttackRange { get; set; }
    public AttackType attackType { get; set; }
    public int DamageToPlayer {get; set;}
    public bool IsAssigned {get; private set;} = false;

    private (int, int) initialPosition = (-1, -1);

    public Piece(){
        CurrentHP = HP;
        CurrentPosition = initialPosition;
        IsAssigned = false;
    }


    public Piece(string Name, int Price, int HP, int Armor, int AttackPoint,int AttackSpeed,
    int AttackRange, int DamageToPlayer, AttackType attackType, Races races, PieceClass pieceClass){
        this.Name = Name;
        this.Price = Price;
        this.HP = HP;
        CurrentHP = HP;
        this.Armor = Armor;
        this.AttackPoint = AttackPoint;
        this.AttackSpeed = AttackSpeed;
        this.AttackRange = AttackRange;
        this.attackType = attackType;
        this.DamageToPlayer = DamageToPlayer;
        Races = races;
        Class = pieceClass;
        StarLevel = StarLevel.One;
    }

    public bool SetAssignedToTrue(){
        if(IsAssigned == true){ //check again
            return false;
        }
        IsAssigned = true;
        return true;
    }

    public bool SetPosition((int, int) position){
        if(position.Item1 < 0 || position.Item2 < 0){
            return false;
        }
        CurrentPosition = position;
        return true;
    }

    public bool ResetAssigned(){
        if(IsAssigned == false){ //check again
            return false;
        }
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

}

