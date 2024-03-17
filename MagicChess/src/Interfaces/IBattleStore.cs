namespace MagicChess;

public interface IBattleStore{
    int PiecesToShow {get;}
    public List<IPiece> GetPieces();

    // IEnumerable<IPiece> ShowStore();
    // bool ShuffleDeck();
}