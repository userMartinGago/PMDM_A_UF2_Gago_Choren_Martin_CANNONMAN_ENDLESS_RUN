
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public JugadorFunctions jugadorFunctions;
    public Rigidbody2D jugadorInstancia; //Rigibody del jugador
    public GameObject limiteInferior; //Suelo del juego.
    public PressKey pulsadorGif; //Gif de pulsar espacio
    public PressKey pulsadorImpulsoVertical; // Gestor gif impulso vertical
    public RealTimeStats realTimeStats; //Referencia del objeto de estadisticas en tiempo real (Distancia recorrida, velocidad, altura, puntos)
    public BarraPotencia barraPotencia;
    public Image interfazPressDown;

    public MenuGameOver menuGameOver;

    private bool onRun = false; //Estado que indica si el jugador está en una run o no.
    private static Jugador instance = null;
    public PlayerStats playerStats;
    private ManageTerrain gestionarTerreno;
    private PhysicsMaterial2D material; //Referencia al material2D del objeto jugador

    private bool tocandoLimite = false; //Si el jugador está rozando contra el suelo no podrá generar el efecto de impulso vertical.
    private float lastPositionX; //Calculo de la ultima posicion del jugador: Se emplea para calcular su puntuación.
    private bool efectoGenerado = false; //Solo se puede realizar el impulso 1 vez en el aire. Para poder volver a usarlo es necesario tocar el suelo o a un adversario.

    private Transform jugadorSpawnPoint;
    private double distanciaRecorrida = 0;
    //Sonidos efectos del jugador
    public AudioClip soundColchoneta;
    public AudioClip soundHit;
    public AudioClip soundFloor;
    private AudioSource audioSource;




    // Método para obtener la instancia única del Jugador
    public static Jugador Instance
    {
        get
        {
            return instance;
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            jugadorFunctions = new JugadorFunctions(this); //Clase que maneja las funciones del jugador
            instance = this;
            playerStats = PlayerStats.Instance;

            audioSource = GetComponent<AudioSource>(); //Instancia de audioSource del jugador
            jugadorSpawnPoint = GameObject.FindWithTag("JugadorSpawnPoint").transform; //Obtiene referencia del spawnpoint para setear al jugador
            gestionarTerreno = FindObjectOfType<ManageTerrain>(); //Obtiene referencias al Gestor de terrenos del jugador
            material = instance.GetComponent<Collider2D>().sharedMaterial; //Obtiene referencias al material del jugador

            //Establece las posiciones
            
            RestorePlayerSpawnPoint(); //pone al jugador en su spawnpoint
            
            UpdatePlayerScore(); //establece en la interfaz los puntos de los que dispone.
            
            material.friction = playerStats.GetFriccion(); //friccion del material

            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (onRun)
        {
            jugadorFunctions.ActivarImpulsoVertical(); //Detecta si el usuario acciona el impulso vertical
            jugadorFunctions.RestorePlayerPosition(); //Mueve el jugador al inicio
            gestionarTerreno.UpgradeTerreno(distanciaRecorrida); //Actualiza el terreno en base a la distancia que ha recorrido el jugador
            UpdatePlayerScore(); //Actualiza la información del jugador
            playerStats.AplyAerodinamica(jugadorInstancia); //aplica aerodinamica al jugador

            if (jugadorInstancia.velocity.magnitude <= 0)
            {
                Debug.Log("Velocidad detenida. Reiniciando nivel");
                menuGameOver.SetFinalPuntuacion(); //establece puntuacion final
                menuGameOver.ActivarMenuGameOver();
                onRun = false;          
            }
        }

    }

    //Funcion que gestiona el fin de una partida
    public void RestartGameRun()
    {
        RestorePlayerSpawnPoint(); //Reinicia la posicion del jugador
        barraPotencia.ReiniciarBarraPotencia(); //Reinicia la barra potencia a su valor iniciar y actualiza los valores maximos y minimos de lanzamiento
        gestionarTerreno.ResetTerrain(); //reinicia el nivel del terreno y sus propiedades a 0
        RestartMarcadores(); //reinicia puntuacion y marcadores
        interfazPressDown.gameObject.SetActive(false);
    }

    public void RestartMarcadores(){
        jugadorFunctions.RestorePoints(); //Reinicia los puntos del jugador
        realTimeStats.UpdateRealTimeText();
    }

    //Realiza un calculo de puntuación del jugador
    void UpdatePlayerScore()
    {
        float distance = jugadorInstancia.position.x - lastPositionX;
        // Si la distancia es negativa, significa que el jugador ha sido reseteado
        if (distance < 0)
        {
            distance = 0;
        }
        // Añade la distancia al puntaje
        playerStats.Puntuacion += System.Convert.ToInt64(distance * 10);
        playerStats.PuntuacionTotal += System.Convert.ToInt64(distance * 10);
        distanciaRecorrida += distance;
        // Actualiza la última posición X del jugador
        lastPositionX = jugadorInstancia.position.x;
        realTimeStats.UpdateRealTimeText();

    }

    // Detecta cuando el jugador está tocando el suelo
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == limiteInferior)
        {
            material.friction = playerStats.GetFriccion();
            if(efectoGenerado){
            //Añade efecto de sonido al tocar la hierba sólo si viene con el impulso
            jugadorFunctions.EmitirSonido(soundFloor);
            }
            tocandoLimite = true;
            efectoGenerado = false;
            
        }

    }

    // Detecta cuando el usuario deja de tocar el suelo
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == limiteInferior)
        {
            tocandoLimite = false;
            if(onRun){
                pulsadorImpulsoVertical.IniciarAnimacion();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D entity)
    {
        jugadorFunctions.CheckCollisionEntity(entity);
        if (entity.gameObject.CompareTag("TrampolinAlly")){
            pulsadorImpulsoVertical.IniciarAnimacion();
        }
         
    }

    //Funcion que resetea la posicion del jugador a su spawnpoint.
    public void RestorePlayerSpawnPoint()
    {
        instance.transform.position = jugadorSpawnPoint.position;
    }

    public Rigidbody2D GetJugadorRigibody()
    {
        return jugadorInstancia;
    }

    public double GetDistanciaRecorrida()
    {
        return distanciaRecorrida;
    }

    public void SetDistanciaRecorrida(double distanciaRecorrida)
    {
        this.distanciaRecorrida = distanciaRecorrida;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    public bool IsTocandoLimite()
    {
        return tocandoLimite;
    }

    public bool IsEfectoGenerado()
    {
        return efectoGenerado;
    }

    public void SetEfectoGenerado(bool efectoGenerado)
    {
        this.efectoGenerado = efectoGenerado;
    }

    public bool IsOnRun()
    {
        return onRun;
    }
    public void SetOnRun(bool onRun)
    {
        this.onRun = onRun;
    }

}
