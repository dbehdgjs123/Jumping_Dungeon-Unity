using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    public Slider gage; //슬라이더
    public GameObject bar; //gage의 실체 오브젝트
    public GameObject sshield; //트리거를 위해 사용 
    
    

    private GameObject player;
    private SpriteRenderer pf; //플레이어의 filp을 사용하기 위해 선언
    private SpriteRenderer sf; //아이템의 플립을 사용하기 위해 선언
    private Animator ani;
    private Animator pAni;
    private Rigidbody2D pR; //플레이어의 돌진을 위해 선언
    private PlayerController pC;

    public CircleCollider2D cir; //게이지가 다 찼을때만 활성화하기 위해 선언

    void Awake()
    {
        player = GameObject.Find("PlayerTr").transform.GetChild(PlayerPrefs.GetInt("Character", 0)).gameObject;
        pAni = player.GetComponent<Animator>();
        pf = player.GetComponent<SpriteRenderer>();
        pC = player.GetComponent<PlayerController>();
        sf = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        pR = player.GetComponent<Rigidbody2D>();
        cir = GetComponent<CircleCollider2D>();
        

        
    }
    private void OnEnable()
    {
        if (bar.active == true)
        {
            bar.SetActive(false);
            cir.enabled = false;
            bar.SetActive(true);
            StartCoroutine(UpGage());
        }
        else
        {
            cir.enabled = false;
            bar.SetActive(true);
            gage.value = 0;
            StartCoroutine(UpGage());
        }

        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (pf.flipX == true)
        {
            sf.flipX = true;
        }
        else
        {
            sf.flipX = false;
        }
    }
    IEnumerator UpGage()
    {
        while (gameObject.active == true)
        {
            if (GameManager.instance.isGameOver == false)
            {
                while (gage.value < 1.0f)
                {
                    gage.value += 0.01f;
                    yield return new WaitForSeconds(0.1f); //10초
                }
                if (gage.value == 1)
                {
                    cir.enabled = true;
                    if (pC.iceCurse.active == true)
                    {
                        pC.curseCount = 0;
                        SoundManager.instance.PlaySE("IceBreaking");
                        pC.iceExplosion.SetActive(true);
                        pC.iceCurse.SetActive(false);
                    }

                    ani.SetTrigger("Shot");
                    SoundManager.instance.PlaySE("SwordReady");
                    yield return new WaitForSeconds(0.5f);
                    SoundManager.instance.PlaySE("SwordJump");
                    pAni.Play(pC.jumpName, -1, 0f);
                    pR.velocity = new Vector2(0, 30f);
                    pC.jumpCount = 3;
                    yield return new WaitForSeconds(0.2f);
                    ani.SetTrigger("End");
                    ani.SetTrigger("Idle");
                    yield return new WaitForSeconds(1.8f);
                    cir.enabled = false;

                    gage.value = 0f;
                }

            }
        }
    }

    
}
