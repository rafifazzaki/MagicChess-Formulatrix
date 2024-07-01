public interface ISynergy{
    public string Name {get;}
    public abstract void ExecuteEffect(PieceClass pieceClass, out object effect);
}