
using UnityEngine;

public class FuerzaVertical
{
    //Clase que gestiona y maneja la fuerza de impulso vertical del jugador
    public float VelocidadMaxima { get; set; } // se sustitiye por el valor de velocidad maxima del jugador
    public float Base { get; set; } //impulso base
    public int Level { get; set; } //nivel del impulso
      private readonly int CosteBase = 100;
    public long CosteTotal { get; set; } //coste total aumenta un 5% por cada nivel

    public float Value { get; set; } //calculo de la fuerza del impulso

    public FuerzaVertical(float basee, int level){
        Base = basee;
        Level = level;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Calcula el CosteTotal en el constructor
    }

    // Funcion que realiza un calculo de la fuerza vertical aplicable al jugador
    //Dicha funcion tiene en cuenta la velocidad de desplazamiento actual del jugador para aplicar una fuerza proporcional a esta
    public float CalcFuerzaVertical(float velocidadActual)
    {
        float factor = velocidadActual / VelocidadMaxima;
        float pow = Mathf.Pow(1.045f, Level) * factor;
        Value = Base + pow + (Level * 0.2f);
        return Value;
    }

    public void Upgrade()
    {
        Level += 1;
        CosteTotal = (int)(CosteBase * Mathf.Pow(1.1f, Level)); // Recalcula el CosteTotal cuando se mejora
    }

}
