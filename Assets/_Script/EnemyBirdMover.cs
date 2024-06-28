using UnityEngine;

public class EnemyBirdMover : MonoBehaviour
{
    [SerializeField] private float Speed = 5;

    private void Start()
    {
        Speed = Random.Range(2, Speed);
    }

    void Update()
    {
        transform.Translate(new Vector2(-Speed * GameManager.Instance.GetGameSpeed(), 0) * Time.deltaTime);
        if (transform.position.x < -9f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            Player.Instance.HitBird();
        }
    }
}
