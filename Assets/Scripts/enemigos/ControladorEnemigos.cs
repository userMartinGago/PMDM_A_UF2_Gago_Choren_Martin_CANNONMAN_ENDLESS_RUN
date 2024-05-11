
using UnityEngine;


public class ControladorEnemigos : MonoBehaviour
{

    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;

    private bool enemigosGenerados = false;


    private void Start()
    {
        GenerarEnemigos();
    }


    public void GenerarEnemigos()
    {
        if (!enemigosGenerados)
        {
            GenerarTodosEnemigos();
            enemigosGenerados = true;
        }
    }
    
    // Genera enemigo aleatorio en todos los spawnPoints disponibles.
    private void GenerarTodosEnemigos()
    {
        foreach (Transform punto in puntos)
        {
            int numeroEnemigo = Random.Range(0, enemigos.Length);
            if(numeroEnemigo == 0){
                CrearEnemigo(0, -.45f, punto); //El enemigo 0 es la roca pequeña la cual debe estar ligeramente abajo para que coincida con el suelo
            }else{
                CrearEnemigo(numeroEnemigo, 0 , punto);
            }
            
        }
    }

    //Genera un enemigo
    private void CrearEnemigo(int enemigoValue, float altura, Transform punto)
    {
        Vector2 posicionEnemigo = new Vector2(punto.position.x, punto.position.y + altura);
        Instantiate(enemigos[enemigoValue], posicionEnemigo, Quaternion.identity);
    }


}
