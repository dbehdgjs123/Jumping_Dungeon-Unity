using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltLauncher : MonoBehaviour
{
    private int count = 3; //총알 생성수
    public GameObject[] bullet; //오브젝트 풀링을 하기 위해 배열에 선언.
    public float seconds; //아이스와 파이어의 시간간격차는 다르므로

    public string ItemSound;




    private void OnEnable()
    {


        StartCoroutine(Shot()); //샷                              
    }
    void Start()
    {
        for (int i = 0; i < count; i++) //
        {
            bullet[i].SetActive(true);
        }
    }

    void Update()
    {

    }

    IEnumerator Shot()
    {
        while (gameObject.active == true)
        {
            if (gameObject.tag == "FbulletTr")
            {
                SoundManager.instance.PlaySE(ItemSound);
            }
            for (int i = 0; i < count; i++)
            {
                if (bullet[i].active == true)
                {
                    break;
                }
                else
                {
                    bullet[i].SetActive(true);
                }



            }
                   
            yield return new WaitForSeconds(seconds);
        
        }
    }
}
