using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public GameObject coinSpark; //파티클

    private void OnEnable()
    {
        if(coinSpark.active == true)
        {
            coinSpark.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.instance.AddGold(1);
            SoundManager.instance.PlaySE("Coin");
            coinSpark.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
