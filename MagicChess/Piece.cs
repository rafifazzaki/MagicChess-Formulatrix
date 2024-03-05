namespace MagicChess;

public class Piece : IPiece
{
}

public interface IPiece{

}

public enum PieceType{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
public enum SkillsType{
    BatteryAssault,
    ParalysisShuriken,

}
public enum PieceCategories{
    Knight,
    Warlock,
    Mage,
    Warrior,
    Hunter
}