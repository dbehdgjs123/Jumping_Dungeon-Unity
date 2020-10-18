using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerController playerController;

    private GameObject shield;
    private GameObject player;

    private GameObject swordTr;
    private GameObject sword;
    private CircleCollider2D swordCircle;
    private GameObject iceEffect; //얼음 저주
    private GameObject speedDown; //플레이어의 스피드를 내리기위해 선언


    public float speed;
    public static float rt = 1; //시간 배속을 설정(슬로우기능)
    public static float art = 1;
    // Start is called before the first frame update

    private void Awake()
    {
        if(GameManager.instance.isGameOver == false)
        {
            speedDown = GameObject.Find("Item Use").transform.Find("SpeedDown").gameObject;
        }
        
    }
    private void OnEnable()
    {
        swordTr = GameObject.Find("SwordObjectTr");
        sword = swordTr.transform.Find("Sword").gameObject;
        swordCircle = sword.GetComponent<CircleCollider2D>();
        //Invoke("reload", 3f);
        transform.position = transform.parent.position;
        StartCoroutine(reload());
    }
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        shield = GameObject.Find("Shield");     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, 0f) * TimeManager.brt;
        


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerController = collision.GetComponent<PlayerController>();
            if (iceEffect == null)
            {
                iceEffect = playerController.iceCurse;
            }
            if (playerController != null && playerController.shield.active == false && playerController.Super.active == false && playerController.Ishield.active == false && swordCircle.enabled == false && iceEffect.active == false)
            {
                SoundManager.instance.PlaySE("PlayerHit");
                playerController.damageMotion();
                if(speedDown.active == true)
                {
                    speedDown.SetActive(false);
                    speedDown.SetActive(true);
                }
                else
                {
                    speedDown.SetActive(true);
                }
                
                playerController.Super.SetActive(true);
                
            }

        }
    }

    IEnumerator reload()
    {
        

            yield return new WaitForSeconds(3.0f * TimeManager.art);
            gameObject.SetActive(false);
            transform.position = transform.parent.position;
        
    }
    
}
