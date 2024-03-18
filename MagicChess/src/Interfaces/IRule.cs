namespace MagicChess;

public interface IRule
{
    public int MaxLevel {get;}
    public int[] GoldToLevelPrice {get;}
    public int[] ExpNeedForLevel {get;}
    public int[] PiecesPerLevel {get;}
}
