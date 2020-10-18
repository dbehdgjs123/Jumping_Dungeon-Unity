using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    //파티클을 없애주기 위한 코드
    public float seconds;

    private void OnEnable()
    {
        StartCoroutine(Disabled());
    }
    IEnumerator Disabled()
    {
        while(gameObject.active == true)
        {
            yield return new WaitForSeconds(seconds);
            gameObject.SetActive(false);
        }
    }
}
