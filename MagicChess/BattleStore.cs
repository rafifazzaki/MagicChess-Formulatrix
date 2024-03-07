namespace MagicChess;

public class BattleStore : IBattleStore
{
    public List<IPiece> Pieces {get;}
    public int PiecesToShow {get; private set;}

    public BattleStore(int piecesToShow, List<IPiece> pieces){
        PiecesToShow = piecesToShow;
        Pieces = pieces;
    }

    // public bool ShuffleDeck(){
    //     Util.Shuffle(Pieces);
    //     return true;
    // }

    // public IEnumerable<IPiece> ShowStore(){
    //     var pieces = Pieces.Take(PiecesToShow);
    //     return pieces;
    // }


    

}

public interface IBattleStore{
    int PiecesToShow {get;}
    List<IPiece> Pieces {get;}

    // IEnumerable<IPiece> ShowStore();
    // bool ShuffleDeck();
}