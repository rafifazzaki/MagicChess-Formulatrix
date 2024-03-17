namespace MagicChess;

public interface IRule
{
    public const int MaxLevel = 5;
    public int[] GoldToLevelPrice {get;}
    public int[] ExpNeedForLevel {get;}
    public int[] PiecesPerLevel {get;}
}
