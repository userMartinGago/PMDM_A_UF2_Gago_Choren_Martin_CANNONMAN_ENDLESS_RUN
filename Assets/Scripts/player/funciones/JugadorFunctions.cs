using UnityEngine;

public class JugadorFunctions
{

    private readonly Jugador jugador; //Referencia al objeto jugador que ejecutará estas funciones
    public JugadorFunctions(Jugador jugador)
    {
        this.jugador = jugador;
    }

    /**
    * Funcion que ralentiza la velocidad del jugador al tocar un enemigo.
    Recibe como parametros la base de la ralentización y un porcentaje extra de ralentización.
    */
    public void RalentizarJugador(float baseNum, float porcentaje)
    {
        Debug.Log("aplicando ralentización al jugador");
        float ralentizacionEnemigo = baseNum + jugador.jugadorInstancia.velocity.x * porcentaje; //ralentización que va a aplicar el enemigo al jugador
        if (jugador.jugadorInstancia.velocity.x > ralentizacionEnemigo)
        {
            //si la velocidad del jugador es mayor a la ralentización entonces se aplica la ralentización al jugador:
            jugador.jugadorInstancia.velocity = new Vector2(jugador.jugadorInstancia.velocity.x - ralentizacionEnemigo, jugador.jugadorInstancia.velocity.y);
        }
        else
        {
            //el jugador va mas lento que la ralentización que aplica el enemigo: La velocidad del jugador es 0
            jugador.jugadorInstancia.velocity = new Vector2(0, jugador.jugadorInstancia.velocity.y);
        }
    }

    /**
    Aplica un Impulso al jugador, recibe como parametros un impulso base + un % de impulso extra que se realizará
    */
    public void ImpulsarJugador(float baseNum, float porcentaje)
    {
        float aceleracionJugador = baseNum + jugador.jugadorInstancia.velocity.x * porcentaje; //aceleracion que se aplica al jugador de forma horizontal

        float velocidadVertical = 0f;
        if (jugador.jugadorInstancia.velocity.y < 0)
        { //si su velocidad es vertical lo impulsa hacia arriba
            velocidadVertical = jugador.jugadorInstancia.velocity.y * (1.4f + porcentaje) * -1;
            Debug.Log("viene de arriba: " + velocidadVertical);
        }
        else
        {
            Debug.Log("viene de un lateral: " + velocidadVertical);
            velocidadVertical = 5f + jugador.jugadorInstancia.velocity.x * porcentaje;
        }
        jugador.jugadorInstancia.velocity = new Vector2(jugador.jugadorInstancia.velocity.x + aceleracionJugador, velocidadVertical);
    }

    //Cuando el jugador pulsa la flecha hacia abajo activa el impulso vertical:
    public void ActivarImpulsoVertical()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!jugador.IsTocandoLimite() && !jugador.IsEfectoGenerado())
            {
                jugador.jugadorInstancia.velocity = new Vector2(jugador.jugadorInstancia.velocity.x, 0);//Se cancela primero la velocidad en el eje y, ya que puede causar problemas
                //Este valor se multiplica por la velocidad a la que va el jugador, cuanto más lento, menos fuerza aplica.
                float fuerza = jugador.playerStats.GetFuerzaVertical(jugador.jugadorInstancia.velocity.x);
                jugador.jugadorInstancia.AddForce(Vector2.down * fuerza, ForceMode2D.Impulse);
                jugador.SetEfectoGenerado(true);
            }
        }
    }


    //Cada 1000 uds recorridas se produce un reseteo del jugador 
    public void RestorePlayerPosition()
    {
        if (jugador.jugadorInstancia.position.x > 1200)
        {
            float currentY = jugador.jugadorInstancia.position.y;
            jugador.jugadorInstancia.position = new Vector2(200, currentY);
        }
    }

    public void RestorePoints(){
        jugador.playerStats.Puntuacion = 0;
        jugador.SetDistanciaRecorrida(0);
    }


    /**
    Detecta las colisiones con los elementos del mapa
    Sobre cada entidad con la que colisione aplica un efecto de ralentización (enemigo) con sus respectivas estadisticas
    O impulso de jugador (Colchoneta). Si el jugaodr viene con el efecto verticala activado el impulso será mayor que si viene en un estado normal
    */
    public void CheckCollisionEntity(Collider2D entity)
    {
        if (entity.gameObject.CompareTag("RinoEnemy"))
        {
            Debug.Log("enemigo hit");
            RalentizarJugador(30f, 0.20f); //ralentiza al jugador 30f + 20% velocidad actual
            //Efecto sonido 
            EmitirSonido(jugador.soundHit);
        }
        else if (entity.gameObject.CompareTag("RockEnemy"))
        {
            Debug.Log("enemigo hit");
            RalentizarJugador(20f, 0.15f); //ralentiza el jugador 20f + 15% velocidad actual
            //Efecto sonido 
            EmitirSonido(jugador.soundHit);
        }
        else if (entity.gameObject.CompareTag("LittleRockEnemy"))
        {
            Debug.Log("enemigo hit");
            RalentizarJugador(10f, 0.07f); //ralentiza el jugador 10f + 7% velocidad actual
            //Efecto sonido 
            EmitirSonido(jugador.soundHit);
        }
        else if (entity.gameObject.CompareTag("BeeEnemy"))
        {
            Debug.Log("Abeja hit");
            RalentizarJugador(15f, 0.15f); //ralentiza al jugador 40f + el 15% de su velocidad actual
            //Efecto sonido 
            EmitirSonido(jugador.soundHit);
        }
        else if (entity.gameObject.CompareTag("TrampolinAlly"))
        {
            if (jugador.IsEfectoGenerado()) //El jugador viene con impulso
            {
                Debug.Log("Impulso extra!!!");
                ImpulsarJugador(20f, 0.25f); //Si el jugador viene con el efecto que haberse impulsado hacia abajo
            }
            else
            {
                Debug.Log("Impulso normal");
                ImpulsarJugador(15f, 0.1f); //Acelera al jugador en 15f + 20% de su velocidad actual
            }
            //Efecto sonido 
            EmitirSonido(jugador.soundColchoneta);
            jugador.SetEfectoGenerado(false); //Permite al jugador pulsar impulso vertical nuevamente
        }
    }

    //Emite un efecto de sonido
    public void EmitirSonido(AudioClip sound)
    {
        jugador.GetAudioSource().clip = sound;
        jugador.GetAudioSource().Play();

    }
}
