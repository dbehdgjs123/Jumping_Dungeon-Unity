using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeDestroy : MonoBehaviour
{
    
    public float seconds;

    private void OnEnable()
    {
        StartCoroutine(Disabled());
    }
    IEnumerator Disabled()
    {
        while (gameObject.active == true)
        {
            yield return new WaitForSecondsRealtime(seconds);
            gameObject.SetActive(false);
        }
    }
}
