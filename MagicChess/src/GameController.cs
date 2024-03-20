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
    /// <summary>
    /// Get battleLogger
    /// </summary>
    /// <returns>
    /// returns BattleLogger to displays:
    /// players and pieces that attacking, and whos being attacked (piece and player)
    /// and what piece that has died
    /// </returns>
    public IBattleLogger GetBattleLogger(){
        return battleLogger;
    }
    /// <summary>
    /// Get the rule of the game such as how many exp and gold do the players get at a certain levels
    /// </summary>
    /// <returns></returns>
    public IRule GetRule(){
        return rule;
    }
    /// <summary>
    /// get current player of the game
    /// </summary>
    /// <returns></returns>
    public IPlayer GetCurrentPlayer(){
        return currentPlayer;
    }
    /// <summary>
    /// returns all of the player that was inside the turn
    /// </summary>
    /// <returns></returns>
    public IPlayer[] GetPlayersTurn(){
        return PlayersTurn;
    }
    /// <summary>
    /// get BattleStore
    /// </summary>
    /// <returns></returns>
    public IBattleStore GetStore(){
        return store;
    }
    /// <summary>
    /// Get BattleArena
    /// </summary>
    /// <returns></returns>
    public IBattleArena GetArena(){
        return arena;
    }
    /// <summary>
    /// Get All player's Data
    /// </summary>
    /// <returns></returns>
    public Dictionary<IPlayer, IPlayerData> GetPlayersData(){
        return playersData;
    }
    /// <summary>
    /// get specifics player data using IPlayer parameter
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IPlayerData GetPlayerData(IPlayer player){
        return playersData[player];
    }
    #endregion

    #region GetMethod
    /// <summary>
    /// Get current player data
    /// </summary>
    /// <returns></returns>
    public IPlayerData GetCurrentPlayerData(){
        return playersData[currentPlayer];
    }
    /// <summary>
    /// Get all assigned pieces from a player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
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
    /// <summary>
    /// get all unassigned pieces from a player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
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

    /// <summary>
    /// get how max a player can assign according to rule
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public int CurrentMaxAssignPiece(IPlayer player){
        return rule.PiecesPerLevel[GetCurrentPlayerData().Level - 1];
    }
    #endregion
    /// <summary>
    /// get logger
    /// </summary>
    /// <returns></returns>
    public ILogger<GameController>? GetLogger(){
        return _log;
    }

    #region Checks
    /// <summary>
    /// checks if any of the players die
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// check if there is any player die, return player who wins
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Get player's pieces from an index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IPiece GetPlayerPiece(int index){
        IPiece piece = playersData[currentPlayer].GetPieces()[index - 1];
        _log?.LogInformation("GetPlayerPiece: {pieces} from {player}", piece.Name, currentPlayer);
        return piece;
    }
    /// <summary>
    /// GetPieces from the store
    /// </summary>
    /// <returns></returns>

    public IEnumerable<IPiece> GetPieces(){
        _log?.LogInformation("GetPieces: GetPieces from store {pieces}", store.GetPieces().Count());
        return store.GetPieces();
    }
    /// <summary>
    /// set current player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool SetCurrentPlayer(IPlayer player){
        // validasi player if already in dict
        _log?.LogInformation("SetCurrentPlayer: player is {player}", currentPlayer.Name);
        currentPlayer = player;
        return true;
    }

    /// <summary>
    /// get piece on store according to var: PieceToShow
    /// </summary>
    /// <returns></returns>
    public List<IPiece> PieceOnStore(){
        
        // if(shuffle) {
        //     Util.Shuffle(store.GetPieces());
        // }
        //take the first 5 list, if PieceToshow is 5
        // ienumerable not need tolist

        _log?.LogInformation("PieceOnStore: get from store {number}", store.PiecesToShow);
        return store.GetPieces().Take(store.PiecesToShow).ToList(); 
    }
    /// <summary>
    /// contains logic for the next turn
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool NextTurn(IPlayer player){
        // validation
        // set it as current
        int index = Array.IndexOf(PlayersTurn, player);
        int nextIndex = (index + 1) % PlayersTurn.Length; //modulo for making the last index = 0. thx chatGPT
        _log?.LogInformation("NextTurn: from {player} to {nextPlayer}", PlayersTurn[nextIndex]);
        currentPlayer = PlayersTurn[nextIndex];
        return true;
    }
    /// <summary>
    /// for setting if the game has ended
    /// </summary>
    /// <returns></returns>
    public bool SetGameEnded(){
        IsGameEnded = true;
        _log?.LogInformation("SetGameEnded called");
        return true;
    }
    /// <summary>
    /// buy pieces from the store
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <returns></returns>
    public bool BuyPiece(IPlayer player, IPiece piece){
        // check if the player can buy
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


    
    /// <summary>
    /// Check if the player has maxed their levels
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsLevelMaxed(IPlayer player){
        if (GetPlayerData(player).Level >= rule.MaxLevel)
        {
            _log?.LogInformation("IsLevelMaxed: {player}'s level is sufficient", GetCurrentPlayerData().Level);
            return true;
        }
        _log?.LogWarning("IsLevelMaxed: cannot level up. player's level: {level}, rule.Maxlevel: {}", GetCurrentPlayerData().Level, rule.MaxLevel);
        return false;
    }

    /// <summary>
    /// for checking if the player can level up
    /// </summary>
    /// <param name="player"></param>
    /// <returns>
    /// how much exp needed to level up
    /// </returns>
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
    /// <summary>
    /// Buy level's if the player met the requirement
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool BuyLevel(IPlayer player){
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

    /// <summary>
    /// give gold and exp to all players in playerData
    /// </summary>
    /// <param name="gold"></param>
    /// <param name="exp"></param>
    /// <returns></returns>
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

    /// <summary>
    /// AutoAttack will looping from player 1 piece's attacking each pieces of the player 2,
    /// then vice versa
    /// </summary>
    /// <param name="pieceLog"></param>
    /// <returns></returns>
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
    /// <summary>
    /// remove dead pieces if any of the pieces current hp is 0 or less
    /// </summary>
    /// <returns></returns>
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




