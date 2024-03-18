
namespace MagicChess;
public class Rule : IRule{
    // first index is not used, since initial
    
    public int MaxLevel {get; set;}
    public int[] GoldToLevelPrice {get; set;}// = new int[MaxLevel]{0, 3, 4, 6, 7};
    public int[] ExpNeedForLevel {get; set;}// = new int[MaxLevel] {3, 5, 8, 12, 17};
    public int[] PiecesPerLevel {get; set;}// = new int[MaxLevel] {3, 4, 5, 6, 7};


    public Rule(){}
    public Rule(int maxLevel, int[] goldToLevelPrice, int[] expNeedForLevel, int[] piecesPerLevel){
        MaxLevel = maxLevel;
        GoldToLevelPrice = goldToLevelPrice;
        ExpNeedForLevel = expNeedForLevel;
        PiecesPerLevel = piecesPerLevel;
    }
}