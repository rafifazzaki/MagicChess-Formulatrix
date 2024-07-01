// public abstract class Synergy : ISynergy
// {
//     public string Name {get; protected set;}
//     protected int pieceNeeded; 
//     public abstract void ExecuteEffect(PieceClass pieceClass, out object effect);
// }

public abstract class Synergy{    
    public int Count {get; protected set;}

    public Synergy(){
        Count = 0;
    }

    public bool AddCount(){
        Count += 1;
        return true;
    }

    public bool ResetCount(){
        Count = 0;
        return true;
    }

}


