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

        BattleArena arena = new("Default", 4);


        List<IPiece> pieces = new();
        pieces.Add(new RedAxe("Axe 1"));
        pieces.Add(new RedAxe("Axe 2"));
        pieces.Add(new RedAxe("Axe 3"));

        BattleStore store = new(3, pieces);

        Player p1 = new(name1);
        PlayerData pd1 = new();
        Player p2 = new(name2);
        PlayerData pd2 = new();

        Dictionary<IPlayer, PlayerData> playersData = new();
        playersData.Add(p1, pd1);
        playersData.Add(p2, pd2);

        GameController gc = new(arena, store, playersData);

        #endregion

        string? answer = "";
        while (!gc.IsGameEnded)
        {
            foreach (var playerData in gc.PlayersData)
            {
                MainMenu(gc, playerData, answer);
            }
        }
    }

    static void MainMenu(GameController gc, KeyValuePair<IPlayer, PlayerData> playerData, string answer)
        {
            while (playerData.Key == gc.CurrentPlayer)
            {
                Console.Clear();
                Console.WriteLine($"Player: {playerData.Key.Name}");
                Console.WriteLine($"Gold: {playerData.Value.Gold}, Exp: {playerData.Value.Exp}, Lvl: {playerData.Value.Level}");
                Console.WriteLine("1. Info");
                Console.WriteLine("2. Assign");
                Console.WriteLine("3. Store");
                Console.WriteLine("4. Level up (not implemented)");
                Console.WriteLine("5. End Turn");
                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        Info(gc);
                        break;
                    case "2":
                        // code block
                        break;
                    case "3":
                        ShowStore(gc);
                        break;
                    case "4":
                        // code block
                        break;
                    case "5":
                        // code block
                        gc.NextTurn(gc.CurrentPlayer);
                        break;
                    default:
                        // code block
                        break;
                }
            }
        }

        static void Info(GameController gc)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Player: {gc.CurrentPlayer.Name}");
                Console.WriteLine(
                    $"Gold: {gc.PlayersData[gc.CurrentPlayer].Gold}, Exp: {gc.PlayersData[gc.CurrentPlayer].Exp}, Lvl: {gc.PlayersData[gc.CurrentPlayer].Level}"
                    );
                Console.WriteLine("===================================");
                Console.WriteLine("current Pieces: ");
                Console.WriteLine("===================================");
                int i = 1;
                foreach (var item in gc.PlayersData[gc.CurrentPlayer].Pieces)
                {
                    Console.WriteLine($"{i}. {item.Name}");
                    i++;
                }
                Console.WriteLine("1.Back");
                string answer = Console.ReadLine();

                if (answer == "1")
                {
                    break;
                }
            }

        }

        static void ShowStore(GameController gc)
        {
            while (true)
            {
                Console.Clear();
                int i = 1;

                foreach (var item in gc.PieceOnStore(false))
                {
                    Console.WriteLine(
                        $"{i}. {item.Name} (${item.Price}) :::: HP {item.HP}, ATK {item.AttackPoint}");
                    i++;
                }

                string answer = Console.ReadLine();

                if (int.TryParse(answer, out int choice) && choice >= 1 && choice <= gc.PieceOnStore(false).Count())
                {
                    Console.WriteLine($"You Bought: {gc.PieceOnStore(false)[choice - 1].Name}!");
                    gc.PlayersData[gc.CurrentPlayer].AddPiece(gc.PieceOnStore(false)[choice - 1]);
                    gc.store.Pieces.Remove(gc.PieceOnStore(false)[choice - 1]);
                    break;
                }
                else if (answer == i++.ToString())
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
                i = 0;
            }
        }
}