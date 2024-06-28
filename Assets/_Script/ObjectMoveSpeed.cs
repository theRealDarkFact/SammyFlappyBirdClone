using UnityEngine;

public class ObjectMoveSpeed : MonoBehaviour
{
    [SerializeField] private Vector2[] startPosition;
    [SerializeField] private float Speed = 1;
    [SerializeField] private GameObject[] LayerObjects;
    [SerializeField] private float LayerResetX;


    private void Update()
    {
        int index = 0;
        foreach(GameObject go in LayerObjects) 
        {
            go.transform.Translate(new Vector2(-Speed * GameManager.Instance.GetGameSpeed(), 0) * Time.deltaTime);
            if (go.transform.position.x <= LayerResetX) go.transform.position = startPosition[index];
        }
    }
}
