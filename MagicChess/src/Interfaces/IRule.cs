namespace MagicChess;

/// <summary>
/// This class is a base for setting some rules per level up.
/// </summary>
public interface IRule
{

    /// <summary>
    /// Seet this to set how many level can player achieves
    /// </summary>
    public int MaxLevel {get;}

    /// <summary>
    /// Set this as how many gold that player need to buy the level
    /// </summary>
    public int[] GoldToLevelPrice {get;}

    /// <summary>
    /// Set this as how many exp needed per player to level up
    /// </summary>
    public int[] ExpNeedForLevel {get;}

    /// <summary>
    /// Set this as how many player can assign pieces per level
    /// </summary>
    public int[] PiecesPerLevel {get;}
}
