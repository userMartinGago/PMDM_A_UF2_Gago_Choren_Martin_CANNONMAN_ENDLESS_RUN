
using UnityEngine;

public class JugadorSpriteRenderer : MonoBehaviour
{
    public Jugador jugador;
    public SpriteRenderer playersprite; // Referencia al SpriteRenderer del jugador
    public Sprite defaultSprite; // El sprite por defecto
    public Sprite effectSprite; // El sprite del efecto

    // Start is called before the first frame update
    void Start()
    {
        if (jugador.IsEfectoGenerado())
        {
            playersprite.sprite = effectSprite; // Si jugadorEfectoGenerado es true, aplica effectSprite
        }
        else
        {
            playersprite.sprite = defaultSprite; // Si jugadorEfectoGenerado es false, aplica defaultSprite
        }
    }

    void Update()
    {
        if (jugador.IsEfectoGenerado())
        {
            playersprite.sprite = effectSprite; // Si jugadorEfectoGenerado es true, aplica effectSprite
        }
        else
        {
            playersprite.sprite = defaultSprite; // Si jugadorEfectoGenerado es false, aplica defaultSprite
        }
    }
}
