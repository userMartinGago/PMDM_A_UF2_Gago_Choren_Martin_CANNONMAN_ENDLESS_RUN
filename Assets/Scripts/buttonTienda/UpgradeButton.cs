using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private PlayerStats jugadorEstadisticas;
    //Referencia de los botones
    public Button speedButton; // Referencia al botón de velocidad
    public Button verticalForceButton; // Referencia al botón de fuerza vertical
    public Button aerodynamicsButton; // Referencia al botón de aerodinámica
    public Button frictionButton; // Referencia al botón de fricción

    //Texto de puntos:
    public TextMeshProUGUI puntosTotales;

    //Referencia de los textos de nivel

    //Velocidad
    public TextMeshProUGUI lvlSpeedText; //referencia texto nivel de velocidad
    public TextMeshProUGUI costSpeedText; //referencia coste mejora.
    //Fuerza vertical
    public TextMeshProUGUI lvlVFText; //referencia texto nivel de fuerza vertical
    public TextMeshProUGUI costVFText; //coste mejora
    //Aerodinamica
    public TextMeshProUGUI lvlAeroText; //referencia texto nivel de aerodinamica
    public TextMeshProUGUI costAeroText;
    //Friccion
    public TextMeshProUGUI lvlFrictionText; //referencia texto nivel de friccion
    public TextMeshProUGUI costFricctionText;


    //Estado de los botones cuando se inicializa la aplicacion
    void Start()
    {
        jugadorEstadisticas = PlayerStats.Instance;

        //Se inicializan los datos de los botones:
        lvlSpeedText.text = jugadorEstadisticas.velocidad_attrb.Level.ToString();
        costSpeedText.text = jugadorEstadisticas.velocidad_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
        //inicializados textos btn friccion
        lvlVFText.text = jugadorEstadisticas.fuerzaVertical_attrb.Level.ToString();
        costVFText.text = jugadorEstadisticas.fuerzaVertical_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
        //inicializados textos btn aerodinamica
        lvlAeroText.text = jugadorEstadisticas.aerodinamica_attrb.Level.ToString();
        costAeroText.text = jugadorEstadisticas.aerodinamica_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
        //inicializados textos btn friccion
        lvlFrictionText.text = jugadorEstadisticas.friccion_attrb.Level.ToString();
        costFricctionText.text = jugadorEstadisticas.friccion_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));

        //Boton de la velocidad
        speedButton.onClick.AddListener(() =>
        {
            jugadorEstadisticas.UpgradeVelocidad();
            lvlSpeedText.text = jugadorEstadisticas.velocidad_attrb.Level.ToString("N0", new CultureInfo("es-ES"));
            costSpeedText.text = jugadorEstadisticas.velocidad_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
            jugadorEstadisticas.PuntuacionTotal -= jugadorEstadisticas.velocidad_attrb.CosteTotal;
            puntosTotales.text = jugadorEstadisticas.PuntuacionTotal.ToString("N0", new CultureInfo("es-ES"));
        }

        );
        //Boton fuerza vertical
        verticalForceButton.onClick.AddListener(() =>
        {
            jugadorEstadisticas.UpgradeFuerzaVertical();
            lvlVFText.text = jugadorEstadisticas.fuerzaVertical_attrb.Level.ToString("N0", new CultureInfo("es-ES"));
            costVFText.text = jugadorEstadisticas.fuerzaVertical_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
            jugadorEstadisticas.PuntuacionTotal -= jugadorEstadisticas.fuerzaVertical_attrb.CosteTotal;
            puntosTotales.text = jugadorEstadisticas.PuntuacionTotal.ToString("N0", new CultureInfo("es-ES"));
        });


        //Boton de la aerodinamica
        aerodynamicsButton.onClick.AddListener(() =>
        {
            jugadorEstadisticas.UpgradeAerodinamica();
            lvlAeroText.text = jugadorEstadisticas.aerodinamica_attrb.Level.ToString("N0", new CultureInfo("es-ES"));
            costAeroText.text = jugadorEstadisticas.aerodinamica_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
            jugadorEstadisticas.PuntuacionTotal -= jugadorEstadisticas.aerodinamica_attrb.CosteTotal;
            puntosTotales.text = jugadorEstadisticas.PuntuacionTotal.ToString("N0", new CultureInfo("es-ES"));
        });
        //Boton de la friccion
        frictionButton.onClick.AddListener(() =>
        {
            jugadorEstadisticas.UpgradeFriccion();
            lvlFrictionText.text = jugadorEstadisticas.friccion_attrb.Level.ToString("N0", new CultureInfo("es-ES"));
            costFricctionText.text = jugadorEstadisticas.friccion_attrb.CosteTotal.ToString("N0", new CultureInfo("es-ES"));
            jugadorEstadisticas.PuntuacionTotal -= jugadorEstadisticas.friccion_attrb.CosteTotal;
            puntosTotales.text = jugadorEstadisticas.PuntuacionTotal.ToString("N0", new CultureInfo("es-ES"));
        });
    }

    void Update()
    {
        // Habilita o deshabilita los botones basándose en si el jugador tiene suficientes puntos
        speedButton.interactable = jugadorEstadisticas.PuntuacionTotal >= jugadorEstadisticas.velocidad_attrb.CosteTotal;
        verticalForceButton.interactable = jugadorEstadisticas.PuntuacionTotal >= jugadorEstadisticas.fuerzaVertical_attrb.CosteTotal;
        aerodynamicsButton.interactable = jugadorEstadisticas.PuntuacionTotal >= jugadorEstadisticas.aerodinamica_attrb.CosteTotal;
        frictionButton.interactable = jugadorEstadisticas.PuntuacionTotal >= jugadorEstadisticas.friccion_attrb.CosteTotal;
    }

}
