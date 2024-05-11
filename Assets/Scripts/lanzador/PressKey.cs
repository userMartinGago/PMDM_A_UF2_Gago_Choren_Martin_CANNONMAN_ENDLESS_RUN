using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PressKey : MonoBehaviour
{
public Sprite[] frames; // tus imágenes aquí
    public float framesPerSecond = 10.0f;
    private Image image;

    private Coroutine gifAnimacion;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public IEnumerator AnimateGif()
    {
        while (true)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                image.sprite = frames[i];
                yield return new WaitForSeconds(5.0f / framesPerSecond);
            }
        }
    }

    public void IniciarAnimacion(){
        if(gifAnimacion == null){
            Debug.Log("creada animacion");
            gifAnimacion = StartCoroutine(AnimateGif());
        }   
    }

    public void PararAnimacion(){
        if(gifAnimacion != null){
            Debug.Log("Parada animacion");
            StopCoroutine(gifAnimacion);
            gifAnimacion = null;
            image.sprite = frames[1]; // Establece el sprite a la posición 1
        }
    }
}
