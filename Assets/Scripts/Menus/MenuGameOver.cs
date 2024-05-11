using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{

    public Jugador jugador; //Referencia al objeto jugador para obtener el estado general

    public TextMeshProUGUI puntuacionFinal;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private GameObject menuInterfazGlobal;
    [SerializeField] private GameObject menuEscape;
    
    //[SerializeField] private GameObject menuEstadoJugador;

    // Start is called before the first frame update

    public void SetFinalPuntuacion()
    {
        puntuacionFinal.text = jugador.playerStats.Puntuacion.ToString("N0", new CultureInfo("es-ES"));
    }
    public void ReiniciarPartida()
    {
        menuGameOver.SetActive(false);
        menuInterfazGlobal.SetActive(true); //activa las otras interfaces
        menuEscape.SetActive(true); //activa el menu de scape
        jugador.RestartGameRun(); //funcion que reinicia la partida
        Time.timeScale = 1;
        

    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(SalirEscena(.2f));
    }

    IEnumerator SalirEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }

    public void ActivarMenuGameOver(){
        Time.timeScale = 0;
        menuGameOver.SetActive(true);
        menuInterfazGlobal.SetActive(false); //desactiva las otras interfaces
        menuEscape.SetActive(false); //desactiva menu de escape
    }
}
