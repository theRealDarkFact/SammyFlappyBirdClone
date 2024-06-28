using UnityEngine;

public class Hazard : MonoBehaviour
{
    private AudioSource PowerLineShockSound;

    private void Start()
    {
        PowerLineShockSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Player.Instance.HurtPlayer();
            if (!PowerLineShockSound.isPlaying) PowerLineShockSound.Play();
        }    
    }
}
