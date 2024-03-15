using MagicChess;
using MagicChess.PieceChar;
/*
To Do:
    -Deserialization
	-Stars
	-Synergy

Minor Bug:
    outside of board still not handled properly?
    check if the PlayerTurn is just a copy of playersData (dict)?
    check commented "CHECK HERE"

To Be Done: 
    Alternating between 2 player
    Prototype Design Pattern

Later Feature: 
    -position based attack
	-Range based attack
    -UI
    -Lock store

For The Future:
    -Change tuple to class instead, because tuple is immutable (Piece CurrentPosition)

Balancing:
    -Add more gold to more cheap piece
*/

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

        #region add piece
        pieces.Add(new RedAxe("Axe 1"));
        pieces.Add(new RedAxe("Axe 2"));
        pieces.Add(new RedAxe("Axe 3"));
        pieces.Add(new RedAxe("Axe 4"));

        pieces.Add(new StonePanda("StonePanda 1"));
        pieces.Add(new StonePanda("StonePanda 2"));
        pieces.Add(new StonePanda("StonePanda 3"));
        pieces.Add(new StonePanda("StonePanda 4"));

        pieces.Add(new Unicorn("Unicorn 1"));
        pieces.Add(new Unicorn("Unicorn 2"));
        pieces.Add(new Unicorn("Unicorn 3"));
        pieces.Add(new Unicorn("Unicorn 4"));

        pieces.Add(new SoulReaper("SoulReaper 1"));
        pieces.Add(new SoulReaper("SoulReaper 2"));
        pieces.Add(new SoulReaper("SoulReaper 3"));
        pieces.Add(new SoulReaper("SoulReaper 4"));

        pieces.Add(new ShamanOfDesert("ShamanOfDesert 1"));
        pieces.Add(new ShamanOfDesert("ShamanOfDesert 2"));
        pieces.Add(new ShamanOfDesert("ShamanOfDesert 3"));
        pieces.Add(new ShamanOfDesert("ShamanOfDesert 4"));

        pieces.Add(new Pandoo("Pandoo 1"));
        pieces.Add(new Pandoo("Pandoo 2"));
        pieces.Add(new Pandoo("Pandoo 3"));
        pieces.Add(new Pandoo("Pandoo 4"));

        pieces.Add(new CaptainSpark("CaptainSpark 1"));
        pieces.Add(new CaptainSpark("CaptainSpark 2"));
        pieces.Add(new CaptainSpark("CaptainSpark 3"));
        pieces.Add(new CaptainSpark("CaptainSpark 4"));
        #endregion

        BattleStore store = new(5, pieces);
        Util.Shuffle(store.Pieces);
        Util.Shuffle(store.Pieces);

        Player p1 = new(name1);
        PlayerData pd1 = new();
        Player p2 = new(name2);
        PlayerData pd2 = new();

        Dictionary<IPlayer, IPlayerData> playersData = new();
        playersData.Add(p1, pd1);
        playersData.Add(p2, pd2);

        Rule rule = new Rule();

        GameController gc = new(arena, store, rule, playersData);

        #endregion

        string? answer = "";
        while (!gc.IsGameEnded)
        {
            // give winner
            foreach (var playerData in gc.playersData)
            {
                Console.Clear();
                MainMenu(gc, playerData, answer);
            }

            AutoBattle(gc);

            foreach (var item in gc.PlayersTurn)
            {
                if (gc.playersData[item].HP <= 0)
                {
                    gc.SetGameEnded();
                }
            }
        }

    }

    static void AutoBattle(GameController gc)
    {
        List<KeyValuePair<IPiece, IPlayer>> piecesQueue = gc.arena.PiecePlayer.ToList<KeyValuePair<IPiece, IPlayer>>();
        bool isDone = false;
        while (isDone != true)
        { // loop is where one of players pieces is empty
            // Check Initial HP per pieces
            foreach (var item in piecesQueue)
            {
                Console.WriteLine($"{item.Value.Name}: {item.Key.Name}, HP: {item.Key.CurrentHP}");
            }
// // // // // // // // HERE: Check For Stars (add stats) 
            // loop to check Ipiece, count 

            // Check if there was a player that doesn't have pieces left
            foreach (var item in gc.PlayersTurn)
            {
                bool hasPlayer = piecesQueue.Any(kvp => kvp.Value == item);
                if (!hasPlayer)
                {
                    isDone = true;
                    Console.WriteLine($"{item.Name} Got {piecesQueue.Count} damage!");
                    gc.playersData[item].GetDamage(piecesQueue.Count);

                    Console.WriteLine("break");
                    Console.ReadLine();
                    break;
                }
            }
            
            if (isDone)
            {
                // piecesQueue.Clear();
                foreach (var item in gc.PlayersTurn)
                {
                    gc.playersData[item].AddGold(2); //CHECK HERE, can be more flexible
                    gc.playersData[item].IncreaseExp(3);
                }
                piecesQueue = null;
                break;
            }
            Console.ReadLine();
            Console.WriteLine("Both player still has piece(s)");

            // check if the player is not same, if yes then attack it
            foreach (KeyValuePair<IPiece, IPlayer> kvp in piecesQueue)
            {
                foreach (var item in piecesQueue)
                {
                    // if the player is not the same
                    if (kvp.Value != item.Value)
                    {
                        // damage pieces that has 
                        item.Key.GetDamage(kvp.Key.AttackPoint);
                        Console.WriteLine($"{kvp.Value.Name}: {kvp.Key.Name} Attacking {item.Key.Name} With {kvp.Key.AttackPoint} damage, {item.Key.Name}'s HP: {item.Key.CurrentHP}");
                        Console.WriteLine("then..");
                        break;
                    }
                }
            }

            // Check if there is piece that has current HP 0, if yes, remove it
            for (int i = piecesQueue.Count - 1; i >= 0; i--)
            {
                var item = piecesQueue[i];
                if (item.Key.CurrentHP <= 0)
                {
                    Console.WriteLine($"{piecesQueue[i].Key} ({piecesQueue[i].Key.CurrentHP}) removed");
                    gc.arena.RemovePieceFromBoard(piecesQueue[i].Key);
                    piecesQueue[i].Key.ResetCurrentHP();
                    piecesQueue[i].Key.ResetAssigned();
                    piecesQueue.RemoveAt(i);
                }
            }

            Console.ReadLine();
        }
    }


    static void MainMenu(GameController gc, KeyValuePair<IPlayer, IPlayerData> playerData, string answer)
    {
        while (playerData.Key == gc.currentPlayer)
        {
            
            Console.Clear();
            Console.WriteLine($"Player: {playerData.Key.Name}");
            Console.WriteLine($"HP: {playerData.Value.HP}, Gold: {playerData.Value.Gold}, Exp: {playerData.Value.Exp}");
            
            Console.WriteLine($"Lv: {playerData.Value.Level}, Piece Assigned: {gc.arena.GetPiecesByPlayer(gc.currentPlayer).Count}/{gc.GetCurrentPlayerData().MaxAssign}");

            Console.WriteLine("1. Info");
            Console.WriteLine("2. Assign");
            Console.WriteLine("3. Store");
            Console.WriteLine("4. Level up");
            Console.WriteLine("5. Check Board");
            Console.WriteLine("6. Sell Piece");
            Console.WriteLine("7. End Turn");
            // Console.WriteLine(gc.arena.PiecePlayer.Count);
            answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    Info(gc);
                    break;
                case "2":
                    AssignPieces(gc);
                    break;
                case "3":
                    ShowStore(gc);
                    break;
                case "4":
                    BuyLevel(gc);
                    // code block
                    break;
                case "5":
                    // code block
                    CheckBoard(gc);
                    // Console.Clear();
                    break;
                case "6":
                    // code block
                    SellPiece(gc);
                    break;
                case "7":
                    // code block
                    EndTurn(gc);
                    break;
                default:
                    // code block
                    break;
            }
        }
    }

    public static void SellPiece(GameController gc)
    {
        while (true)
        {
            Console.Clear();
            int i = 1;

            foreach (var item in gc.GetCurrentPlayerData().pieces)
            {
                Console.WriteLine(
                    $"{i}. {item.Name} (${item.Price}) :::: HP {item.HP}, ATK {item.AttackPoint}");
                i++;
            }
            Console.WriteLine($"{i}. Back");

            string answer = Console.ReadLine();

            if (int.TryParse(answer, out int choice) && choice >= 1 && choice <= gc.GetCurrentPlayerData().pieces.Count())
            {
                IPiece piece = gc.GetCurrentPlayerData().pieces[choice - 1];
                gc.store.Pieces.Add(piece);
                gc.GetCurrentPlayerData().AddGold(piece.Price);
                gc.GetCurrentPlayerData().pieces.Remove(piece);
                Console.WriteLine($"{piece.Name} is Sold!");
                Console.ReadLine();
                break;
            }
            else if (answer == i++.ToString())
            {
                break;
            }
            i = 0;
        }
    }

    static void BuyLevel(GameController gc)
    {
        // if player level > exp & gold is sufficient
        // can level up with gold
        // add assign on roundData
        Console.Clear();
        bool isCanLevelUp = false;
        int expToLevelUp = 0;
        string answer = "";
        // while(true){
        // get array of number on exp needed
        foreach (int item in gc.rule.ExpNeedForLevel)
        {
            if (gc.GetCurrentPlayerData().Exp >= item)
            {
                isCanLevelUp = true;
                expToLevelUp = item;
                break;
            }
        }
        Console.WriteLine($"EXP: {gc.GetCurrentPlayerData().Exp}, Exp for Level Up: {expToLevelUp}");

        if (gc.GetCurrentPlayerData().Level >= Rule.MaxLevel)
        {
            Console.WriteLine("Your Level is Maxed Out");
            Console.ReadLine();
            return;
        }

        if (isCanLevelUp)
        {
            // store
            Console.WriteLine("Your Exp is Sufficient to level up!");
            Console.WriteLine($"Gold Needed: {gc.rule.GoldToLevelPrice[gc.GetCurrentPlayerData().Level - 1]}");
            Console.WriteLine("Level Up?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. Back");
            answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                    // buy
                    // CHECK HERE, check maximum level

                    // check if player's level more than 1, then check gold
                    if (gc.GetCurrentPlayerData().Level > 1)
                    {
                        int tempGold = gc.GetCurrentPlayerData().Gold - gc.rule.GoldToLevelPrice[gc.GetCurrentPlayerData().Level - 1];
                        Console.WriteLine($"tempGold: {gc.GetCurrentPlayerData().Gold} - {gc.rule.GoldToLevelPrice[gc.GetCurrentPlayerData().Level - 1]} = {tempGold}");
                        if (tempGold < 0)
                        {
                            Console.WriteLine("Insuffient Gold");
                            Console.ReadLine();
                            break;
                        }
                    }


                    gc.GetCurrentPlayerData().IncreaseLevel();
                    gc.GetCurrentPlayerData().SetCurrentMaxAssign(gc.rule.PiecesPerLevel[gc.GetCurrentPlayerData().Level - 1]);
                    gc.GetCurrentPlayerData().RemoveGold(gc.rule.GoldToLevelPrice[gc.GetCurrentPlayerData().Level - 2]);

                    Console.WriteLine("Level Up Successful!");
                    Console.WriteLine($"Current Level: {gc.GetCurrentPlayerData().Level}");
                    Console.WriteLine("Now You can assign up to: " + gc.rule.PiecesPerLevel[gc.GetCurrentPlayerData().Level - 1]);
                    Console.ReadLine();

                    break;
                case "2":

                    break;
                default:

                    break;
            }
        }else{
            Console.WriteLine("Your Exp is insufficient");
            Console.ReadLine();
        }

    }

    static void EndTurn(GameController gc)
    {
        // assign pieces to PiecesPerRound
        // gc.SetPiecesToFight(gc.CurrentPlayer, )

        gc.NextTurn(gc.currentPlayer);
    }

    static void CheckBoard(GameController gc)
    {
        string? answer;
        // while (true)
        // {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Board: ");
            Console.WriteLine("===================================");


            int rows = gc.arena.PiecesPosition.GetLength(0);
            int cols = gc.arena.PiecesPosition.GetLength(1);

            Console.WriteLine($"{rows}, {cols}");

            for (int j = 0; j < rows; j++)
            {
                for (int k = 0; k < cols; k++)
                {
                    // Console.Write(gc.arena.PiecesPosition[j, k].Name + " ");
                    if (gc.arena.PiecesPosition[j, k] != null)
                    {
                        Console.WriteLine($"[{j}, {k}] : {gc.arena.PiecesPosition[j, k].Name}");
                    }
                    else
                    {
                        Console.WriteLine($"[{j}, {k}] : ");
                    }

                }
                Console.WriteLine(); // Move to the next line for the next row
            }
            Console.WriteLine("===================================");
            Console.WriteLine("1. back");
            answer = Console.ReadLine();
            if (answer == "1")
            {
                Console.Clear();
                return;
            }
            Console.Clear();
        // }
    }

    static void Info(GameController gc)
    {
        string? answer;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Player: {gc.currentPlayer.Name}");
            Console.WriteLine(
                $"Gold: {gc.GetCurrentPlayerData().Gold}, Exp: {gc.GetCurrentPlayerData().Exp}, Lvl: {gc.GetCurrentPlayerData().Level}"
                );
            Console.WriteLine("===================================");
            Console.WriteLine("current Pieces: ");
            Console.WriteLine("===================================");
            int i = 1;
            // check if item is assigned

            // if(gc.IsPieceAssigned(item)){

            // }else{
                
            // }
            // is assigned
            // get assigned piece in player
            foreach (var item in gc.GetPlayerAssignedPieces(gc.currentPlayer))
            {
                
            }
            foreach (IPiece item in gc.GetCurrentPlayerData().GetPieces())
            {
                if (item.IsAssigned)
                {
                    Console.WriteLine($"{i}. {item.Name} (is Assigned At: [{item.CurrentPosition.Item1}, {item.CurrentPosition.Item2}])");
                }
                else
                {
                    Console.WriteLine($"{i}. {item.Name}");
                }
                i++;
            }
            Console.WriteLine("===================================");
            Console.WriteLine("1.Back");
            answer = Console.ReadLine();

            if (answer == "1")
            {
                break;
            }
        }

    }

    static void AssignPieces(GameController gc)
    {
        string? answer;
        int i;
        while (true)
        {
            i = 1;
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Assign Piece:");
            Console.WriteLine("FORMAT: Piece(space)X_Axis(space)Y_Axis");
            Console.WriteLine("EXAMPLE: \"1 0 0\"");
            Console.WriteLine("===================================");
            foreach (var item in gc.GetCurrentPlayerData().pieces)
            {
                // BUG, the second not showing???
                if (item.IsAssigned)
                {
                    Console.WriteLine($"{i}. {item.Name} (is Assigned At: [{item.CurrentPosition.Item1}, {item.CurrentPosition.Item2}])");
                }
                else
                {
                    Console.WriteLine($"{i}. {item.Name}");
                }

                i++;
            }
            Console.WriteLine($"{i}. Back");
            answer = Console.ReadLine();

            if (
                gc.arena.GetPieceAndPosition(gc, answer,
                 out IPiece piece, out int x, out int y)
            )
            {

                // Check If you can still Assign a piece (still below limit)
                gc.arena.SetPiecePosition(gc.currentPlayer, piece, x, y);
                Console.WriteLine("Pieces Assigned");
                Console.ReadLine();
                break;
            }
            else if (answer == $"{i}")
            {
                i = 0;
                break;
            }
            else
            {
                Console.WriteLine("Invalid format");
                Console.ReadLine();
            }
        }
    }

    static void ShowStore(GameController gc)
    {
        List<IPiece> pieces = gc.PieceOnStore(false);
        Util.Shuffle(gc.store.Pieces);
        while (true)
        {
            Console.Clear();
            int i = 1;

            foreach (var item in pieces)
            {
                Console.WriteLine(
                    $"{i}. {item.Name} (${item.Price}) :::: HP {item.HP}, ATK {item.AttackPoint}");
                i++;
            }
            Console.WriteLine($"{i}. Back");

            string answer = Console.ReadLine();
      
            if (int.TryParse(answer, out int choice) && choice >= 1 && choice <= pieces.Count())
            {
                // Check if player's Gold is sufficient
                if (gc.GetCurrentPlayerData().Gold >= pieces[choice - 1].Price)
                {
                    Console.WriteLine($"You Bought: {pieces[choice - 1].Name}!");
                    gc.GetCurrentPlayerData().AddPiece(pieces[choice - 1]);
                    gc.store.Pieces.Remove(pieces[choice - 1]);
                    gc.GetCurrentPlayerData().RemoveGold(pieces[choice - 1].Price);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Insufficient Gold");
                }

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
            Console.ReadLine();
            i = 0;
        }
    }
}