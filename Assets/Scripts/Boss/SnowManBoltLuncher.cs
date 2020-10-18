using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManBoltLuncher : MonoBehaviour
{
    public GameObject[] bolt = new GameObject[5]; //총알


    private void OnEnable()
    {
        for(int i = 0; i< 5; i++)
        {
            bolt[i].SetActive(false);
        }
        StartCoroutine("Create");
    }

    IEnumerator Create()
    {
        int count = 0;
        yield return new WaitForSeconds(1f);
        while(count < 5)
        {
            yield return new WaitForSeconds(0.7f);
            SoundManager.instance.PlaySE("Iattack1");
            bolt[count].SetActive(true);
            count++;
        }
    }
}
