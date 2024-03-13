namespace MagicChess;



public class BattleArena : IBattleArena
{
    public string Name { get; private set; }
    public int MaxBoard { get; private set; }
    public IPiece[,] PiecesPosition { get; private set; }
    public Dictionary<IPiece, IPlayer> PiecePlayer { get; private set; }

    public List<IPiece> GetPiecesByPlayer(IPlayer player)
    {
        // Use LINQ to filter the dictionary based on the player and extract pieces
        List<IPiece> pieces = PiecePlayer.Where(kv => kv.Value == player).Select(kv => kv.Key).ToList();
        return pieces;
    }

    public BattleArena(string name, int maxBoard)
    {
        if (maxBoard % 2 != 0)
        {
            throw new Util.NumberInputException("Number must be even");
        }
        if (maxBoard > 5)
        {
            throw new Util.NumberInputException("maxBoard is more than 5");
        }
        Name = name;
        MaxBoard = maxBoard;
        PiecesPosition = new IPiece[maxBoard, maxBoard];
        PiecePlayer = new Dictionary<IPiece, IPlayer>();
    }

    public bool SetPiecePosition(IPlayer player, IPiece piece, int x, int y)
    {
        // check moard boundary
        if (x >= MaxBoard || y >= MaxBoard || x < 0 || y < 0)
        {
            return false;
        }
        // Check if already in PiecePlayer, if so change position
        if(PiecePlayer.ContainsKey(piece)){
            PiecesPosition[piece.CurrentPosition.Item1, piece.CurrentPosition.Item1] = null;
            piece.SetPosition((x, y));
            PiecesPosition[x, y] = piece;
            return false;
        }

        // add to PiecePlayer
        if(PiecesPosition[x, y] == null){
            PiecePlayer.Add(piece, player);
            piece.SetAssignedToTrue();
            piece.SetPosition((x, y));
            PiecesPosition[x, y] = piece;
            return true;
        }
        return false;
    }

    public bool RemovePieceFromBoard(IPiece piece){
        PiecesPosition[piece.CurrentPosition.Item1, piece.CurrentPosition.Item1] = null;
        PiecePlayer.Remove(piece);
        return true;
    }

    public bool ClearPlayerPieces()
    {
        PiecePlayer.Clear();
        return true;
    }

    public bool GetPieceAndPosition(GameController gc, string input, out IPiece? piece, out int x, out int y)
    {
        piece = null;
        x = 0;
        y = 0;

        string[] numbersString = input.Split(' ');

        int num1, num2, num3;

        if (numbersString.Length == 3 &&
            int.TryParse(numbersString[0], out num1) &&
            int.TryParse(numbersString[1], out num2) &&
            int.TryParse(numbersString[2], out num3))
        {
            // get the appropriate piece number
            // PlayersData[gc.CurrentPlayer].Pieces
            piece = gc.PlayersData[gc.CurrentPlayer].Pieces[num1 - 1];
            x = num2;
            y = num3;
            return true;
        }
        return false;
    }



}

public interface IBattleArena
{
    string Name { get; }
    IPiece[,] PiecesPosition { get; }

    public bool SetPiecePosition(IPlayer player, IPiece piece, int x, int y);
    public bool GetPieceAndPosition(GameController gc, string input, out IPiece? piece, out int x, out int y);

    public Dictionary<IPiece, IPlayer> PiecePlayer { get; }
    public bool RemovePieceFromBoard(IPiece piece);
    public List<IPiece> GetPiecesByPlayer(IPlayer player);
    
}