namespace MagicChess;

public class GameController
{
}

public class IAutoChessGameController{
    int BattleRound {get;}
    // int BattleTime {get;}
    int PreparationTime{get;}
    Dictionary<IPlayer, PlayerData> PlayersData;
    //List<IPiece> AllPieceList;
    // List<IPiece> PlayersList;
    // List<IPiece> ArenaTypeList;

    // kayaknya bakalan bikin 1 kelas buat handle semua pergerakan Ipiece
    // PieceLocation LocationMoveTo;
    // List<IPlayer> OpponentList
    // List<IPlayer> RoundWinnerList
    // List<IPlayer> RoundLoserList
    // Action<IPlayer, IPiece, IPiece> PieceAttackDefend

}

public class PlayerData{

}