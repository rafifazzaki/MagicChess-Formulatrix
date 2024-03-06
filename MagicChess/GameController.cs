namespace MagicChess;

public class GameController : IAutoChessGameController
{
    public int BattleRound {get; private set;}
    public Dictionary<IPlayer, PlayerData> PlayersData {get; private set;}
    IBattleArena arena;
    BattleStore store;
    IPlayer[] players;
    List<IPiece> AllPieces;

    public GameController(IBattleArena arena, BattleStore store, List<IPiece> allPieces, params IPlayer[] players){
        this.arena = arena;
        this.store = store;
        this.players = players;
        this.AllPieces = allPieces;

        foreach(IPlayer i in players)  
        { 
            PlayerData data = new();
            PlayersData.Add(i, data);
        } 
    }

    public bool ChangeTurn(){
        Util.Shuffle(AllPieces);
        return true;
    }

    public IEnumerable<IPiece> ShowStore(){
        var pieces = AllPieces.Take(store.PiecesToShow);
        return pieces;
    }

    public bool Buy(IPlayer player, IPiece piece){
        PlayersData[player].SetGold(PlayersData[player].Gold - piece.Price);
        PlayersData[player].AddPiece(piece);
        return true;
    }

}

public interface IAutoChessGameController{
    int BattleRound {get;}
    Dictionary<IPlayer, PlayerData> PlayersData {get;}
    
}
public class PlayerData{
    List<IPiece> Pieces;
    public int Gold {get; private set;}
    public int Exp {get; private set;} //
    public int Level {get; private set;} //max 9

    public PlayerData(){
        Pieces = new();
    }
    
    public int SetGold(int gold){
        Gold = gold;
        return gold;
    }
    public bool AddPiece(IPiece piece){
        Pieces.Add(piece);
        return true;
    }
}

public enum ExpAndLevel{
    Lv1 = 23,
    Lv2,
    Lv3,
    Lv4,
    Lv5

}