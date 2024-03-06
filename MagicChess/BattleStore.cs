namespace MagicChess;

public class BattleStore : IBattleStore
{
    public int PiecesToShow => throw new NotImplementedException();


    public IPiece BuyPiece()
    {
        throw new NotImplementedException();
    }

}

public interface IBattleStore{
    IPiece BuyPiece();
    int PiecesToShow {get;}
}