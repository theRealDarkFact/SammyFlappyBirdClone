using UnityEngine;

public class HouseMover : MonoBehaviour
{
    [SerializeField] private float Speed = 1;
    [SerializeField] private Vector2 startingLocation;
    
    private Material HouseMaterial;

    private int RandomColor;
    private NightDay ndCycler;

    private void Start()
    {
        ndCycler = FindObjectOfType<NightDay>();
        HouseMaterial = GetComponent<SpriteRenderer>().material;
        ChangeColor();
    }

    private void Update()
    {
        transform.Translate(new Vector2(-Speed * GameManager.Instance.GetGameSpeed(), 0) * Time.deltaTime);
        if(transform.position.x < -9f) 
        {
            transform.position = startingLocation;
            ChangeColor();
        }
        CheckDay();
    }

    private void CheckDay() 
    {
        if (ndCycler.dayState() && HouseMaterial.GetFloat("_GlowStrength") < 5f)
        {
            HouseMaterial.SetFloat("_GlowStrength", Mathf.Lerp(HouseMaterial.GetFloat("_GlowStrength"), 2f, 2 * Time.deltaTime));
        }
        else if (!ndCycler.dayState() && HouseMaterial.GetFloat("_GlowStrength") > 2f)
        {
            HouseMaterial.SetFloat("_GlowStrength", Mathf.Lerp(HouseMaterial.GetFloat("_GlowStrength"), 5f, 2 * Time.deltaTime));
        }
    }

    private void ChangeColor() 
    {
        RandomColor = Random.Range(0, 4);
        switch (RandomColor) 
        {
            case 0:
                HouseMaterial.SetColor("_RandomColor", Color.red);
                break;
            case 1:
                HouseMaterial.SetColor("_RandomColor", Color.cyan);
                break;
            case 2:
                HouseMaterial.SetColor("_RandomColor", Color.magenta);
                break;
            case 3:
                HouseMaterial.SetColor("_RandomColor", Color.green);
                break;
            case 4:
                HouseMaterial.SetColor("_RandomColor", Color.yellow);
                break;
        }
    }
}
