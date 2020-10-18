using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : MonoBehaviour
{
    private GameObject player;
    private PlayerController pc;
    private SpriteRenderer pf; //플레이어의 filp을 사용하기 위해 선언
    private SpriteRenderer sf; //아이템의 플립을 사용하기 위해 선언
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("PlayerTr").transform.GetChild(PlayerPrefs.GetInt("Character", 0)).gameObject;
        pf = player.GetComponent<SpriteRenderer>();
        sf = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        
    }
    private void Update()
    {
        if(pf.flipX == true)
        {
            sf.flipX = true;
        }
        else
        {
            sf.flipX = false;
        }
    }

}
