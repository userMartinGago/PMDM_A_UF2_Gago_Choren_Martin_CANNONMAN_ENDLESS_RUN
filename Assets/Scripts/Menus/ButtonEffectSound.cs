
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonEffectSound : MonoBehaviour
{
    
    public AudioClip clickSoundClip;
    private AudioSource audioSource;
  void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clickSoundClip;
        audioSource.playOnAwake = false;

        Button button = GetComponent<Button>();
        button.onClick.AddListener(PlayClickSound);
    }

    void PlayClickSound()
    {
        audioSource.Play();
    }
}
