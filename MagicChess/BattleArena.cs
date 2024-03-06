namespace MagicChess;



public class BattleArena : IBattleArena
{
    public int[,] MaxBoard {get; private set;}

    public BattleArena(int maxBoard){
        MaxBoard = new int[maxBoard,maxBoard];
    }

}

public interface IBattleArena{
    int[,] MaxBoard {get;}
}