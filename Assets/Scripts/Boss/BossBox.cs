using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBox : MonoBehaviour
{
    public GameObject coinPrefab;

    private GameObject[] coin;
    private GameObject[] items;



    private int count = 30;

    private void Awake()
    {
        items = new GameObject[8];
        coin = new GameObject[count];

        for(int i = 0; i <8; i++)
        {
            items[i] = gameObject.transform.GetChild(i).gameObject;
            if(items[i].active == true)
            {
                items[i].SetActive(false);
            }          
        }

        for(int i = 0; i< count; i++)
        {
            coin[i] = Instantiate(coinPrefab, transform);
            coin[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        SoundManager.instance.PlaySE("BossBox");
        for(int i = 0; i< count; i++)
        {
            float x = Random.Range(-3f, 3f);
            float y = Random.Range(-3f, 3f);
            coin[i].transform.localPosition = new Vector2(x, y);
            coin[i].SetActive(true);
        }
        for(int i = 1; i<8; i++)
        {
            if(Random.Range(0,2) == 0)
            {
                float x = Random.Range(-3f, 3f);
                float y = Random.Range(-3f, 3f);
                items[i].transform.localPosition = new Vector2(x, y);
                items[i].SetActive(true);
            }
            else
            {
                items[i].SetActive(false);
            }
            float fx = Random.Range(-3f, 3f);
            float fy = Random.Range(-3f, 3f);
            items[0].transform.localPosition = new Vector2(fx, fy);
            items[0].SetActive(true);
        }
    }
}
