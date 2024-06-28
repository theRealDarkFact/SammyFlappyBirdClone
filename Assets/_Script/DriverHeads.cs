using UnityEngine;

public class DriverHeads : MonoBehaviour
{
    [SerializeField] private Sprite[] DriverHeadSprite;
    private SpriteRenderer HeadSpriteRenderer;
    private int RandomIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        HeadSpriteRenderer = GetComponent<SpriteRenderer>();
        RandomIndex = Random.Range(0, DriverHeadSprite.Length);
        HeadSpriteRenderer.sprite = DriverHeadSprite[RandomIndex];
    }

    public void RandomizeDrive() 
    {
        RandomIndex = Random.Range(0, DriverHeadSprite.Length);
        HeadSpriteRenderer.sprite = DriverHeadSprite[RandomIndex];
    }
}
