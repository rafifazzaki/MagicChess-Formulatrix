using System.Text.Json;
namespace MagicChess;
#region payTheirDue
// https://stackoverflow.com/questions/273313/randomize-a-listt
#endregion

public static class Util
{
    private static Random rng = new Random();
    /// <summary>
    /// shuffle a list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    /// <summary>
    /// deserialize pieces from file
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<Piece> DeserializePieces(string path){
        string result;
        using(StreamReader sr = new(path)) 
		{
			result = sr.ReadToEnd();
		}
		List<Piece> pieces = JsonSerializer.Deserialize<List<Piece>>(result);
        return pieces;
    }
    /// <summary>
    /// deserialize rule from file
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Rule DeserializeRule(string path){
        string result;
        using(StreamReader sr = new(path)) 
		{
			result = sr.ReadToEnd();
		}
		Rule rule = JsonSerializer.Deserialize<Rule>(result);
		return rule;
    }
    /// <summary>
    /// parse input from string to have the choice of the player and then coordinates of the board,
    /// used for getting the piece and assign it to the board
    /// </summary>
    /// <param name="input"></param>
    /// <param name="choice"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool ParseInputXY(string input, out string choice, out int x, out int y)
    {
        x = 0;
        y = 0;
        choice = "";

        string[] numbersString = input.Split(' ');

        int num1, num2, num3;

        if (numbersString.Length == 3 &&
            int.TryParse(numbersString[0], out num1) &&
            int.TryParse(numbersString[1], out num2) &&
            int.TryParse(numbersString[2], out num3))
        {
            // get the appropriate piece number
            // PlayersData[gc.CurrentPlayer].Pieces
            choice = num1.ToString();
            x = num2;
            y = num3;
            return true;
        }
        return false;
    }

    static void SerializeTestRule(){
        
        int MaxLevel = 5;
        int[] GoldToLevelPrice = new int[5]{0, 3, 4, 6, 7};
        int[] ExpNeedForLevel = new int[5] {3, 5, 8, 12, 17};
        int[] PiecesPerLevel = new int[5] {3, 4, 5, 6, 7};

        Rule rule = new Rule(MaxLevel, GoldToLevelPrice, ExpNeedForLevel, PiecesPerLevel);
		
		string json = JsonSerializer.Serialize(rule);
		using(StreamWriter sw = new("rule.json")) 
		{
			sw.Write(json);
		}
    }
    static void SerializeTestPieces(){

		List<Piece> pieces = new();
        pieces.Add(new Piece("Red Axe 1", 1, 7, 1, 2, 2, 2, 3, AttackType.WideFront, Races.Cave, PieceClass.Warrior));
        pieces.Add(new Piece("CaptainSpark 1", 1, 5, 0, 1, 2, 2, 3, AttackType.Straight, Races.Human, PieceClass.Hunter));
        pieces.Add(new Piece("Pandoo 1", 1, 3, 0, 1, 1, 1, 3, AttackType.Area, Races.Pandaman, PieceClass.Mage));
        pieces.Add(new Piece("ShamanOfDesert 1", 2, 4, 0, 3, 2, 2, 3, AttackType.Area, Races.Cave, PieceClass.Mage));
        pieces.Add(new Piece("SoulReaper 1", 2, 4, 0, 3, 2, 2, 3, AttackType.Area, Races.Demon, PieceClass.Warlock));
        pieces.Add(new Piece("StonePanda", 2, 6, 3, 2, 2, 2, 3, AttackType.WideFront, Races.Pandaman, PieceClass.Mage));
        pieces.Add(new Piece("Unicorn 1",1, 5, 3, 2, 2, 2, 3, AttackType.Straight, Races.Feathered, PieceClass.Hunter));


		string json = JsonSerializer.Serialize(pieces);
		using(StreamWriter sw = new("pieces.json")) 
		{
			sw.Write(json);
		}
    }
    
    public class NumberInputException : Exception
    {
        public NumberInputException(string message) : base(message)
        {

        }
    }
}
