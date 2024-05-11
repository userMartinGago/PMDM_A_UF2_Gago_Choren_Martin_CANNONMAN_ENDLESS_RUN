
using UnityEngine;

public class Friccion
{
    //Clase que gestiona y maneja la friccion del jugador con el terreno
    public float Base { get; set; } //velocidad base
    public int Level { get; set; } //nivel de la velocidad
    private readonly int CosteBase = 200;
    public long CosteTotal { get; set; } //coste total aumenta un 5% por cada nivel

    public Friccion(float basee, int level){
        Base = basee;
        Level = level;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Calcula el CosteTotal en el constructor
    }
    public float Value { get { return Base - Level * 0.04f; } } //calculo de la friccion mejorada al nivel 100 la friccion es de 0

    public void Upgrade()
    {
        Level += 1;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Calcula el CosteTotal en el constructor
    }
}
