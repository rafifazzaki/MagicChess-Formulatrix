public interface IPieceAttack{
<<<<<<< Updated upstream

    /// <summary>
    /// how much damage that this piece can deal to other pieces
    /// </summary>
    int AttackPoint {get;}

    /// <summary>
    /// how much speed this piece can has
    /// </summary>
    int AttackSpeed {get;}

    /// <summary>
    /// how much range this piece can attack on board
    /// </summary>
    int AttackRange {get;}

    /// <summary>
    /// what attack type this piece has on board
    /// </summary>
    AttackType attackType {get;}

    /// <summary>
    /// how much this piece damage the player
    /// </summary>
=======
    int AttackPoint {get;}
    int AttackSpeed {get;}
    int AttackRange {get;}
    AttackType attackType {get;}
>>>>>>> Stashed changes
    int DamageToPlayer {get;}
}

