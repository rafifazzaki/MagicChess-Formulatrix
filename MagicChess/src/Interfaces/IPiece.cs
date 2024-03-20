public interface IPiece : IPieceAttack{
    /// <summary>
    /// this piece's name
    /// </summary>
    string Name {get;}

    /// <summary>
    /// this piece's races for synergy
    /// </summary>
    Races Races {get;}

    /// <summary>
    /// this piece's class for synergy
    /// </summary>
    PieceClass Class {get;}

    /// <summary>
    /// this piece's price
    /// </summary>
    int Price {get;}

    /// <summary>
    /// this piece's star level
    /// </summary>
    StarLevel StarLevel {get;}

    /// <summary>
    /// The HP of this piece
    /// </summary>
    int HP {get;}
    

    /// <summary>
    /// how much armor this piece has
    /// </summary>
    int Armor {get;}

    /// <summary>
    /// to get this piece status, whether it is assigned or not
    /// </summary>
    bool IsAssigned {get;}

    /// <summary>
    /// this piece's current HP
    /// </summary>
    int CurrentHP {get;}

    /// <summary>
    /// current position on board, will return (-1, -1) if not in board
    /// </summary>
    public (int, int) CurrentPosition {get;}

    /// <summary>
    /// set assigned to true
    /// </summary>
    /// <returns></returns>
    public bool SetAssignedToTrue();

    /// <summary>
    /// Set the piece currentPosition
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool SetPosition((int, int) position);

    /// <summary>
    /// reset piece's isAssigned and CurrentPosition
    /// </summary>
    /// <returns></returns>
    public bool ResetAssigned();

    /// <summary>
    /// damage this piece
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int GetDamage(int damage);

    /// <summary>
    /// reset current HP
    /// </summary>
    /// <returns></returns>
    public bool ResetCurrentHP();
    
}