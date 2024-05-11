
using UnityEngine;
using UnityEngine.UI;


public class DownEffect : MonoBehaviour
{
    public Jugador jugador;
    public Image targetImage;
    public Sprite defaultImage;
    public Sprite effectImage;

    // Start is called before the first frame update
    void Start(){
        if(!jugador.IsEfectoGenerado()){
            targetImage.sprite = defaultImage;
        }else{
            targetImage.sprite = effectImage;
        }
    }

    void Update(){
         if(!jugador.IsEfectoGenerado()){
            targetImage.sprite = defaultImage;
        }else{
            targetImage.sprite = effectImage;
        }
    }
}
