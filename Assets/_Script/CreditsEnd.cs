using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEnd : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("StartCredits");
    }
    public void EndCreditsSequence() 
    {
        GameManager.Instance.HideCredits();
    }
}
