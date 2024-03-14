namespace MagicChess;
public class Rule{
    // first index is not used, since initial
    public const int maxLevel = 5;
    public int[] GoldToLevelPrice = new int[maxLevel]{0, 3, 4, 6, 7};
    public int[] ExpNeedForLevel = new int[maxLevel] {3, 5, 8, 12, 17};
    public int[] PiecesPerLevel = new int[maxLevel] {3, 4, 5, 6, 7};
}