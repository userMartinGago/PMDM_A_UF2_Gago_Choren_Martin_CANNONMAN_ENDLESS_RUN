using System.Collections;
using UnityEngine;

public class LauncherCharacter : MonoBehaviour
{
    public BarraPotencia barraPotencia; //Referencia al objeto Barra de potencia.
    private Jugador jugador; //Referencia al objeto jugador
    public PressKey pressSpace;
    public PressKey pressDownArrow;
    public Transform jugadorSpawnPoint; //punto desde el que se lanza el objeto

    private Rigidbody2D jugadorInstancia;
    private float lanzadorPotencia; //potencia del cañon => Esta propiedad viene dada por PlayerStats
    public AudioClip SoundLauncher;
    private AudioSource audioSource;
    public PressDownManage pulsadorImpulsoVertical;


    void Start()
    {
        jugador = Jugador.Instance;
        audioSource = GetComponent<AudioSource>(); //Instancia de audioSource del jugador
        jugadorInstancia = jugador.GetJugadorRigibody();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jugador.IsOnRun())
        {
            //Se detiene barra potencia:
            barraPotencia.isStoped = true;
            pressSpace.gameObject.SetActive(false);
            lanzadorPotencia = barraPotencia.GetValorPotencia(); //Se obtiene la potencia que ha conseguido el usuario
            // Asigna la velocidad al jugador existente
            Invoke("LaunchPlayer", 1f);

        }
    }

    void LaunchPlayer()
    {
        audioSource.clip = SoundLauncher;
        audioSource.Play();
        jugadorInstancia.velocity = jugadorSpawnPoint.up * lanzadorPotencia;
        jugador.SetOnRun(true);
        pulsadorImpulsoVertical.gameObject.SetActive(true); //activa la interfaz del impulso vertical
        StartCoroutine(StartAnimationWithDelay(1.0f)); // Inicia la animación con un retraso de 1 segundo
    }

    IEnumerator StartAnimationWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        pressDownArrow.IniciarAnimacion(); // Reinicia la animación de pulsar tecla abajo
    }

}
