
using UnityEngine;

public class PlayerStats
{

    public Velocidad velocidad_attrb; //Velocidad del jugador de la que puede salir disparada del cañon
    public FuerzaVertical fuerzaVertical_attrb; // Fuerza vertical que tiene el jugador para impulsarse hacia abajo
    public Aerodinamica aerodinamica_attrb; //Atributo de la aerodinamica del jugador (Resistencia al viento)
    public Friccion friccion_attrb; // atributo de la friccion del jugador con el suelo

    public long Puntuacion { get; set; } //Puntuación del jugador
    public long PuntuacionTotal {get; set;} //Puntuación total del jugador acumulada en diferentes runs
    public int MultiplicadorPuntuacion { get; set; } //multiplicador de puntuacion


    // Instancia única del jugador
    private static PlayerStats instance = null;

    private PlayerStats()
    {
        velocidad_attrb = new Velocidad (32f, 0 ); //Velocidad base máxima con la que puede salir impulsado el jugador. Cada mejora incrementa un 5% sobre el anterior
        fuerzaVertical_attrb = new FuerzaVertical (11f, 0); //Velocidad con la que el jugador se impulsa hacia abajo al activar habilidad.
        aerodinamica_attrb = new Aerodinamica ( 8f, 0 ); //Nivel de aerodinamica del jugador +nivel - resistencia
        friccion_attrb = new Friccion (4f, 0 ); //Friccion que tiene el jugador con el terreno
    }


    // Método para obtener la instancia única del jugador
    public static PlayerStats Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerStats();
            }
            return instance;
        }
    }

    //Mejora de los atributos
    public void UpgradeVelocidad() //Mejora la velocidad del jugador
    {
        velocidad_attrb.Upgrade();
    }

    public void UpgradeFuerzaVertical() //Mejora el impulso vertical del jugador
    {
        fuerzaVertical_attrb.Upgrade();
    }

    public void UpgradeAerodinamica() //Mejora aerodinamica del jugador
    {
        aerodinamica_attrb.Upgrade();
    }

    public void UpgradeFriccion()
    { //mejora la friccion del jugador
        friccion_attrb.Upgrade();
    }

    //Getters de los atributos:

    public void UpdateVelocidadMaxima(float newVelocidad)
    {
        fuerzaVertical_attrb.VelocidadMaxima = newVelocidad;
    }

    public void AplyAerodinamica(Rigidbody2D rigibodyJugador)
    {
        aerodinamica_attrb.AplicarAerodinamica(rigibodyJugador);
    }

    public long GetPuntuacionTotal(){
        return PuntuacionTotal;
    }
    public float GetVelocidad()
    {
        return velocidad_attrb.Value;
    }

    public float GetFriccion()
    {
        return friccion_attrb.Value;
    }

    public float GetFuerzaVertical(float velocidadActual)
    {
        //Hay que obtener la fuerza del jugador, no la velocidad que este sigue
        return fuerzaVertical_attrb.CalcFuerzaVertical(velocidadActual);
    }

}
