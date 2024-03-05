namespace MagicChess;



public class BattleArena : IBattleArena
{
    public int ID {get; private set;}
    public string Name {get; private set;}
}

public interface IBattleArena{
    int ID {get;}
    string Name {get;}
}
