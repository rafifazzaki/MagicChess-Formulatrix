namespace MagicChess;
public class Rule : IRule{
    // first index is not used, since initial
    public const int MaxLevel = 5;
    public int[] GoldToLevelPrice {get; private set;} = new int[MaxLevel]{0, 3, 4, 6, 7};
    public int[] ExpNeedForLevel {get; private set;} = new int[MaxLevel] {3, 5, 8, 12, 17};
    public int[] PiecesPerLevel {get; private set;} = new int[MaxLevel] {3, 4, 5, 6, 7};
}