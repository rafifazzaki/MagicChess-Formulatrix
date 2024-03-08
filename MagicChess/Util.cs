namespace MagicChess;
#region payTheirDue
// https://stackoverflow.com/questions/273313/randomize-a-listt
#endregion

public static class Util
{
    private static Random rng = new Random();

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
    public static void ParseIntChar(string input, out int number, out char text){
        number = 0;
        text = ' ';

        string numberPart = input.Substring(0, input.Length - 1); // Extract the number part
            string charPart = input.Substring(input.Length - 1);     // Extract the character part

            // Parse the number part into an integer
            if (int.TryParse(numberPart, out int intResult))
            {
                number = intResult;
                text = charPart.ToCharArray()[0];
            }
            else
            {
                Console.WriteLine("Invalid input format.");
            }

        
    }
}
