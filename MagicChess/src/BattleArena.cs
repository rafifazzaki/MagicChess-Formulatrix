namespace MagicChess;


public class BattleArena : IBattleArena
{

    public string Name { get; private set; }
    public int MaxBoard { get; private set; }
    IPiece[,] PiecesPosition;// { get; private set; }
    Dictionary<IPlayer, List<IPiece>> playersAndPieces;// { get; private set; }

    /// <summary>
    /// get piece position
    /// </summary>
    /// <returns>
    /// IPiece[,]
    /// </returns>
    public IPiece[,] GetPiecesPosition(){
        return PiecesPosition;
    }
    /// <summary>
    /// Get list of player and their assigned pieces
    /// </summary>
    /// <returns></returns>
    public Dictionary<IPlayer, List<IPiece>> GetPlayersAndPieces(){
        return playersAndPieces;
    }

    /// <summary>
    /// get pieces by player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerable<IPiece> GetPiecesByPlayer(IPlayer player)
    {
        return playersAndPieces[player];
    }

    public BattleArena(string name, int maxBoard)
    {
        if (maxBoard % 2 != 0)
        {
            throw new Util.NumberInputException("Number must be even");
        }
        if (maxBoard > 5)
        {
            throw new Util.NumberInputException("maxBoard is more than 5");
        }
        Name = name;
        MaxBoard = maxBoard;
        PiecesPosition = new IPiece[maxBoard, maxBoard];
        playersAndPieces = new Dictionary<IPlayer, List<IPiece>>();
    }

    // restructure from dict<IPiece, IPlayer> to dict<IPlayer, List<IPiece>>
    /// <summary>
    /// for checking whether the position already saving a piece position or not
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsCanAssign(int x, int y){
        // check board boundary
        if (x >= MaxBoard || y >= MaxBoard || x < 0 || y < 0)
        {
            return false;
        }

        // check if position is not null
        if(PiecesPosition[x, y] != null){
            return false;
        }else if(PiecesPosition[x, y] == null){
            return true;
        }
        return false;
    }
    /// <summary>
    /// set a piece position
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool SetPiecePosition(IPlayer player, IPiece piece, int x, int y){
        bool found = false;
        // if the same piece already assigned, make the the initial position null
        foreach (var kvp in playersAndPieces)
        {
            if (kvp.Value.Contains(piece))
            {
                found = true;
                PiecesPosition[piece.CurrentPosition.Item1, piece.CurrentPosition.Item1] = null;
                break;
            }
        }
        // if there is not found, then add one
        if (!found)
        {
            playersAndPieces[player] = new List<IPiece>();
            playersAndPieces[player].Add(piece);
        }
        // piece saves isAssigned and currentPosition, and set in the PiecesPosition
        piece.SetAssignedToTrue();
        piece.SetPosition((x, y));
        PiecesPosition[x, y] = piece;
        return true;
    }
    /*public bool SetPiecePositionOri(IPlayer player, IPiece piece, int x, int y)
    {
        // check moard boundary
        if (x >= MaxBoard || y >= MaxBoard || x < 0 || y < 0)
        {
            return false;
        }
        // Check if already in playersAndPieces, if so change position
        if(playersAndPieces[player].Contains(piece)){
            PiecesPosition[piece.CurrentPosition.Item1, piece.CurrentPosition.Item1] = null;
            piece.SetPosition((x, y));
            PiecesPosition[x, y] = piece;
            return false;
        }
    
        // add to playersAndPieces
        if(PiecesPosition[x, y] == null){
            if (!playersAndPieces.ContainsKey(player))
                {
                    playersAndPieces[player] = new List<IPiece>();
                }
            playersAndPieces[player].Add(piece);
            // playersAndPieces.Add(player, dict[player1].Add(piece1););
            piece.SetAssignedToTrue();
            piece.SetPosition((x, y));
            PiecesPosition[x, y] = piece;
            return true;
        }
        return false;
    }*/

    /// <summary>
    /// remove pieces from board, and set the position back to null
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <returns></returns>
    public bool RemovePieceFromBoard(IPlayer player, IPiece piece){
        if(player == null || piece == null || piece.IsAssigned == false){
            return false;
        }
        PiecesPosition[piece.CurrentPosition.Item1, piece.CurrentPosition.Item1] = null;
        playersAndPieces[player].Remove(piece);
        
        return true;
    }

    /// <summary>
    /// Check if there is at least 2 players assign the piece to the board
    /// </summary>
    /// <param name="playersTurn"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsEnoughPlayer(IPlayer[] playersTurn, out IPlayer player){
        player = null;
        if(playersAndPieces.Count() < 2){
            var playersNotInDictionary = playersTurn.Except(playersAndPieces.Keys).ToArray();
            player = playersNotInDictionary[0];
            return false;
        }
        return true;
    }

    /// <summary>
    /// check if there is no pieces left in one of the players that ar assigned on the board
    /// </summary>
    /// <param name="playerTurns"></param>
    /// <param name="playerLose"></param>
    /// <returns></returns>
    public bool IsAnyPiecesEmpty(IPlayer[] playerTurns, out IPlayer playerLose){
        playerLose = null;
        bool isPlayerZeroPiece = false;

        

        foreach (var item in playersAndPieces)
        {
            
            foreach (var i in item.Value)
            {
                Console.WriteLine($"{item.Key.Name}: {i.Name}");
            }
        }
        
        foreach (var player in playersAndPieces)
        {
            Console.WriteLine("pieces inside player: " + playersAndPieces.Count());
            
            isPlayerZeroPiece = playersAndPieces.TryGetValue(player.Key, out List<IPiece> pieces) && pieces.Count <= 0;
            Console.WriteLine(isPlayerZeroPiece);

            if (isPlayerZeroPiece)
            {
                playerLose = player.Key;
                return true; // Player has zero pieces, return true and set playerLose
            }
        }
        return false;
    }

}

