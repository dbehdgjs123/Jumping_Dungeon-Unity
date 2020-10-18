using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 4f;
    public float jump = 8.5f;
    private float dSpeed = 2.5f;
    private float dJump = 7.0f;
    private float wSpeed = 5f;
    private float wJump = 11.7f;
    public int count = 0; //쉴드 애니메이션 깜빡임의 정도

    public string jumpName; //각각 다른 애니메이션을 구성하기 위해선언
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    public GameObject shield;
    public GameObject Ishield;
    public GameObject iceCurse; //얼음저주
    public GameObject Super; //피격 상태 (무적)
    public GameObject[] itemEffect; //아이템 이펙트
    public GameObject bW; //흑백화면(타임스톱)

    public GameObject wings; //날개 아이템
    public GameObject fire; //화염 아이템
    public GameObject ice; //얼음 아이템
    public GameObject sword; //칼 아이템
    public GameObject bar; //칼 게이지 아이템
    public GameObject speedDown; //bolt를 맞았을때 생기는 효과

    public GameObject iceExplosion; //얼틈 터지는 효과


    public int jumpCount;
    public int curseCount;
    private int moveCount;
    private Rigidbody2D rb;
    private Animator ani;
    public int hp = 3; //플레이어의 하트 갯수
    
    SpriteRenderer spar;
    
    // Start is called before the first frame update

    private void OnEnable()
    {
        hp = 3; // 플레이어가 부활하면 하트 세개로 복구
        life1.SetActive(true);
        life2.SetActive(true);
        life3.SetActive(true);            
        Ishield.SetActive(false);
        wings.SetActive(false); //날개 해제
        fire.SetActive(false); //화염 해제
        ice.SetActive(false); //얼음 해제
        sword.SetActive(false); //칼 해제
        bar.SetActive(false);
        iceCurse.SetActive(false);
        iceExplosion.SetActive(false);
        speedDown.SetActive(false);
        if (GameManager.instance.isRevive == true)
        {           
            if(shield.active == false)
            {
                shield.SetActive(true);
            }          
            rb.velocity = new Vector2(0, 40f);
            ani.Play(jumpName, -1, 0f);
        }
        else
        {
            shield.SetActive(false);
        }
        if (bW.active == true)
        {
            bW.SetActive(false); //흑백 화면 해제
        }

    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        spar = GetComponent<SpriteRenderer>();    //플립 기능을 쓰기 위해
        jumpCount = 0;
        moveCount = 0;
        Super.SetActive(false); //처음 시작했을때는 무적 해제
        
             

    }

    // Update is called once per frame
    void Update()
    {

        jumpingAnimation();
        //playerBoundary();         

    }

    public void RightMove() //오른쪽 버튼 클릭에 대응
    {

        if (GameManager.instance.isGameOver == false)
        {

            if (moveCount < 3 && iceCurse.active == false)
            {
                SoundManager.instance.PlaySE("SideMove");
                ani.SetTrigger("isDash");
                if (jumpCount == 0)
                {
                    if (speedDown.active == true)
                    {
                        speedDown.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.25f);
                        rb.velocity = new Vector2(dSpeed, 0.1f);
                        spar.flipX = true;
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed, 0.1f);
                        spar.flipX = true;
                    }
                }
                else
                {
                    if (speedDown.active == true)
                    {

                        moveCount++;
                        speedDown.transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.25f);
                        rb.velocity = new Vector2(dSpeed, 0.1f);
                        spar.flipX = true;
                    }
                    else
                    {
                        moveCount++;
                        rb.velocity = new Vector2(speed, 0.1f);
                        spar.flipX = true;
                    }
                }


            }
            else if (iceCurse.active == true)
            {
                if (curseCount < 15)
                {
                    SoundManager.instance.PlaySE("CantMove");
                    spar.flipX = true;
                    curseCount++;
                }
                else
                {
                    curseCount = 0;
                    SoundManager.instance.PlaySE("IceBreaking");
                    iceExplosion.SetActive(true);
                    iceCurse.SetActive(false);
                    rb.velocity = new Vector2(speed, 0.1f);
                }
            }
            
        }
    }
    public void LeftMove()
    { //왼쪽 버튼 클릭에 대응

        if (GameManager.instance.isGameOver == false)
        {
            if (moveCount < 3 && iceCurse.active == false)
            {
                SoundManager.instance.PlaySE("SideMove");
                ani.SetTrigger("isDash");
                if (jumpCount == 0)
                {
                    if (speedDown.active == true)
                    {
                        speedDown.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.25f);
                        rb.velocity = new Vector2(-dSpeed, 0.1f);
                        spar.flipX = false;
                    }
                    else
                    {
                        rb.velocity = new Vector2(-speed, 0.1f);
                        spar.flipX = false;
                    }
                }
                else
                {
                    if (speedDown.active == true)
                    {
                        moveCount++;
                        speedDown.transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.25f);
                        rb.velocity = new Vector2(-dSpeed, 0.1f);
                        spar.flipX = false;
                    }
                    else
                    {
                        moveCount++;
                        rb.velocity = new Vector2(-speed, 0.1f);
                        spar.flipX = false;
                    }
                }


            }
            else if (iceCurse.active == true)
            {
                if (curseCount < 15)
                {
                    SoundManager.instance.PlaySE("CantMove");
                    spar.flipX = false;
                    curseCount++;
                }
                else
                {
                    curseCount = 0;
                    SoundManager.instance.PlaySE("IceBreaking");
                    iceExplosion.SetActive(true);
                    iceCurse.SetActive(false);
                    rb.velocity = new Vector2(-speed, 0.1f);
                }
            }
        }
    }

    public void jumpeMove()
    {
        if (GameManager.instance.isGameOver == false)
        {
            if (jumpCount < 3 && iceCurse.active == false)
            {
                if (wings.active == true)
                {
                    rb.velocity = new Vector2(0f, wJump);
                    SoundManager.instance.PlaySE("WingJump");
                }
                else
                {
                    if (speedDown.active == true)
                    {
                        rb.velocity = new Vector2(0f, dJump);
                        SoundManager.instance.PlaySE("UpMove");
                    }
                    else
                    {
                        rb.velocity = new Vector2(0f, jump);
                        SoundManager.instance.PlaySE("UpMove");
                    }

                }
                jumpCount++;
                ani.Play(jumpName, -1, 0f);
            }
            else if (iceCurse.active == true)
            {
                if (curseCount < 15)
                {
                    SoundManager.instance.PlaySE("CantMove");
                    curseCount++;
                }
                else
                {
                    curseCount = 0;
                    SoundManager.instance.PlaySE("IceBreaking");
                    iceExplosion.SetActive(true);
                    iceCurse.SetActive(false);
                    rb.velocity = new Vector2(0f, jump);
                }
            }
        }
    }

    void jumpingAnimation()
    {
        if (rb.velocity.y > 0f)
        {
            ani.SetBool("isJumping", true);
            ani.SetBool("isFalling", false);
        }
        if (rb.velocity.y < -1f)
        {
            ani.SetBool("isJumping", false);
            ani.SetBool("isFalling", true);
        }
        if (rb.velocity.y == 0f)
        {
            ani.SetBool("isFalling", false);
        }
    }

    void playerBoundary()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if(pos.x <0.05f)
        {
            pos.x = 0.05f;
        }
        if (pos.x > 0.95f)
        {
            pos.x = 0.95f;
        }
        if (pos.y < 0.1f)
        {
            pos.y = 0.1f;
        }
        if (pos.y > 1f)
        {
            pos.y = 1f;
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f) //첫 번째 충동값이 위를 보게하고 그 각도를 0.7 (대각선) 만큼 설정
        {
            ani.SetBool("isGrounded", true); // 바닥에 닿았을때 애니메이션

            moveCount = 0;
            jumpCount = 0;
        }

        if(collision.contacts[0].normal.y > 0.7f && collision.gameObject.tag == "Spring") //스프링을 밟았을때
        {
            Debug.Log("is Jumping");
            rb.velocity = Vector2.zero;
            SoundManager.instance.PlaySE("Boing");
            rb.velocity = new Vector2(0, 40f);
            ani.Play(jumpName, -1, 0f);
          
            if(shield.active == true)
            {
                shield.SetActive(false);
                shield.SetActive(true);
            }
            shield.SetActive(true);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f && moveCount >3 && jumpCount > 3)
        {
            moveCount = 0;
            jumpCount = 0;           
        }
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" && shield.active == false && GameManager.instance.isGameOver == false)
        {
            hp = 0;
            onDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water" && shield.active == false && GameManager.instance.isGameOver == false)
        {
            hp = 0;
            onDamage();
        }
        if(collision.tag == "Item_heart")
        {
            SoundManager.instance.PlaySE("HeartItemSound");
            restore();
            
        }
        
        if(collision.tag == "Item_shield")
        {
            SoundManager.instance.PlaySE("ShieldSound");
            itemEffect[0].SetActive(true);
            if(Ishield.active == true)
            {
                Ishield.SetActive(false);
                Ishield.SetActive(true);
            }
            else
            {
                Ishield.SetActive(true);
            }
            
        }
        if (collision.tag == "Item_wing")
        {
            if(wings.active == true)
            {
                wings.SetActive(false);
                wings.SetActive(true);
            }
            else
            {
                wings.SetActive(true);
            }
            SoundManager.instance.PlaySE("WingSound");
            itemEffect[1].SetActive(true);           
        }
        if (collision.tag == "Item_sword")
        {
            if (sword.active == true)
            {
                sword.SetActive(false);
                sword.SetActive(true);
            }
            else
            {
                sword.SetActive(true);
            }
            SoundManager.instance.PlaySE("SwordItemSound");
            itemEffect[2].SetActive(true);
            
        }
        if (collision.tag == "Item_snow")
        {
            if (ice.active == true)
            {
                ice.SetActive(false);
                ice.SetActive(true);
            }
            else
            {
                ice.SetActive(true);
            }
            SoundManager.instance.PlaySE("IceItemSound");
            itemEffect[3].SetActive(true);          
        }
        if (collision.tag == "Item_fire")
        {
            if (fire.active == true)
            {
                fire.SetActive(false);
                fire.SetActive(true);
            }
            else
            {
                fire.SetActive(true);
            }
            SoundManager.instance.PlaySE("FireItemSound");
            itemEffect[4].SetActive(true);
            
        }
        if (collision.tag == "Item_clock")
        {
            if(bW.active == true)
            {
                return;
            }
            SoundManager.instance.PlaySE("Clock");
            itemEffect[5].SetActive(true);
            TimeManager.rt = 0.5f;
            TimeManager.brt = 0.1f;
            TimeManager.art = 10f;      
                
            /*
            Boss_Fire.art = 10f;
            Boss_Fire.ani.speed = 0.1f;
            Boss_bolt.rt = 0.1f;
            BoltHellBolt.rt = 10f;
            BoltHellBolt.art = 10f;
            EnergyWave.art = 10f;
            */
            
            bW.SetActive(true);
            Invoke("ClockEnd", 10f);
            
            
        }
    }
    public void onDamage()
    {
        if(GameManager.instance.isGameOver == false) {
            switch (hp) //플레이어가 피해를 입었을때
            {

                case 2: //hp가 2인경우 하트 하나가 소모된다.                
                    life3.SetActive(false);
                    break;
                case 1: //hp가 1인경우 하트 하나가 소모된다.
                    if (life3.active == true)
                    {
                        life3.SetActive(false);
                    }
                    life2.SetActive(false);
                    break;
                case 0: //없으면 죽는다.
                    if (life3.active == true)
                    {
                        life3.SetActive(false);
                    }
                    if (life2.active == true)
                    {
                        life2.SetActive(false);
                    }
                    life1.SetActive(false);
                    Debug.Log("Die");
                    GameManager.instance.isGameOver = true;
                    ani.SetTrigger("isDie");
                    SoundManager.instance.PlaySE("PlayerDie");                  
                    Invoke("DieMotion", 1.5f);
                    break;
            }

        }
    }
        
    public void DieMotion()
    {
        if (Random.RandomRange(0, 2) == 0 && GameManager.instance.isRevive == false)
        {
            GameManager.instance.Chance();
        }
        else
        {
            GameManager.instance.Die();
        }
    }
    public void restore()
    {
        switch (hp) //플레이어가 회복 아이템을 먹었을때
        {
            case 3:
                if(life3.active == false)
                {
                    life3.SetActive(true);
                }
                break;
            case 2: //hp가 2인경우 하트 하나가 회복된다.
                if (life2.active == false)
                {
                    life2.SetActive(true);
                }
                if (life1.active == false)
                {
                    life1.SetActive(true);
                }
                life3.SetActive(true);
                hp++;
                break;
            case 1: //hp가 1인경우 하트 하나가 회복된다.
                life2.SetActive(true);
                hp++;
                break;

        }
    }
    public void damageMotion() //피격 모션
    {
        if(spar.flipX == true)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(-1.5f, 2.5f);
        }

        if (spar.flipX == false)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(1.5f, 2.5f);
        }
    }
    public void ClockEnd()
    {
        TimeManager.brt = 1f;
        TimeManager.rt = 1f;
        TimeManager.art = 1f;
        /*Boss_Fire.art = 1f;
        Boss_Fire.ani.speed = 1f;
        Boss_bolt.rt = 1f;
        BoltHellBolt.rt = 1f;
        BoltHellBolt.art = 0f;
        EnergyWave.art = 1f;
        */
        bW.SetActive(false);
    }


}

