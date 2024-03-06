namespace MagicChess;

public class GameController
{
}

public class IAutoChessGameController{
    int BattleRound {get;}
    
    int PreparationTime{get;}
    Dictionary<IPlayer, PlayerData> PlayersData;
    
}

public class PlayerData{
    List<IPiece> Pieces;
    Dictionary<Races, IPiece> RaceSynergy;
    Dictionary<PieceClass, IPiece> ClassSynergy;
    Arena myArena;

}
public enum Arena{
    Default,
    Red
}