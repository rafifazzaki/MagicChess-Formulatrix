using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace MagicChess;
/// <summary>
/// GameController To control the flow of the game, assuming a normal gameplay where
/// the user want to play with automatic battle (as for now without position)
/// </summary>
public class GameController : IAutoChessGameController
{
    public bool IsGameEnded {get; private set;}
    public int BattleRound { get; private set; }
    Dictionary<IPlayer, IPlayerData> playersData;
    IBattleArena arena;
    IBattleStore store;
    IPlayer[] PlayersTurn;
    IPlayer currentPlayer;
    IRule rule;
    internal IBattleLogger battleLogger;
    ILogger<GameController>? _log;

    #region GetReferenceType

    public IBattleLogger GetBattleLogger(){
        return battleLogger;
    }

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

    
    public int CurrentMaxAssignPiece(){
        return rule.PiecesPerLevel[GetCurrentPlayerData().Level - 1];
    }
    #endregion
    
    public ILogger<GameController>? GetLogger(){
        return _log;
    }

    #region Checks
    
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


    /// <summary>
    /// constructor, needs BattleArena, BattleStore, Rule, PlayersData,
    /// BattleLogger (for getting variable from AutoBattle), and optionally a logger for the game
    /// </summary>
    /// <param name="arena"></param>
    /// <param name="store"></param>
    /// <param name="rule"></param>
    /// <param name="playersData"></param>
    /// <param name="battleLogger"></param>
    /// <param name="logger"></param>
    public GameController(IBattleArena arena, IBattleStore store, Rule rule, Dictionary<IPlayer, IPlayerData> playersData, IBattleLogger battleLogger, ILogger<GameController>? logger = null)
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
        this.battleLogger = battleLogger;
        _log = logger;
        _log?.LogInformation("Game Controller has been made");
    }
    
    public IPiece GetPlayerPiece(int index){
        IPiece piece = playersData[currentPlayer].GetPieces()[index - 1];
        _log?.LogInformation("GetPlayerPiece: {pieces} from {player}", piece.Name, currentPlayer);
        return piece;
    }
    

    public IEnumerable<IPiece> GetPieces(){
        _log?.LogInformation("GetPieces: GetPieces from store {pieces}", store.GetPieces().Count());
        return store.GetPieces();
    }
    
    public bool SetCurrentPlayer(IPlayer player){
        // validasi player if already in dict
        _log?.LogInformation("SetCurrentPlayer: player is {player}", currentPlayer.Name);
        currentPlayer = player;
        return true;
    }

    
    public IEnumerable<IPiece> PieceOnStore(){
        
        // if(shuffle) {
        //     Util.Shuffle(store.GetPieces());
        // }
        //take the first 5 list, if PieceToshow is 5
        // ienumerable not need tolist

        _log?.LogInformation("PieceOnStore: get from store {number}", store.PiecesToShow);
        return store.GetPieces().Take(store.PiecesToShow); 
    }
    
    public bool NextTurn(IPlayer player){
        // validation
        if(player == null){
            return false;
        }
        // set it as current
        int index = Array.IndexOf(PlayersTurn, player);
        int nextIndex = (index + 1) % PlayersTurn.Length; //modulo for making the last index = 0. thx chatGPT
        _log?.LogInformation("NextTurn: from {player} to {nextPlayer}", PlayersTurn[nextIndex]);
        currentPlayer = PlayersTurn[nextIndex];
        return true;
    }
    
    public bool SetGameEnded(){
        if(IsGameEnded == true){ //check again
            return false;
        }
        IsGameEnded = true;
        _log?.LogInformation("SetGameEnded called");
        return true;
    }
    
    public bool BuyPiece(IPlayer player, IPiece piece){
        // check if the player can buy
        if(player == null || piece == null){
            return false;
        }
        if(playersData[player].Gold >= piece.Price){
            _log?.LogInformation("BuyPiece: {player} has bought {piece}", player, piece);
            playersData[player].AddPiece(piece);
            store.GetPieces().Remove(piece);
            playersData[player].RemoveGold(piece.Price);
            return true;
        }
        _log?.LogWarning("BuyPiece: {player} cannot buy {piece}. trying to buy {price} with {gold}", player.Name, piece.Name, piece.Price, playersData[player].Gold);
        return false;
    }


    
    
    public bool IsLevelMaxed(IPlayer player){
        if(player == null){
            return false;
        }
        if (GetPlayerData(player).Level >= rule.MaxLevel)
        {
            _log?.LogInformation("IsLevelMaxed: {player}'s level is sufficient", GetCurrentPlayerData().Level);
            return true;
        }
        _log?.LogWarning("IsLevelMaxed: cannot level up. player's level: {level}, rule.Maxlevel: {}", GetCurrentPlayerData().Level, rule.MaxLevel);
        return false;
    }

    
    public int RuleToLevelUp(IPlayer player){
        bool isCanLevelUp = false;
        int expToLevelUp = 0;
        foreach (var item in rule.ExpNeedForLevel)
        {
            if (GetPlayerData(player).Exp >= item)
            {
                isCanLevelUp = true;
                expToLevelUp = item;
                break;
            }
        }
        if(!isCanLevelUp){
            _log?.LogWarning("RuleToLevelUp: cannot levels up. return 0");
            return 0;
        }
        _log?.LogInformation("RuleToLevelUp: return {expToLevelUp}", expToLevelUp);
        return expToLevelUp;
    }
    
    public bool BuyLevel(IPlayer player){
        if(player == null){
            return false;
        }
        if(!IsLevelMaxed(player)){
            _log?.LogWarning("BuyLevel: {player} Level is maxed", player.Name);
            return false;
        }
        if(RuleToLevelUp(player) < 0){
            // ASK
            _log?.LogWarning("BuyLevel: cannot level up");
            return false;
        }
        GetCurrentPlayerData().IncreaseLevel();
        GetCurrentPlayerData().SetCurrentMaxAssign(rule.PiecesPerLevel[GetCurrentPlayerData().Level - 1]);
        GetCurrentPlayerData().RemoveGold(rule.GoldToLevelPrice[GetCurrentPlayerData().Level - 2]);
        _log?.LogInformation("BuyLevel: {player} level's {level}. Gold Removed: {gold}, exp to next Level: {nextLevel}", player.Name, GetCurrentPlayerData().Level, rule.GoldToLevelPrice[GetCurrentPlayerData().Level - 2], rule.PiecesPerLevel[GetCurrentPlayerData().Level - 1]);
        return true;
    }

    
    public bool GiveGoldAndExp(int gold, int exp){
        if(playersData.Count() < 1){
            _log?.LogWarning("GiveGoldAndExp: fail to add gold & exp, player less than 1");
            return false;
        }
        foreach (var item in playersData)
        {
            item.Value.IncreaseExp(exp);
            item.Value.AddGold(gold);
        }
        _log?.LogInformation("GiveGoldAndExp: gold & exp added to {players}", playersData.Count());
        return true;
    }

    
    public bool AutoAttack(ref IBattleLogger pieceLog){
        // dictionary ngga pasti urut
        if(arena.GetPlayersAndPieces().Count < 2){
            _log?.LogWarning("AutoAttack: fail players is less than 2");
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
        _log?.LogInformation("AutoAttack: first player's pieces attack all second player's pieces");
        foreach (var piece2 in player2Pieces)
        {
            foreach (var piece1 in player1Pieces)
            {
                piece1.GetDamage(piece2.AttackPoint);
                pieceLog.AddLog(player2, piece2, piece1);
            }
        }
        _log?.LogInformation("AutoAttack: second player's pieces attack all first player's pieces");
        return true;
    }
    
    public bool RemoveDeadPieces(){
        if(arena.GetPlayersAndPieces().Count <= 0){
            _log?.LogWarning("RemoveDeadPieces: fail to execute method: player is less than 1");
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
        _log?.LogInformation("RemoveDeadPieces: All dead pieces removed from board");
        return true;
    }
}




