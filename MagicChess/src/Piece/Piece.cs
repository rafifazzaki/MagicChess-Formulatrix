
public class Piece : IPiece, IPieceAttack, IPosition, IMove
{
    public string Name { get; protected set; }
    //
    public Races Races { get; protected set; }

    public PieceClass Class { get; protected set; }
    //
    public int Price { get; protected set; }

    
    

    public StarLevel StarLevel { get; protected set; }

    public int HP { get; protected set; }

    public int Armor { get; protected set; }

    public int AttackPoint { get; protected set; }

    public int AttackSpeed { get; protected set; }

    public int AttackRange { get; protected set; }

    public AttackType attackType { get; protected set; }


    public int MoveSpeed { get; protected set; }
    public int[,] Position { get; protected set; }

    public Piece(){
        
    }

    public Piece(string Name, int Price, int HP, int Armor, int AttackPoint,int AttackSpeed,
    int AttackRange, AttackType attackType , int MoveSpeed, Races races, PieceClass pieceClass){
        this.Name = Name;
        this.Price = Price;
        this.HP = HP;
        this.Armor = Armor;
        this.AttackPoint = AttackPoint;
        this.AttackSpeed = AttackSpeed;
        this.AttackRange = AttackRange;
        this.MoveSpeed = MoveSpeed;
        this.attackType = attackType;
        this.Races = races;
        this.Class = pieceClass;
        StarLevel = StarLevel.One;
    }

    public bool SetPosition(int[,] Position){
        return true;
    }

    public int GetDamage()
    {
        throw new NotImplementedException();
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

