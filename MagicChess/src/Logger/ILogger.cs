namespace MagicChess;

public interface ILogger{
    int Number {get;}
    string Name {get;}
    public int Turns {get;}

    public List<IPlayer> GetAttackerPlayer();
    public List<IPiece> GetAttackerPieces();
    public List<IPiece> GetDamagedPieces();

    public bool AddDeadPieces(IPlayer player, IPiece piece);
    public bool AddLog(IPlayer attackerPlayer, IPiece attackerpiece, IPiece damagedPiece);
}