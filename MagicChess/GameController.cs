namespace MagicChess;

public class GameController : IAutoChessGameController
{
    public int BattleRound { get; private set; }
    public Dictionary<IPlayer, PlayerData> PlayersData { get; private set; }
    public IBattleArena arena { get; private set; }
    public BattleStore store { get; private set; }
    public IPlayer[] PlayersTurn {get; private set;}
    public IPlayer CurrentPlayer {get; private set;}
    public bool IsGameEnded {get; private set;}
    public Rule Rule {get; private set;}

    public GameController(IBattleArena arena, BattleStore store, Rule rule, Dictionary<IPlayer, PlayerData> playersData)
    {
        this.arena = arena;
        this.store = store;
        this.Rule = rule;

        this.PlayersData = playersData;
        CurrentPlayer = playersData.Keys.First();

        PlayersTurn = new IPlayer[playersData.Count];
        int i = 0;
        foreach (var item in playersData)
        {
            PlayersTurn[i] = item.Key;
            i++;
        }
    }


    public List<IPiece> GetPieces(){
        return store.Pieces;
    }

    public bool SetCurrentPlayer(IPlayer player){
        CurrentPlayer = player;
        return true;
    }

    public List<IPiece> PieceOnStore(bool shuffle){
        if(shuffle) {
            Util.Shuffle(store.Pieces);
        }
        //take the first 5 list, if PieceToshow is 5
        return store.Pieces.Take(store.PiecesToShow).ToList(); 
    }

    public bool NextTurn(IPlayer player){
        // set it as current
        int index = Array.IndexOf(PlayersTurn, player);
        int nextIndex = (index + 1) % PlayersTurn.Length; //modulo for making the last index = 0. thx chatGPT
        CurrentPlayer = PlayersTurn[nextIndex];
        return true;
    }

/*
    bool Buy(IPlayer player, IPiece piece)
    {
        PlayersData[player].SetGold(PlayersData[player].Gold - piece.Price);
        PlayersData[player].AddPiece(piece);
        store.Pieces.Remove(piece);
        return true;
    }
*/
    public bool SetGameEnded(){
        IsGameEnded = true;
        return true;
    }

    public bool BuyLevel(IPlayer player){
        // if exp > needed to lvl up
            // lvl + 1
            // exp -= according to priceLevel
        return true;
    }

}

public interface IAutoChessGameController
{
    int BattleRound { get; }
    IBattleArena arena { get; }
    BattleStore store { get; }
    Dictionary<IPlayer, PlayerData> PlayersData { get; }
    public IPlayer CurrentPlayer {get;}
    bool IsGameEnded {get;}

}

