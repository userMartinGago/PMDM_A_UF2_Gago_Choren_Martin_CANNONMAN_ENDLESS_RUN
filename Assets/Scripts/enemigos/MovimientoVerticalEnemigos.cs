
using UnityEngine;

public class MovimientoVerticalEnemigos : MonoBehaviour
{
    private Rigidbody2D enemigoRB;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask limitMap;
    [SerializeField] private LayerMask limitTopMap;

    private float distanciaRecorrida = 0f;
    private readonly float distanciaLimite = 10f;


    private void Start()
    {
        enemigoRB = GetComponent<Rigidbody2D>();
        // Genera de forma aleatoria la dirección a la que se moverán los enemigos al empezar
        if (Random.Range(0, 2) == 0)
        {
            velocidadMovimiento = Mathf.Abs(velocidadMovimiento); // Mover hacia arriba
        }
        else
        {
            velocidadMovimiento = -Mathf.Abs(velocidadMovimiento); // Mover hacia abajo
        }
    }

    private void Update()
    {
        enemigoRB.velocity = new Vector2(enemigoRB.velocity.x, velocidadMovimiento);
        RaycastHit2D informacionMapa = Physics2D.Raycast(transform.position, Vector2.up * Mathf.Sign(velocidadMovimiento), distancia, limitMap); //limite de abajo
        RaycastHit2D informacionMapaSuperior = Physics2D.Raycast(transform.position, Vector2.up * Mathf.Sign(velocidadMovimiento), distancia, limitTopMap);
        // Genera un efecto de "patrulla" el enemigo irá de arriba a abajo avanzando hasta llegar a distanciaLimite, cuando lo alcance cambiará de dirección y avanzará nuevamente
        if (informacionMapa || informacionMapaSuperior || distanciaRecorrida > distanciaLimite)
        {
            CambiarDireccion();
            distanciaRecorrida = 0;
        }
        else
        {
            distanciaRecorrida += Mathf.Abs(velocidadMovimiento * Time.deltaTime);
        }
    }

    private void CambiarDireccion()
    {
        velocidadMovimiento = -velocidadMovimiento; // Cambia la dirección del movimiento
    }

}
