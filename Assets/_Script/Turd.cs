using UnityEngine;

public class Turd : MonoBehaviour
{
    private void OnDestroy()
    {
        Player.Instance.ClearDropCounter();
    }
}
