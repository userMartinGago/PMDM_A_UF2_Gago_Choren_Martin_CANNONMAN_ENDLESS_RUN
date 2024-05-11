
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento; //Valor de velocidad de cada uno de los elementos del background
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRB;


    private void Awake() {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Jugador").GetComponent<Rigidbody2D>();
    }

    private void Update(){
        offset = (jugadorRB.velocity.x < 60f  ? jugadorRB.velocity.x : 60f) * 0.00156f * velocidadMovimiento * Time.deltaTime;
        // Limitamos el valor de offset a 60
        material.mainTextureOffset += offset;
    }
}
