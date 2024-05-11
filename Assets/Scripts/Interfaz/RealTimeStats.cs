
using System.Globalization;
using TMPro;
using UnityEngine;

public class RealTimeStats : MonoBehaviour
{

    //Textos de la interfaz:
    public Jugador jugador;

    public TextMeshProUGUI scoreText; //text que indica los puntos del jugador
    public TextMeshProUGUI distanciaRecorridaText; //text que indica la distancia recorrida.
    public TextMeshProUGUI alturaJugadorText; //text que indica la altura del jugador.
    public TextMeshProUGUI velocidadJugadorText; //text que indica la velocidad del jugador.
    public TextMeshProUGUI totalPointsText; //text que indica los puntos totales acumulados del jugador.


    public void UpdateRealTimeText()
    {
        //Actualiza informacion de la interfaz:
        scoreText.text = jugador.playerStats.Puntuacion.ToString("N0", new CultureInfo("es-ES")); //puntuacion
        totalPointsText.text = jugador.playerStats.PuntuacionTotal.ToString("N0", new CultureInfo("es-ES")); //puntuación total del jugador.
        distanciaRecorridaText.text = ((int)jugador.GetDistanciaRecorrida()).ToString("N0", new CultureInfo("es-ES")); //distancia
        alturaJugadorText.text = ((int)jugador.jugadorInstancia.position.y + 4).ToString("N0", new CultureInfo("es-ES")); //altura
        velocidadJugadorText.text = ((int)jugador.jugadorInstancia.velocity.x).ToString("N0", new CultureInfo("es-ES")); //velocidad
    }

}
