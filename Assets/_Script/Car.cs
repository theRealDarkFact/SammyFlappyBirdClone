using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Car : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private Vector2 startingLocation;
    [SerializeField] private Sprite[] CarColorSprites;
    [SerializeField] private DriverHeads dHeads;
    [SerializeField] private Light2D HeadLight;
    [SerializeField] private BoxCollider2D[] CarHitColliders;
    private float SpeedOffset = 0;
    private NightDay ndCycler;
    private SpriteRenderer CarSpriteRenderer;

    private void Awake()
    {
        CarSpriteRenderer = GetComponent<SpriteRenderer>();
        CarSpriteRenderer.sprite = CarColorSprites[Random.Range(0, CarColorSprites.Length)];
        ndCycler = FindObjectOfType<NightDay>();
        SetRandomSpeed();
    }

    private void Update()
    {
        CheckHeadLights();
        transform.Translate(new Vector2(-Speed * GameManager.Instance.GetGameSpeed(), 0) * Time.deltaTime);
        if (transform.position.x < -10.18f)
        {
            SetRandomSpeed();
            dHeads.RandomizeDrive();
            transform.position = startingLocation;
        }
    }

    private void CheckHeadLights()
    {
        if (ndCycler.dayState() && !HeadLight.enabled)
        {
            HeadLight.enabled = true;
        }
        else if (!ndCycler.dayState() && HeadLight.enabled)
        {
            HeadLight.enabled = false;
        }
    }

    private void SetRandomSpeed() 
    {
        Speed -= SpeedOffset;
        SpeedOffset = Random.Range(0.0f, 0.6f);
        Speed += SpeedOffset;
        foreach(BoxCollider2D bc in CarHitColliders) 
        {
            bc.enabled = true;
            bc.gameObject.GetComponent<HitZone>().resetSplatters();
        }

    }
}
