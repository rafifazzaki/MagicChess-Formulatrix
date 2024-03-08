namespace MagicChess;



public class BattleArena : IBattleArena
{
    public string Name {get; private set;}
    public int[,] BoardPosition {get; private set;}

    public BattleArena(string name, int maxBoard){
        // check if not even then throw errors
        Name = name;
        BoardPosition = new int[maxBoard,maxBoard];
    }

}

public interface IBattleArena{
    string Name {get;}
    int[,] BoardPosition {get;}
}