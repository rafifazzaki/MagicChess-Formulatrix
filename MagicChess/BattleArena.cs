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

    public bool TargetInRange(IPiece piece, IPiece target){
        // check 
        int x = piece.CurrentPosition.Item1;
        int y = piece.CurrentPosition.Item2;
        int xTarget = target.CurrentPosition.Item1;
        int yTarget = target.CurrentPosition.Item2;

        // Define the positions of piece and target
        int[] piecePosition = { x, y };
        int[] targetPosition = { xTarget, yTarget };

        // Calculate the difference in positions
        int diffRow = targetPosition[0] - piecePosition[0];
        int diffCol = targetPosition[1] - piecePosition[1];

        if (diffRow > 0)
                piecePosition[0]++; // Move down
        else if (diffRow < 0)
            piecePosition[0]--; // Move up

        if (diffCol > 0)
            piecePosition[1]++; // Move right
        else if (diffCol < 0)
            piecePosition[1]--; // Move left

        return false;
    }

    public (int, int) GetAdjacentObject(IPiece piece, List<KeyValuePair<IPiece, IPlayer>> piecesQueue){
        // check 
        int x = piece.CurrentPosition.Item1;
        int y = piece.CurrentPosition.Item2;
        int[] position = { x, y };

        int[,] directions = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } }; // Up, down, right, left

here
        foreach (var dir in directions)
        {
            int newRow = position[0] + dir[0];
            int newCol = position[1] + dir[1];
            int[] newPos = { newRow, newCol };

            // if (gridObjects.ContainsKey(newPos))
            // {
            //     return gridObjects[newPos];
            // }
        }
        return (0, 0);
    }

    public bool AreAdjacentPos(int[] pos1, int[] pos2)
    {
        int diffRow = Math.Abs(pos1[0] - pos2[0]);
        int diffCol = Math.Abs(pos1[1] - pos2[1]);
        return (diffRow <= 1 && diffCol <= 1);
    }

    // method bool areAdjacent()
    public bool AreAdjacent(IPiece piece, IPiece target)
    {
        int[] piecePosition = { piece.CurrentPosition.Item1, piece.CurrentPosition.Item2 };
        int[] targetPosition = { target.CurrentPosition.Item1, target.CurrentPosition.Item2 };

        int diffRow = Math.Abs(piecePosition[0] - targetPosition[0]);
        int diffCol = Math.Abs(piecePosition[1] - targetPosition[1]);
        return (diffRow <= 1 && diffCol <= 1);

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

    public bool TargetInRange(IPiece piece, IPiece target);
    public bool AreAdjacent(IPiece piece, IPiece target);
    
}