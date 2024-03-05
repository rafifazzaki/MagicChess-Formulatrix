namespace MagicChess;

public class Player : IPlayer
{
    public int ID {get;}
    public string Name {get;}
    public int GameLevel {get; private set;}
    public int GameExp {get; private set;}

    public Player(int id, string name){
        ID = id;
        Name = name;
        GameLevel = 0;
        GameExp = 0;
    }
}

public interface IPlayer{
    int ID {get;}
    string Name {get;}
    int GameLevel {get;}
    int GameExp {get;}
}