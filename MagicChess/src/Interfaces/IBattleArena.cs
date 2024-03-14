namespace MagicChess;

public interface IBattleArena
{
    string Name { get; }
    IPiece[,] PiecesPosition { get; }

    public bool SetPiecePosition(IPlayer player, IPiece piece, int x, int y);
    public bool GetPieceAndPosition(GameController gc, string input, out IPiece? piece, out int x, out int y);

    public Dictionary<IPiece, IPlayer> PiecePlayer { get; }
    public bool RemovePieceFromBoard(IPiece piece);
    public List<IPiece> GetPiecesByPlayer(IPlayer player);
    
}
