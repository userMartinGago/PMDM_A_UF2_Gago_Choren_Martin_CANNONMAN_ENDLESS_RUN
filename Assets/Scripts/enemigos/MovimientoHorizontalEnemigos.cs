using UnityEngine;


public class MovimientoEnemigos : MonoBehaviour
{
    private Rigidbody2D enemigoRB;
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask limitMap;

    private float distanciaRecorrida = 0f;
    private readonly float distanciaLimite = 15f;



    private void Start()
    {
        enemigoRB = GetComponent<Rigidbody2D>();
        //Genera de forma aleatoria la dirección a la que miran los enemigos al empezar
        if (Random.Range(0, 2) == 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void Update()
    {
        enemigoRB.velocity = new Vector2(velocidadMovimiento * -transform.right.x, enemigoRB.velocity.y);
        RaycastHit2D informacionMapa = Physics2D.Raycast(transform.position, -transform.right, distancia, limitMap);
        //Genera un efecto de "patrulla" el enemigo irá de izquierda a derecha avanzando hasta llegar a distanciaLimite, cuando lo alcance girará y avanzará nuevamente
        if (informacionMapa || distanciaRecorrida > distanciaLimite)
        {
            Girar();
            distanciaRecorrida = 0;
        }
        else
        {
            distanciaRecorrida += Mathf.Abs(velocidadMovimiento * Time.deltaTime);
        }
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y - 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * distancia);
    }
}
