namespace MagicChess;

public class DeadPieceInfo{
    IPlayer player;
    IPiece piece;
    public int turns {get; private set;}

    public DeadPieceInfo(IPlayer player, IPiece piece, int turns)
    {
        this.player = player;
        this.piece = piece;
        this.turns = turns;
    }
    public IPlayer GetPlayer(){
        return player;
    }
    public IPiece GetPiece(){
        return piece;
    }
}