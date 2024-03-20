namespace MagicChess;

public class BattleStore : IBattleStore
{
     
    List<IPiece> pieces;
    public int PiecesToShow {get; private set;}

    public BattleStore(int piecesToShow, List<IPiece> pieces){
        PiecesToShow = piecesToShow;
        this.pieces = pieces;
    }
    /// <summary>
    /// get pieces from the store
    /// </summary>
    /// <returns></returns>
    public List<IPiece> GetPieces(){
        return pieces;
    }

}

