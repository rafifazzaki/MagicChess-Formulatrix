namespace MagicChess;

public class Player : IPlayer
{
    public string Name {get;}

    public Player(string name){
        Name = name;
    }
}



public interface IPlayer{
    string Name {get;}
}