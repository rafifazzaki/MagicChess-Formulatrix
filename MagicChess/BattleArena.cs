namespace MagicChess;



public class BattleArena : IBattleArena
{
    public string Name {get; private set;}
    public int MaxBoard {get; private set;}
    public IPiece[,] PiecesPosition {get; private set;}

    public BattleArena(string name, int maxBoard){
        if (maxBoard % 2 != 0)
            {
                throw new Util.NumberInputException("Number must be even");
            }
        if(maxBoard > 5){
            throw new Util.NumberInputException("maxBoard is more than 5");
        }
        Name = name;
        MaxBoard = maxBoard;
        PiecesPosition = new IPiece[maxBoard,maxBoard];
    }

    public bool SetPiecePosition(IPiece piece, int x, int y){
        // ngecek maksimal piece didalam board
        // ngecek apakah didalamnya sudah ada piece
        // assign IPiece
        if(x > MaxBoard || y > MaxBoard){
            return false;
        }
        if(PiecesPosition[x,y] != null){
            return false;
        }
        PiecesPosition[x,y] = piece;
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

public interface IBattleArena{
    string Name {get;}
    IPiece[,] PiecesPosition {get;}

    public bool SetPiecePosition(IPiece piece, int x, int y);
    public bool GetPieceAndPosition(GameController gc, string input, out IPiece? piece, out int x, out int y);
}