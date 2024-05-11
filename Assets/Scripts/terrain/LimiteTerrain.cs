
public class LimiteTerrain
{
    
    public int limitTerrain_1; //limite del terreno 1
    public int limitTerrain_2; //limite del terreno 2
    public int limitTerrain_3; //limite del terreno 3
    public int limiteActualMinimo; //limite actual minimo
    public int limiteActual; //limite actual maximo(Default 5000)

    //Constructor de los limites del mapa
    private LimiteTerrain() {
        limitTerrain_1 = 5000;
        limitTerrain_2 = 25000;
        limitTerrain_3 = 100000;
        limiteActual = limitTerrain_1;
        limiteActualMinimo = 0;
    }


    private static LimiteTerrain instance;

    // Acceso a la instancia única
    public static LimiteTerrain Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LimiteTerrain();
            }
            return instance;
        }
    }

}
