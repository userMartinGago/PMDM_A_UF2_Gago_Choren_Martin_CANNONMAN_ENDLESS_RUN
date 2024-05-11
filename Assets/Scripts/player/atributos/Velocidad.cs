
using UnityEngine;

public class Velocidad
{

    //Clase que gestiona y maneja la velocidad y mejora de la propiedad de velocidad
    public float Base { get; set; } //velocidad base
    public int Level { get; set; } //nivel de la velocidad
    private readonly int CosteBase = 25;
    public long CosteTotal { get; set; } //coste total aumenta un 20% por cada nivel
    public float Value { get { return Base + Mathf.Pow(1.07f, Level) + (0.5f * Level); } } //calculo de la velocidad mejorada

    public Velocidad(float basee, int level)
    {
        Base = basee;
        Level = level;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Calcula el CosteTotal en el constructor
    }

    public void Upgrade()
    {
        Level += 1;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Recalcula el CosteTotal cuando se mejora
    }
}
