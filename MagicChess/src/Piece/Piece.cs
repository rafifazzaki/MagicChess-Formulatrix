namespace MagicChess;

public class Piece : IPiece, IPieceAttack, IPosition, IMove
{
    #region IPiece
    public int ID { get; private set; }

    public string name { get; private set; }

    public Races Races { get; private set; }

    public PieceClass Class { get; private set; }

    public int Price { get; private set; }

    public StarLevel StarLevel { get; private set; }

    public DamageToPlayer damageToPlayer { get; private set; }

    public int HP { get; private set; }

    public int Armor { get; private set; }

    #endregion

    #region IPieceAttack
    public int AttackPoint { get; private set; }

    public int AttackSpeed { get; private set; }

    public int AttackRange { get; private set; }

    public AttackType attackType { get; private set; }

    
    #endregion

    #region IPosition

    public int[,] Position {get; private set;}
    #endregion

    #region IMove
    public float MoveSpeed {get; private set;}

    #endregion


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

