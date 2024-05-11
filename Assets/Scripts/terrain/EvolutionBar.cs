
using UnityEngine;
using UnityEngine.UI;

public class EvolutionBar : MonoBehaviour
{

    public Slider slider;
    private Jugador jugador; //instancia de jugador para llamar al método y obtener la distancia reocorrida por el mismo.
    private LimiteTerrain limiteTerrain;

    // Start is called before the first frame update
    void Start()
    {
        jugador = Jugador.Instance; //Se crea una instancia de jugador 
        limiteTerrain = LimiteTerrain.Instance;
        //Se establecen los valores minimos/maximos y valor del slider
        slider.minValue = limiteTerrain.limiteActualMinimo;
        slider.maxValue = limiteTerrain.limiteActual;
        slider.value = (float) jugador.GetDistanciaRecorrida();
    }

    void Update(){
        slider.value = (float) jugador.GetDistanciaRecorrida();
        //slider.minValue = limiteTerrain.limiteActualMinimo;
        //slider.maxValue = limiteTerrain.limiteActual;
    }

}
