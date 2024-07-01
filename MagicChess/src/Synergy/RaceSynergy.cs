public class RaceSynergy : Synergy
{
    public Races Race {get;}
    public static int AddHealth(Races races){
        int AddHealth = 0;
       
        switch(races) 
        {
        case Races.Pandaman:
            AddHealth = 2;
            break;
        case Races.Human:
            // code block
            AddHealth = 2;
            break;
        case Races.Feathered:
            // code block
            AddHealth = 1;
            break;
        case Races.Demon:
            // code block
            AddHealth = 1;
            break;
        case Races.Cave:
            // code block
            AddHealth = 3;
            break;
        default:
            // code block
            break;
        }
        return AddHealth;
    }
}
