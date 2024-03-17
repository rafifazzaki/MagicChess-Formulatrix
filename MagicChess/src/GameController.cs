using Spectre.Console;

namespace MagicChess;

public class GameController : IAutoChessGameController
{
    public bool IsGameEnded {get; private set;}
    public int BattleRound { get; private set; }
    Dictionary<IPlayer, IPlayerData> playersData;// { get; private set; }
    IBattleArena arena;// { get; private set; }
    IBattleStore store;// { get; private set; }
    IPlayer[] PlayersTurn;// {get; private set;}
    IPlayer currentPlayer; //{get; private set;}
    IRule rule;// {get; private set;}
    PieceBattleLog pieceBattleLog;

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
    public IPlayerData GetPlayerData(IPlayer player){
        return playersData[player];
    }
    #endregion

    #region GetMethod
    public IPlayerData GetCurrentPlayerData(){
        return playersData[currentPlayer];
    }
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
    public int CurrentMaxAssignPiece(IPlayer player){
        return rule.PiecesPerLevel[GetCurrentPlayerData().Level - 1];
    }
    #endregion

    #region Checks
    // public bool IsPieceAssigned(IPiece piece){
    //         return piece.IsAssigned;
    //     }
    #endregion

    #region GameFunction
    public bool IsAnyPlayerDie(){
        bool isAnyDie = false;
        foreach (var item in PlayersTurn)
        {
            if (playersData[item].HP <= 0)
            {
                isAnyDie = true;
                break;
            }
        }
        if(isAnyDie){
            return true;
        }
        return false;
    }
    public IPlayer GetWinner(){
        bool isAnyAlive = false;
        IPlayer winner = null!;
        if(!IsAnyPlayerDie()){
            return null;
        }
        foreach (var item in PlayersTurn)
        {
            if (playersData[item].HP >= 0)
            {
                isAnyAlive = true;
                winner = item;
                break;
            }
        }
        if(isAnyAlive){
            return winner;
        }
        return null;
    }

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
        pieceBattleLog = new();
    }

    public IPiece GetPlayerPiece(int index){
        return playersData[currentPlayer].GetPieces()[index - 1];
    }

// `ienumerable
    public IEnumerable<IPiece> GetPieces(){
        return store.GetPieces();
    }

    public bool SetCurrentPlayer(IPlayer player){
        // validasi player if already in dict
        currentPlayer = player;
        return true;
    }

// ienumerable
    public List<IPiece> PieceOnStore(){
        
        // if(shuffle) {
        //     Util.Shuffle(store.GetPieces());
        // }
        //take the first 5 list, if PieceToshow is 5
        // ienumerable not need tolist
        return store.GetPieces().Take(store.PiecesToShow).ToList(); 
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

    public bool BuyPiece(IPlayer player, IPiece piece){
        // check if the player can buy
        if(playersData[player].Gold >= piece.Price){
            playersData[player].AddPiece(piece);
            store.GetPieces().Remove(piece);
            playersData[player].RemoveGold(piece.Price);
            return true;
        }
        return false;
    }

    public bool IsLevelMaxed(IPlayer player){
        if (GetCurrentPlayerData().Level >= Rule.MaxLevel)
        {
            return true;
        }
        return false;
    }

    public int RuleToLevelUp(IPlayer player){
        bool isCanLevelUp = false;
        int expToLevelUp = 0;
        foreach (int item in rule.ExpNeedForLevel)
        {
            if (GetCurrentPlayerData().Exp >= item)
            {
                isCanLevelUp = true;
                expToLevelUp = item;
                break;
            }
        }
        if(!isCanLevelUp){
            return 0;
        }
        return expToLevelUp;
    }

    public bool BuyLevel(IPlayer player){
        if(!IsLevelMaxed(player)){
            return false;
        }
        if(RuleToLevelUp(player) <= 0){
            return false;
        }
        GetCurrentPlayerData().IncreaseLevel();
        GetCurrentPlayerData().SetCurrentMaxAssign(rule.PiecesPerLevel[GetCurrentPlayerData().Level - 1]);
        GetCurrentPlayerData().RemoveGold(rule.GoldToLevelPrice[GetCurrentPlayerData().Level - 2]);
        return true;
    }

    public bool GiveGoldAndExp(int gold, int exp){
        if(playersData.Count() < 1){
            return false;
        }
        foreach (var item in playersData)
        {
            item.Value.IncreaseExp(exp);
            item.Value.AddGold(gold);
        }
        return true;
    }


    public bool AutoAttack(out ILogger pieceLog){
        // dictionary ngga pasti urut

        pieceLog = new PieceBattleLog();
        if(arena.GetPlayersAndPieces().Count < 2){
            return false;
        }

        IPlayer player1 = PlayersTurn[0];
        IPlayer player2 = PlayersTurn[1];
        List<IPiece> player1Pieces = arena.GetPlayersAndPieces()[player1];
        List<IPiece> player2Pieces = arena.GetPlayersAndPieces()[player2];
        foreach (var piece1 in player1Pieces)
        {
            foreach (var piece2 in player2Pieces)
            {
                piece2.GetDamage(piece1.AttackPoint);
                pieceLog.AddLog(player1, piece1, piece2);
            }
        }

        foreach (var piece2 in player2Pieces)
        {
            foreach (var piece1 in player1Pieces)
            {
                piece1.GetDamage(piece2.AttackPoint);
                pieceLog.AddLog(player2, piece2, piece1);
            }
        }
        return true;
    }

    public bool RemoveDeadPieces(){
        if(arena.GetPlayersAndPieces().Count <= 0){
            return false;
        }
        Dictionary<IPlayer, List<IPiece>> PlayerPieces = arena.GetPlayersAndPieces();

        for (int i = 0; i < PlayerPieces.Count; i++)
        {
            var kvp = PlayerPieces.ElementAt(i);
            var player = kvp.Key;
            var pieces = kvp.Value;

            for (int j = 0; j < pieces.Count; j++)
            {
                var piece = pieces[j];

                if (piece.CurrentHP <= 0)
                {
                    // Add the piece and its player to the removal list
                    // piecesToRemove.Add((player, piece));

                    piece.ResetCurrentHP();
                    arena.RemovePieceFromBoard(kvp.Key, piece);
                    
                }
            }
        }
        return true;
    }

    
}




