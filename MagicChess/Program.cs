using MagicChess;

class Program
{
    static void Main()
    {
        #region Game Setup
        Console.WriteLine("input player 1 name:");
        string? name1 = Console.ReadLine();

        Console.WriteLine("input player 2 name:");
        string? name2 = Console.ReadLine();
        
        BattleArena arena = new(4);
        BattleStore store = new();

        List<IPiece> pieces = new();
        pieces.Add(new RedAxe());
        pieces.Add(new RedAxe());
        pieces.Add(new RedAxe());
        
        Player p1 = new(name1);
        PlayerData pd1 = new();
        Player p2 = new(name2);
        PlayerData pd2 = new();

        Dictionary<IPlayer, PlayerData> playersData = new();
        playersData.Add(p1, pd1);
        playersData.Add(p2, pd2);

        GameController gc = new(arena, store, pieces, playersData);

        // TODO: Console log instanced game object
        // Check: pass Dictionary Player Data?
        
        #endregion
        
    }
}