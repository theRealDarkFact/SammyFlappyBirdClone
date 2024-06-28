using UnityEngine;

public class HitZone : MonoBehaviour
{
    [SerializeField] private int scoreValue = 0;
    [SerializeField] private SpriteRenderer spriteObject;
    private AudioSource SplishSound;
    private BoxCollider2D boxCol;
    private void Start()
    {
        SplishSound = GetComponent<AudioSource>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    public void resetSplatters() 
    {
        spriteObject.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TurdBiscuit")) 
        {
            Destroy(collision.gameObject);
            boxCol.enabled = false;
            SplishSound.Play();
            spriteObject.enabled = true;
            GameManager.Instance.AddScore(scoreValue);
        }
    }
}
