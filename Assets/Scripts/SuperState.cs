using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperState : MonoBehaviour
{

    public GameObject player;
    int count = 0; //무적 모드 카운트
    SpriteRenderer playerColor;

    private void Awake()
    {
        playerColor = player.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(Disabled(2.0f)); //무적 삭제
        StartCoroutine(SuperAnimation()); //무적
    }


    IEnumerator Disabled(float waitTime) //무적시간
    {
        yield return new WaitForSeconds(waitTime);
        playerColor.color = new Color32(255, 255, 255, 255);
        gameObject.SetActive(false);
    }

    IEnumerator SuperAnimation() //깜빡임 애니메이션
    {
        


        while (gameObject.active == true)
        {
                playerColor.color = new Color32(255, 255, 255, 130);
                yield return new WaitForSeconds(0.1f);
                playerColor.color = new Color32(255, 255, 255, 255);
                yield return new WaitForSeconds(0.1f);
           
        }

    }


}

