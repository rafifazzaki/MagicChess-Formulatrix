namespace MagicChess;

public interface IBattleArena
{
    string Name { get; }

    /// <summary>
    /// set a piece position
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool SetPiecePosition(IPlayer player, IPiece piece, int x, int y);
    // public bool GetPieceAndPosition(GameController gc, string input, out IPiece? piece, out int x, out int y);


    /// <summary>
    /// remove pieces from board, and set the position back to null
    /// </summary>
    /// <param name="player"></param>
    /// <param name="piece"></param>
    /// <returns></returns>
    public bool RemovePieceFromBoard(IPlayer player, IPiece piece);

    /// <summary>
    /// get all piece position as an IPiece[,]
    /// </summary>
    /// <returns>
    /// IPiece[,]
    /// </returns>
    public IPiece[,] GetPiecesPosition();


    /// <summary>
    /// Get list of player and their assigned pieces
    /// </summary>
    /// <returns></returns>
    public Dictionary<IPlayer, List<IPiece>> GetPlayersAndPieces();


    /// <summary>
    /// get pieces by player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public IEnumerable<IPiece> GetPiecesByPlayer(IPlayer player);


    // restructure from dict<IPiece, IPlayer> to dict<IPlayer, List<IPiece>>
    /// <summary>
    /// for checking whether the position already saving a piece position or not
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsCanAssign(int x, int y);

    /*
    
    /// <summary>
    /// check if there is no pieces left in one of the players that ar assigned on the board
    /// </summary>
    /// <param name="playerTurns"></param>
    /// <param name="playerLose"></param>
    /// <returns></returns>
    public bool IsAnyPiecesEmpty(IPlayer[] playerTurns, out IPlayer playerLose);
    */

    /// <summary>
    /// Check if there is at least 2 players assign the piece to the board
    /// </summary>
    /// <param name="playersTurn"></param>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsEnoughPlayer(IPlayer[] playersTurn, out IPlayer player);
}
