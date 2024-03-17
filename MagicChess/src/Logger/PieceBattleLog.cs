namespace MagicChess;

public class PieceBattleLog : ILogger{
    public int Number {get;} = 0;
    public string Name {get;}
    List<IPlayer> attackerPlayer;
    List<IPiece> attackerPieces;
    List<IPiece> damagedPieces;
    List<DeadPieceInfo> deadPieceInfos;
    public int Turns {get; private set;} = 0; 
    


    public PieceBattleLog() : this("Battle Log") {}
    public PieceBattleLog(string name){
        attackerPlayer = new();
        attackerPieces = new();
        damagedPieces = new();
        Name = name;
        Number++;
    }

    public List<IPlayer> GetAttackerPlayer(){
        return attackerPlayer;
    }
    public List<IPiece> GetAttackerPieces(){
        return attackerPieces;
    }
    public List<IPiece> GetDamagedPieces(){
        return damagedPieces;
    }

    public bool AddLog(IPlayer attackerPlayer, IPiece attackerpiece, IPiece damagedPiece){
        if(attackerpiece == null || damagedPiece == null){
            return false;
        }
        this.attackerPlayer.Add(attackerPlayer);
        attackerPieces.Add(attackerpiece);
        damagedPieces.Add(damagedPiece);
        Turns++;
        return true;
    }
    public bool AddDeadPieces(IPlayer player, IPiece piece){
        if(piece == null){
            return false;
        }
        Console.WriteLine(player.Name, piece.Name);
        deadPieceInfos.Add(new DeadPieceInfo(player, piece, Turns));
        Turns++;
        return true;
    }
}