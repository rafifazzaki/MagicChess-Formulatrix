namespace MagicChess;

public class Piece
{
}

public interface IPiece{
    int ID {get;}
    string name {get;}
    PieceType charType {get;}
    Races Races {get;}
    PieceCategories categories {get;}
    int Price {get;}
    // levelsType enum belum dibuat
    bool isAlive {get;}
    int HP {get;}
    int Armor {get;}
    int AttackPoint {get;}
    int AttackSpeed {get;}
    // MagRes?
    // position
}

public enum PieceType{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public enum Races{
    Pandaman,
    Civet,
    Human,
    Feathered,
    Demon
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