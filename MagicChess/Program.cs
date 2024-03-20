using System.Text.Json;
using MagicChess;
using MagicChess.PieceChar;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
// check "TANYA"
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
    private static IEnumerable<object> piecesQueue;

    static void Main()
    {
        #region Game Setup
        Console.WriteLine("input player 1 name:");
        string? name1 = Console.ReadLine();

        Console.WriteLine("input player 2 name:");
        string? name2 = Console.ReadLine();

        BattleArena arena = new("Default", 4);


        List<Piece> deserialized = Util.DeserializePieces("pieces.json");
        List<IPiece> pieces = new();
        foreach (var item in deserialized)
        {
            IPiece convertedPiece = item;
            pieces.Add(convertedPiece);
        }

        ILoggerFactory loggerFactory = LoggerFactory.Create(log =>
		{
			log.SetMinimumLevel(LogLevel.Information);
			log.AddNLog("nlog.config");
		});

        ILogger<GameController> logger = loggerFactory.CreateLogger<GameController>();


        BattleStore store = new(5, pieces);
        Util.Shuffle(store.GetPieces());
        Util.Shuffle(store.GetPieces());

        Player p1 = new(name1);
        PlayerData pd1 = new();
        Player p2 = new(name2);
        PlayerData pd2 = new();

        Dictionary<IPlayer, IPlayerData> playersData = new();
        playersData.Add(p1, pd1);
        playersData.Add(p2, pd2);

        Rule? rule = Util.DeserializeRule("rule.json");//new Rule();
        
        if(rule == null){
            logger?.LogWarning("rule is null");
            return;
        }
        logger?.LogInformation("rule is deserialized");
        PieceBattleLog pieceBattleLog = new();

        GameController gc = new(arena, store, rule, playersData, pieceBattleLog, logger);

        #endregion

        string? answer = "";

        
        while (!gc.IsGameEnded)
        {
            // give winner
            foreach (var playerData in gc.GetPlayersData())
            {
                Console.Clear();
                MainMenu(gc, playerData, answer);
            }

            AutoBattle(gc);
            
            // TANYA: is this good to iterate twice? or compare IPlayer directly
            if(gc.IsAnyPlayerDie()){
                Console.WriteLine($"{gc.GetWinner()} Win!");
                break;
            }
        }
    }
    static void AutoBattle(GameController gc)
    {
        gc.GetLogger()?.LogInformation("Auto Battle Started");
        // get pieces from arena: playersAndPieces
        // List<KeyValuePair<IPiece, IPlayer>> piecesQueue = gc.arena.PiecePlayer.ToList<KeyValuePair<IPiece, IPlayer>>();
        Dictionary<IPlayer, List<IPiece>> playersAndPieces = gc.GetArena().GetPlayersAndPieces();
        int exp = 3;
        int gold = 2;
        bool isDone = false;
        while (isDone != true)
        { 
            // Initial HP per pieces
            // will fight:
            //(player): piece.name, currentHP

            foreach (var item in playersAndPieces)
            {
                foreach (var piece in item.Value)
                {
                    Console.WriteLine($"({item.Key.Name}): {piece.Name}, HP: {piece.CurrentHP}");    
                }
            }
// //       // // // // // // HERE: Check For Stars (add stats) 

            // if any players that has 0 piece

            if(!gc.RemoveDeadPieces()){
                Console.WriteLine("Players pieces less than 2");
                gc.GetLogger()?.LogInformation("Players pieces less than 2");
            }

            Console.WriteLine("in");

            // Check if one of the player not assign a piece
            if(!gc.GetArena().IsEnoughPlayer(gc.GetPlayersTurn(), out IPlayer player)){
                isDone = true;
                gc.GetPlayerData(player).GetDamage(playersAndPieces.Count);
                    
                    Console.WriteLine($"{player.Name} Got {playersAndPieces.Count} damage!");
                    Console.WriteLine($"[[Round Ended]]");
                    if(gc.GiveGoldAndExp(gold, exp)){
                        Console.WriteLine($"[[Each Player Got Gold: {gold}, Exp: {exp}]]");
                    }
                    Console.ReadLine();
            }

            if(gc.GetArena().IsAnyPiecesEmpty(gc.GetPlayersTurn(), out IPlayer playerLose)){
                    isDone = true;
                    
                    // this gives playerLose null, if a player not assign it's piece
                    gc.GetPlayerData(playerLose).GetDamage(playersAndPieces.Count);
                    
                    Console.WriteLine($"{playerLose.Name} Got {playersAndPieces.Count} damage!");
                    Console.WriteLine($"[[Round Ended]]");
                    if(gc.GiveGoldAndExp(gold, exp)){
                        Console.WriteLine($"[[Each Player Got Gold: {gold}, Exp: {exp}]]");
                    }
                    Console.ReadLine();
                    
                    break;
            }
            Console.WriteLine("out");
            Console.ReadLine();

            if(isDone == true){
                gc.GetLogger()?.LogInformation("Auto Battle Ended", gc.GetCurrentPlayer());
                break;
            }
            Console.WriteLine("Both player still has piece(s)");


            // check if the player is not same, if yes then attack it
            if(gc.AutoAttack(ref gc.battleLogger)){
                
                for (int i = 0; i < gc.battleLogger.Turns; i++)
                {
                    Console.WriteLine(gc.battleLogger.GetAttackerPieces()[i]);
                    Console.WriteLine(gc.battleLogger.GetAttackerPlayer()[i]);
                    Console.WriteLine(gc.battleLogger.GetDamagedPieces()[i]);
                    // $"([player]): [damagedPiece] got [damage] damage by [attacker], HP left: [damagedHP] "
                    Console.WriteLine(
                        $"({gc.battleLogger.GetAttackerPlayer()[i]}): " +
                        $"{gc.battleLogger.GetDamagedPieces()[i]} " +
                        $"got {gc.battleLogger.GetAttackerPieces()[i].AttackPoint} dmg " +
                        $"by {gc.battleLogger.GetAttackerPieces()[i]}, " +
                        $"HP left: {gc.battleLogger.GetDamagedPieces()[i].CurrentHP}"
                    );
                    Console.WriteLine("then..");

                }
            }


            // Check if there is piece that has current HP 0, if yes, remove it
            // for (int i = piecesQueue.Count - 1; i >= 0; i--)
            // {
            //     var item = piecesQueue[i];
            //     if (item.Key.CurrentHP <= 0)
            //     {
            //         Console.WriteLine($"{piecesQueue[i].Key} ({piecesQueue[i].Key.CurrentHP}) removed");
            //         gc.arena.RemovePieceFromBoard(piecesQueue[i].Key);
            //         piecesQueue[i].Key.ResetCurrentHP();
            //         piecesQueue[i].Key.ResetAssigned();
            //         piecesQueue.RemoveAt(i);
            //     }
            // }

            Console.ReadLine();
        }
    }


    static void MainMenu(GameController gc, KeyValuePair<IPlayer, IPlayerData> playerData, string answer)
    {
        gc.GetLogger()?.LogInformation("{player} accessing Main menu", gc.GetCurrentPlayer());
        while (playerData.Key == gc.GetCurrentPlayer())
        {
            Console.Clear();
            Console.WriteLine($"Player: {playerData.Key.Name}");
            Console.WriteLine($"HP: {playerData.Value.HP}, Gold: {playerData.Value.Gold}, Exp: {playerData.Value.Exp}");
            
            Console.WriteLine($"Lv: {playerData.Value.Level}, Piece Assigned: {gc.GetPlayerAssignedPieces(gc.GetCurrentPlayer()).Count()}/{gc.GetCurrentPlayerData().MaxAssign}");

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
                    break;
                case "5":
                    CheckBoard(gc);
                    break;
                case "6":
                    SellPiece(gc);
                    break;
                case "7":
                    EndTurn(gc);
                    Util.Shuffle(gc.GetStore().GetPieces());
                    break;
                default:
                    // code block
                    break;
            }
        }
    }

    public static void SellPiece(GameController gc)
    {
        gc.GetLogger()?.LogInformation("{player} accessing SellPiece menu", gc.GetCurrentPlayer());
        while (true)
        {
            Console.Clear();
            int i = 1;

            foreach (var item in gc.GetCurrentPlayerData().GetPieces())
            {
                Console.WriteLine(
                    $"{i}. {item.Name} (${item.Price}) :::: HP {item.HP}, ATK {item.AttackPoint}");
                i++;
            }
            Console.WriteLine($"{i}. Back");
            
            string answer = Console.ReadLine();

            if (answer == i++.ToString())
            {
                break;
            }
            
            else if (int.TryParse(answer, out int choice) && choice >=1)
            {
                Console.WriteLine(choice + "<-choice");
                IPiece piece = gc.GetCurrentPlayerData().GetPieces()[choice - 1];
                gc.GetStore().GetPieces().Add(piece);
                gc.GetCurrentPlayerData().AddGold(piece.Price);
                gc.GetCurrentPlayerData().GetPieces().Remove(piece);
                if(!gc.GetArena().RemovePieceFromBoard(gc.GetCurrentPlayer(), piece)){
                    Console.WriteLine("Piece Removed from board");
                }
                Console.WriteLine($"{piece.Name} is Sold!");
                Console.ReadLine();
                break;
            }
        }
    }

    static void BuyLevel(GameController gc)
    {
        gc.GetLogger()?.LogInformation("{player} accessing BuyLevel menu", gc.GetCurrentPlayer());
        // if player level > exp & gold is sufficient
        // can level up with gold
        // add assign on roundData
        Console.Clear();
        
        string answer = "";
        // while(true){
        // get array of number on exp needed
        bool isCanLevelUp = false;
        int expToLevelUp = 0;
        foreach (var item in gc.GetRule().ExpNeedForLevel)
        {
            if (gc.GetCurrentPlayerData().Exp >= item)
            {
                isCanLevelUp = true;
                expToLevelUp = item;
                break;
            }
        }

        if (gc.GetCurrentPlayerData().Level >= gc.GetRule().MaxLevel)
        {
            Console.WriteLine("Your Level is Maxed Out");
            Console.ReadLine();
            return;
        }

        Console.WriteLine($"EXP: {gc.GetCurrentPlayerData().Exp}, Exp for Level Up: {expToLevelUp}");
        if (isCanLevelUp)
        {
            // store
            Console.WriteLine("Your Exp is Sufficient to level up!");
            Console.WriteLine($"Gold Needed: {gc.GetRule().GoldToLevelPrice[gc.GetCurrentPlayerData().Level - 1]}");
            Console.WriteLine("Level Up?");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. Back");
            answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                    
                    if(gc.IsLevelMaxed(gc.GetCurrentPlayer())){
                        Console.WriteLine("Your Level is Maxed Out");
                        break;    
                    }

                    if(gc.BuyLevel(gc.GetCurrentPlayer())){
                        Console.WriteLine("Level Up Successful!");
                        Console.WriteLine($"Current Level: {gc.GetCurrentPlayerData().Level}");
                        Console.WriteLine("Now You can assign up to: " + gc.CurrentMaxAssignPiece());
                    }
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
        gc.GetLogger()?.LogInformation("{player} turn ended");
        gc.NextTurn(gc.GetCurrentPlayer());
    }

    static void CheckBoard(GameController gc)
    {
        gc.GetLogger()?.LogInformation("{player} accessing CheckBoard menu", gc.GetCurrentPlayer());
        string? answer;
        // while (true)
        // {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("Board: ");
            Console.WriteLine("===================================");


            int rows = gc.GetArena().GetPiecesPosition().GetLength(0);
            int cols = gc.GetArena().GetPiecesPosition().GetLength(1);

            Console.WriteLine($"{rows}, {cols}");

            for (int j = 0; j < rows; j++)
            {
                for (int k = 0; k < cols; k++)
                {
                    // Console.Write(gc.arena.PiecesPosition[j, k].Name + " ");
                    if (gc.GetArena().GetPiecesPosition()[j, k] != null)
                    {
                        Console.WriteLine($"[{j}, {k}] : {gc.GetArena().GetPiecesPosition()[j, k].Name}");
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
        gc.GetLogger()?.LogInformation("{player} accessing Info menu", gc.GetCurrentPlayer());
        string? answer;
        while (true)
        {
            Console.Clear();
            
            Console.WriteLine($"Player: {gc.GetCurrentPlayer().Name}");
            Console.WriteLine(
                $"Gold: {gc.GetCurrentPlayerData().Gold}, Exp: {gc.GetCurrentPlayerData().Exp}, Lvl: {gc.GetCurrentPlayerData().Level}"
                );
            Console.WriteLine("===================================");
            Console.WriteLine("current Pieces: ");
            Console.WriteLine("===================================");
            int i = 1;

            foreach (var item in gc.GetPlayerAssignedPieces(gc.GetCurrentPlayer()))
            {
                Console.WriteLine($"{i}. {item.Name} (is Assigned At: [{item.CurrentPosition.Item1}, {item.CurrentPosition.Item2}])");
                i++;
            }
            foreach (var item in gc.GetPlayerUnassignedPieces(gc.GetCurrentPlayer())){
                Console.WriteLine($"{i}. {item.Name}");
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
        gc.GetLogger()?.LogInformation("{player} accessing AssignPieces menu", gc.GetCurrentPlayer());
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
            foreach (var item in gc.GetPlayerAssignedPieces(gc.GetCurrentPlayer()))
            {
                Console.WriteLine($"{i}. {item.Name} (is Assigned At: [{item.CurrentPosition.Item1}, {item.CurrentPosition.Item2}])");
                i++;
            }
            foreach (var item in gc.GetPlayerUnassignedPieces(gc.GetCurrentPlayer())){
                Console.WriteLine($"{i}. {item.Name}");
                i++;
            }
            Console.WriteLine($"{i}. Back");
            answer = Console.ReadLine();

            // check and parse from answer
            if(!Util.ParseInputXY(answer, out string choice, out int x, out int y)){
                Console.WriteLine("Invalid Input");
                Console.ReadLine();
                break;
            }

            if(int.TryParse(choice, out int answerNumber) && answerNumber > i){
                break;
            }

            Console.WriteLine($"answer: {answer}, choice: {choice}, x: {x}, y: {y}");
            Console.ReadLine();

            // check if it can be assigned there
            if(!gc.GetArena().IsCanAssign(x, y))
            {
                Console.WriteLine("Other piece already assigned in those position");
                break;
                
            }
            
            // get piece from index
            IPiece pieceFromIndex = gc.GetPlayerPiece(int.Parse(choice));
            // check if if can be assigned, input position
            Console.WriteLine("current player: " + gc.GetCurrentPlayer()); //DEBUG
            Console.ReadLine();
            if(gc.GetArena().SetPiecePosition(gc.GetCurrentPlayer(), pieceFromIndex, x, y)){
                Console.WriteLine("Pieces Assigned");
                Console.ReadLine();
                break;
            }

            if (answer == $"{i}")
            {
                i = 0;
                break;
            }
            
        }
    }

    static void ShowStore(GameController gc)
    {
        gc.GetLogger()?.LogInformation("{player} accessing Store menu", gc.GetCurrentPlayer());
        List<IPiece> pieces = gc.PieceOnStore();
        while (true)
        {
            Console.Clear();
            int i = 1;
            // GetPieceOnStore
            foreach (var item in pieces)
            {
                Console.WriteLine(
                    $"{i}. {item.Name} (${item.Price}) :::: HP {item.HP}, ATK {item.AttackPoint}");
                i++;
            }
            Console.WriteLine($"{i}. Back");
            string answer = Console.ReadLine();

            
            // if not the answer will throw an error

            if (answer == i.ToString())
            {
                break;
            }

            if (!int.TryParse(answer, out int choice) || choice < 1 || choice > pieces.Count()){
                Console.WriteLine("Invalid choice");
                Console.ReadLine();
                break;
            }
            // here
            Console.ReadLine();
            Console.WriteLine("choice: "+choice);
            if(gc.BuyPiece(gc.GetCurrentPlayer(), pieces[choice-1])) 
            {
                Console.WriteLine($"You Bought: {pieces[choice - 1].Name}!");
                Console.ReadLine();
                break;
            }else{
                Console.WriteLine("Insufficient Gold");
                Console.ReadLine();
            }

            /*if (int.TryParse(answer, out int choice) && choice >= 1 && choice <= pieces.Count())
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
            */
        }
    }
}