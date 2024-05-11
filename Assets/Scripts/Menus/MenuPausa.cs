
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

    [SerializeField] private GameObject interfazPrincipal;
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private Jugador jugador;
    private Boolean juegoPausado = false;

//Método para pausar el juego

public void Update(){
    if(Input.GetKeyDown(KeyCode.Escape)){
        if(juegoPausado){
            Resume();
        }else{
            Pausa();
        }
    }
}
public void Pausa(){
    juegoPausado = true;
    Time.timeScale = 0f;
    botonPausa.SetActive(false);
    menuPausa.SetActive(true);
    interfazPrincipal.SetActive(false);
}

public void Resume(){
    juegoPausado = false;
    Time.timeScale = 1f;
    botonPausa.SetActive(true);
    menuPausa.SetActive(false);
    interfazPrincipal.SetActive(true);
}

public void Reset(){
    juegoPausado = false;
    StartCoroutine(ReiniciarEscena(0.2f));
    Time.timeScale = 1f;
    
}

    IEnumerator ReiniciarEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        jugador.RestartMarcadores();
    }


public void QuitGame(){
    Time.timeScale = 1f; 
     StartCoroutine(SalirEscena(.2f));
}

    IEnumerator SalirEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
        
    }

}
