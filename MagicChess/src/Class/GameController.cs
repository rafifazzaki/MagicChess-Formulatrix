namespace MagicChess;

public class GameController : IAutoChessGameController
{
    public int BattleRound { get; private set; }
    public Dictionary<IPlayer, IPlayerData> playersData;// { get; private set; }
    public IBattleArena arena;// { get; private set; }
    public IBattleStore store;// { get; private set; }
    public IPlayer[] PlayersTurn;// {get; private set;}
    public IPlayer currentPlayer; //{get; private set;}
    public bool IsGameEnded {get; private set;}
    public IRule rule;// {get; private set;}

    #region GetReferenceType
    public IRule GetRule(){
        return rule;
    }
    public IPlayer GetCurrentPlayer(){
        return currentPlayer;
    }

    public IPlayer[] GetPlayersTurn(){
        return PlayersTurn;
    }

    public IBattleStore GetStore(){
        return store;
    }

    public IBattleArena GetArena(){
        return arena;
    }
    public Dictionary<IPlayer, IPlayerData> GetPlayersData(){
        return playersData;
    }
    #endregion

    #region GetMethod
    public IPlayerData GetCurrentPlayerData(){
        return playersData[currentPlayer];
    }
    // public IEnumerable<IPiece> GetCurrentPlayerPieces(){
    //     return GetCurrentPlayerData().GetPieces();
    // }
    public IEnumerable<IPiece> GetPlayerAssignedPieces(IPlayer player){
        List<IPiece> assignedPieces = new();
        foreach (IPiece piece in playersData[player].GetPieces())
        {
            if(piece.IsAssigned){
                assignedPieces.Add(piece);
            }
        }
        return assignedPieces;
    }

    public IEnumerable<IPiece> GetPlayerUnassignedPieces(IPlayer player){
        List<IPiece> unassignedPieces = new();
        foreach (IPiece piece in playersData[player].GetPieces())
        {
            if(!piece.IsAssigned){
                unassignedPieces.Add(piece);
            }
        }
        return unassignedPieces;
    }
    #endregion

    #region Checks
    public bool IsPieceAssigned(IPiece piece){
            return piece.IsAssigned;
        }
    #endregion

    #region GameFunction

    #endregion
    public GameController(IBattleArena arena, IBattleStore store, Rule rule, Dictionary<IPlayer, IPlayerData> playersData)
    {
        this.arena = arena;
        this.store = store;
        this.rule = rule;

        this.playersData = playersData;
        currentPlayer = playersData.Keys.First();

        PlayersTurn = new IPlayer[playersData.Count];
        int i = 0;
        foreach (var item in playersData)
        {
            PlayersTurn[i] = item.Key;
            i++;
        }
    }

// `ienumerable
    public List<IPiece> GetPieces(){
        return store.Pieces;
    }

    public bool SetCurrentPlayer(IPlayer player){
        // validasi player if already in dict
        currentPlayer = player;
        return true;
    }

// ienumerable
    public List<IPiece> PieceOnStore(bool shuffle){
        
        if(shuffle) {
            Util.Shuffle(store.Pieces);
        }
        //take the first 5 list, if PieceToshow is 5
        // ienumerable not need tolist
        return store.Pieces.Take(store.PiecesToShow).ToList(); 
    }

    public bool NextTurn(IPlayer player){
        // validation
        // set it as current
        int index = Array.IndexOf(PlayersTurn, player);
        int nextIndex = (index + 1) % PlayersTurn.Length; //modulo for making the last index = 0. thx chatGPT
        currentPlayer = PlayersTurn[nextIndex];
        return true;
    }

    public bool SetGameEnded(){
        IsGameEnded = true;
        return true;
    }

    

}


