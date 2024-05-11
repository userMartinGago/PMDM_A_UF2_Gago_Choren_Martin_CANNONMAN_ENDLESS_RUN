using UnityEngine;
using UnityEngine.UI;

public class BarraPotencia : MonoBehaviour
{

    public Gradient gradiente;
    public Slider slider;
    public PressKey pressSpace;
    private PlayerStats jugadorStatsInstancia; //Se llama al objeto PlayerStats que contiene las estadisticas del jugador
    private float velocidadBarra; //Velocidad a la que se mueve la barra. Dicha velocidad tiene que ser proporcional al atributo velocidad del jugador
    private bool barraSubiendo = true;
    public bool isStoped = false;




    private void Start()
    {
        // Obtiene la instancia única del jugador
        jugadorStatsInstancia = PlayerStats.Instance;
        pressSpace.IniciarAnimacion();
        SetMinMaxSpeedValueSlider();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStoped)
        {
            if (barraSubiendo)
            {
                slider.value += velocidadBarra * Time.deltaTime;
                if (slider.value >= slider.maxValue) // Si la barra alcanza el máximo
                {
                    slider.value = slider.maxValue;
                    barraSubiendo = false;
                }
            }
            else
            {
                slider.value -= (velocidadBarra * 1.3f) * Time.deltaTime; //Para añadir mas dificultad, la velocidad de bajada de la barra es un 30% más rápida.
                if (slider.value <= slider.minValue) // Si la barra alcanza el mínimo
                {
                    slider.value = slider.minValue;
                    barraSubiendo = true;
                }
            }
        }
    }


    //Reinicia la barraPotencia
    public void ReiniciarBarraPotencia()
    {
        isStoped = false;
        pressSpace.gameObject.SetActive(true);
        pressSpace.StartCoroutine(pressSpace.AnimateGif());
        SetMinMaxSpeedValueSlider(); //Establece los nuevos valores minimos y maximos del slider.
    }

    //Esta funcion establece cuales serán los valores minimos y maximos a los cuales podrá ser lanzado el jugador
    public void SetMinMaxSpeedValueSlider()
    {
        slider.maxValue = jugadorStatsInstancia.GetVelocidad(); //El valor maximo que puede alcanzar el jugador viene dado por la instancia de jugador
        slider.minValue = jugadorStatsInstancia.GetVelocidad() * 0.4f; //La velocidad minima a la que podrá ser lanzado el jugador será el 40% de la maxima
        velocidadBarra = jugadorStatsInstancia.GetVelocidad() * 3f; //Conforme los valores del slider aumentan, debemos aumentar la velocidad proporcional del slider.
        jugadorStatsInstancia.UpdateVelocidadMaxima(slider.maxValue); //establece la velocidad maxima como el mayor valor del slider.
    }

    public float GetValorPotencia()
    {
        return slider.value;
    }


}
