namespace MagicChess;

public interface IBattleArena
{
    string Name { get; }

    public bool SetPiecePosition(IPlayer player, IPiece piece, int x, int y);
    // public bool GetPieceAndPosition(GameController gc, string input, out IPiece? piece, out int x, out int y);

    public bool RemovePieceFromBoard(IPlayer player, IPiece piece);

    public IPiece[,] GetPiecesPosition();

    public Dictionary<IPlayer, List<IPiece>> GetPlayersAndPieces();
    public IEnumerable<IPiece> GetPiecesByPlayer(IPlayer player);
    public bool IsCanAssign(int x, int y);
    
    public bool IsAnyPiecesEmpty(IPlayer[] playerTurns, out IPlayer playerLose);
    public bool isEnoughPlayer(IPlayer[] playersTurn, out IPlayer player);
}
