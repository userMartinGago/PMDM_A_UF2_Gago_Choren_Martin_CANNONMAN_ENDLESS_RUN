
using UnityEngine;

public class ManageTerrain : MonoBehaviour
{
    //Clase controladora del terreno en el que se encuentra el jugador.

    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    public Sprite[] sprites; // Lista de sprites para cambiar atendiendo al nivel. Contiene la imagen del terreno de los diferentes niveles
    private ManageTerrain[] manageTerrains; //Gestiona las capas de terreno del jugador. Contiene las capas de [sky, mountains, closeMountains, clouds y tree]
    private int indice = 0; //Indice (nivel) en el que se encuentra actualmente el jugador.
    private int ultimoCambio = 0; //Ultima puntuacion del jugador

    private LimiteTerrain limiteTerrain; //Constructor que contiene la información de los límites del mapa
    private EvolutionBar evolutionBar;

    void Start()
    {
        manageTerrains = FindObjectsOfType<ManageTerrain>(); //Inicializa y obtiene todas las referencias de los terrenos a modificar 
        evolutionBar = GameObject.FindWithTag("ProgressBarTerrain").GetComponent<EvolutionBar>();
        if (sprites.Length > 0)
        {
            // Inicializa el SpriteRenderer con el primer sprite
            spriteRenderer.sprite = sprites[indice];
            limiteTerrain = LimiteTerrain.Instance;
        }
    }

    // Función para cambiar el sprite del terreno por el siguiente nivel que corresponda.
    public void IncrementarIndice()
    {
        if (indice < sprites.Length - 1)
        {
            indice++;
            spriteRenderer.sprite = sprites[indice];
        }
    }

    //Funcion que selecciona todos las capas del terreno y los incrementa en +1. Esto provoca el cambio de fase del jugador
    void ChangeTerrain()
    {
        foreach (ManageTerrain manageTerrain in manageTerrains)
        {
            manageTerrain.IncrementarIndice();
        }
    }


    //Reseta el terreno
    public void ResetTerrain()
    {
        ultimoCambio = 0;

        // Recorre todas las instancias de ManageTerrain
        foreach (ManageTerrain manageTerrain in manageTerrains)
        {
            // Establece el indice de cada terreno a 0
            manageTerrain.indice = 0;

            // Se asegura de que el  sprite se actualiza para reflejar el cambio de indice
            if (manageTerrain.sprites.Length > 0)
            {
                manageTerrain.spriteRenderer.sprite = manageTerrain.sprites[manageTerrain.indice];
            }
        }
        evolutionBar.slider.minValue = 0; //limite minimo del slider
        evolutionBar.slider.maxValue = limiteTerrain.limitTerrain_1; //limite maximo del slider

    }

    // Realiza una comprobacion de la puntuación del jugador. Si la puntuación se corresponde con una nueva fase, se llamará a la función ChangeTerrain que incrementará
    // el valor del terreno en +1. Modifica la variable ultimoCambio para evitar que se actualice el terreno con puntuaciones previamente conseguidas.
    public void UpgradeTerreno(double distanciaRecorrida)
    {
        if (distanciaRecorrida >= limiteTerrain.limitTerrain_3 && ultimoCambio < limiteTerrain.limitTerrain_3)
        {
            // Incrementa el terreno cuando la distancia recorrida es mayor o igual a 90000
            ChangeTerrain();
            ultimoCambio = limiteTerrain.limitTerrain_3; //evita que pueda volver a darse esta situacion
            //Al ser el ultimo nivel no se alcanza ningun limite. La barra está al maximo tampoco se modifica el minimo
        }

        else if (distanciaRecorrida >= limiteTerrain.limitTerrain_2 && ultimoCambio < limiteTerrain.limitTerrain_2)
        {
            // Incrementa el terreno cuando la distancia recorrida es mayor o igual a 8000
            ChangeTerrain();
            ultimoCambio = limiteTerrain.limitTerrain_2; //evita que pueda volver a darse esta situacion
            evolutionBar.slider.maxValue = limiteTerrain.limitTerrain_3; //Se establece el nuevo limite maximo
            evolutionBar.slider.minValue = limiteTerrain.limitTerrain_2; //Se establece el nuevo limite minimo
        }
        else if (distanciaRecorrida >= limiteTerrain.limitTerrain_1 && ultimoCambio < limiteTerrain.limitTerrain_1)
        {
            // Incrementa el terreno cuando la distancia recorrida es mayor o igual a 2000
            ChangeTerrain();
            ultimoCambio = limiteTerrain.limitTerrain_1; //evita que pueda volver a darse esta situacion
            evolutionBar.slider.maxValue = limiteTerrain.limitTerrain_2; //Se establece el nuevo limite maximo
            evolutionBar.slider.minValue = limiteTerrain.limitTerrain_1; //Se establece el nuevo limite minimo
        }
    }
}
