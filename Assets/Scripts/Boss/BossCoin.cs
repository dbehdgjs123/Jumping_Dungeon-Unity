using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCoin : MonoBehaviour
{
    public GameObject sparkPrefab;
    private GameObject coinSpark; //파티클

    Transform tr;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        coinSpark = Instantiate(sparkPrefab, tr.position,Quaternion.identity);
    }
    private void OnEnable()
    {
        if (coinSpark.active == true)
        {
            coinSpark.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.AddGold(1);
            SoundManager.instance.PlaySE("Coin");
            coinSpark.transform.position = tr.position;
            coinSpark.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
