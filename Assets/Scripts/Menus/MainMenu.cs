using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuTutorial;

    public void JugarPartida()
    {
        Debug.Log("Cambiando de escena");
        StartCoroutine(CambiarEscena(.2f));//Retraso de 0.2s para cambiar de escena
    }

    public void OpenTutorial()
    {
        menuTutorial.SetActive(true);
    }

    public void CerrarTutorial(){
        menuTutorial.SetActive(false);
    }


    IEnumerator CambiarEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Cerrando el juego");
    }

}
