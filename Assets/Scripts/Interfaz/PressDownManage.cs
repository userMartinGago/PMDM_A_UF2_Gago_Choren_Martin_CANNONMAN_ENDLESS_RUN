using UnityEngine;
using UnityEngine.UI;

public class PressDownManage : MonoBehaviour
{
    public Jugador jugador; //Referencia al objeto jugador.
    public PressKey pressKeyDown; // Referencia al objeto que gestiona el gif de keydown
    public Image containerImage;
    
    // Update is called once per frame
    void Update()
    {
        if(jugador.IsOnRun()){
            pressKeyDown.gameObject.SetActive(true);
            containerImage.gameObject.SetActive(true);

            if(jugador.IsEfectoGenerado() || jugador.IsTocandoLimite()){
                //Si está con el efecto se cancela la animacion
                pressKeyDown.PararAnimacion();
            }
        }else{
            pressKeyDown.gameObject.SetActive(false);
        }
    }
}
