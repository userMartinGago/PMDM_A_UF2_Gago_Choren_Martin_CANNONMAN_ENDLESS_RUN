using UnityEngine;

public class Aerodinamica
{

    //Clase que gestiona y maneja la aerodinamica del jugador.

    public float Base { get; set; } //velocidad base
    public int Level { get; set; } //nivel de la velocidad
    private readonly int CosteBase = 400;
    public long CosteTotal { get; set; } //coste total aumenta un 5% por cada nivel

    public float Desaceleracion { get { return Base - (Level * 0.1f); } } //calculo de la velocidad mejorada

    public Aerodinamica(float basee, int level)
    {
        Base = basee;
        Level = level;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Calcula el CosteTotal en el constructor
    }

    public void Upgrade()
    {
        Level += 1;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level));
    }

    //Funcion que genera una ralentización del usuario en base a la resistencia del viento
    //Si el valor de la desaceleracion es negativo el jugador comenzará a acelerar en lugar de desacelerar. (A partir del nivel 80 de aerodinamica)
    public void AplicarAerodinamica(Rigidbody2D rigibodyJugador)
    {
        float desaceleracionLogaritmica = Mathf.Log(rigibodyJugador.velocity.x + 1) * (Mathf.Abs(Desaceleracion) * 0.003f);
        if (rigibodyJugador.velocity.x > 0) //se comprueba que la velocidad sea positiva para evitar errores.
        {
            if (Desaceleracion < 0) //El jugador aumenta su velocidad ligeramente
            {
                rigibodyJugador.velocity = new Vector2(Mathf.Lerp(rigibodyJugador.velocity.x, rigibodyJugador.velocity.x + desaceleracionLogaritmica * 15, Time.deltaTime), rigibodyJugador.velocity.y);
            }
            else //Se aplica efecto de ralentización
            {
                rigibodyJugador.velocity = new Vector2(Mathf.Lerp(rigibodyJugador.velocity.x, 0, desaceleracionLogaritmica * Time.deltaTime), rigibodyJugador.velocity.y);
            }
        }
    }

}
