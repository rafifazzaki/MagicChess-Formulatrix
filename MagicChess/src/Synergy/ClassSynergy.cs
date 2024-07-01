public class ClassSynergy //: Synergy
{
    public static int AddDamage(PieceClass pieceClass){
        int DamageToAdd = 0;
        switch(pieceClass) 
        {
        case PieceClass.Knight:
            DamageToAdd = 3;
            break;
        case PieceClass.Warlock:
            DamageToAdd = 1;
            break;
        case PieceClass.Mage:
            // code block
            DamageToAdd = 1;
            break;
        case PieceClass.Warrior:
            // code block
            DamageToAdd = 3;
            break;
        case PieceClass.Hunter:
            // code block
            DamageToAdd = 2;
            break;
        default:
            // code block
            break;
        }
        return DamageToAdd;
    }
}