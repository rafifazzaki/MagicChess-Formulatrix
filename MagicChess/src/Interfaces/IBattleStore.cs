namespace MagicChess;

public interface IBattleStore{
    int PiecesToShow {get;}
    List<IPiece> Pieces {get;}

    // IEnumerable<IPiece> ShowStore();
    // bool ShuffleDeck();
}