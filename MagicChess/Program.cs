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
        Player p2 = new(name2);
        GameController gc = new(arena, store, pieces, p1, p2);

        // TODO: Console log instanced game object
        // Check: pass Dictionary Player Data?
        
        #endregion
        
    }
}