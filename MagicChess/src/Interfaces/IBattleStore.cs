namespace MagicChess;

public interface IBattleStore{
    /// <summary>
    /// how many piece to show
    /// </summary>
    int PiecesToShow {get;}
    /// <summary>
    /// get pieces from the store
    /// </summary>
    /// <returns></returns>
    public List<IPiece> GetPieces();

    // IEnumerable<IPiece> ShowStore();
    // bool ShuffleDeck();
}