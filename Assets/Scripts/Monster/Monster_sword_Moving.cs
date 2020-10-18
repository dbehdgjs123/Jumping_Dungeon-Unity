using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_sword_Moving : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb; //움직일때 사용
    private SpriteRenderer spr; //플립을 사용하기 위해 사용
    private Animator ani;
    private BoxCollider2D box;

    private Vector2 startPos;
    private RaycastHit hit;
    private Transform target; //타겟의 컴포넌트를 가져오기 위해 사용
    private Vector2 targetPos; //벡터로 저장하기 위해 사용
    private Coroutine move = null; //스탑코루틴을 쓰기위해 인자전달용 변수를 만들어야함
    private Coroutine discor = null;
    private Coroutine run = null;
    private Coroutine change = null;

    private GameObject ice;
    private GameObject bossTr;
    private GameObject fBoss;
    private GameObject iBoss;
    private GameObject sBoss;

    public int flag = 0; //0 idle 1: left 2: right
    private float grt; //게임매니저 변수를 담아두기 위해 설정 (슬로우)
    private float gart;
    public float rt = 1; //시간 배속을 설정(슬로우기능)
    public float art = 1;

    public float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();

        bossTr = GameObject.Find("BossTr");
        fBoss = bossTr.transform.Find("Boss_firetree").gameObject;
        iBoss = bossTr.transform.Find("Boss_snowMan").gameObject;
        sBoss = bossTr.transform.Find("Boss_skul").gameObject;

        ice = gameObject.transform.Find("Ice_effect").gameObject;


    }
    private void OnEnable()
    {
        rt = 1f;
        art = 1f;
        ani.enabled = true;
        flag = 0;
        tr.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.4f);
        startPos = new Vector2(transform.parent.position.x, tr.position.y);
        change = StartCoroutine("ChangeMoving");
        move = StartCoroutine("Moving");

    }

    private void FixedUpdate()
    {

        if (fBoss.active == true)
        {
            gameObject.SetActive(false);
        }
        if (iBoss.active == true)
        {
            gameObject.SetActive(false);
        }
        if (sBoss.active == true)
        {
            gameObject.SetActive(false);
        }
        //Boundary();
        if (rb.velocity.x > 0.1f)
        {
            ani.SetBool("isTracing", true);
            tr.localScale = new Vector2(-1, tr.localScale.y);
        }
        else if (rb.velocity.x < -0.1f)
        {
            ani.SetBool("isTracing", true);
            tr.localScale = new Vector2(1, tr.localScale.y);
        }
        else
        {
            ani.SetBool("isTracing", false);

        }

    }

    void Boundary()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0.15f)
        {
            pos.x = 0.15f;
        }
        if (pos.x > 0.85f)
        {
            pos.x = 0.85f;
        }

        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    IEnumerator Moving()
    {

        while (gameObject.active == true)
        {
            while (flag == 1)
            {
                rb.velocity = new Vector2(-speed, 0) * rt * TimeManager.rt;
                yield return new WaitForSeconds(0.1f);
            }

            while (flag == 2)
            {
                rb.velocity = new Vector2(speed, 0) * rt * TimeManager.rt;
                yield return new WaitForSeconds(0.1f);
            }

            while (flag == 0)
            {
                rb.velocity = new Vector2(0, 0) * rt * TimeManager.rt;
                yield return new WaitForSeconds(0.1f);
            }

        }
    }
    IEnumerator ChangeMoving()
    {

        while (gameObject.active == true)
        {
            yield return new WaitForSeconds(2f + TimeManager.art);
            flag = Random.Range(0, 3);
            Debug.Log(flag);
        }
    }

    IEnumerator Discorvery()
    {
        Debug.Log("발견!");
        SoundManager.instance.PlaySE("Alert2");
        rb.velocity = new Vector2(0, 0);
        ani.SetBool("isDiscorvery", true);
        yield return new WaitForSeconds(0.25f * TimeManager.art);
        if(run != null)
        {
            StopCoroutine(run);
        }
        ani.SetBool("isDiscorvery", false);
        run = StartCoroutine("RunTarget");

    }
    IEnumerator RunTarget()
    {
        Debug.Log("달려!");
        ani.SetTrigger("isAttackRange");
        SoundManager.instance.PlaySE("Msword");
        while (rb.position.x < targetPos.x) //거리가 0보다 클경우 실행)
        {
            rb.velocity = new Vector2(speed * 9, 0) * rt * TimeManager.rt;
            yield return new WaitForSeconds(0.1f);
        }

        while (rb.position.x > targetPos.x)
        {
            rb.velocity = new Vector2(-speed * 9, 0) * rt * TimeManager.rt;
            yield return new WaitForSeconds(0.1f);

        }
             
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (ice.active == true)
            {
                return;
            }
            Debug.Log("stop");
            
            target = collision.GetComponent<Transform>();
            if (target != null)
            {
                targetPos = new Vector2(target.position.x, tr.position.y);
            }
            if(change != null)
            {
                StopCoroutine(change);
                flag = 0;
            }
            
            if(move != null)
            {
                StopCoroutine(move);
            }

            if (discor != null)
            {
                StopCoroutine(discor);               
            }
            discor = StartCoroutine("Discorvery");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (ice.active == true)
            {
                return;
            }
            Debug.Log("어디갔지?");
            ani.SetBool("isDiscorvery", false);          
            if (discor != null)
            {
                StopCoroutine(discor);               
            }
            if (run != null)
            {
                StopCoroutine(run);
            }
            rb.velocity = new Vector2(0, 0);
            if (gameObject.active == true)
            {
                if (change != null)
                {
                    StopCoroutine(change);                   
                }
                if (move != null)
                {
                    StopCoroutine(move);
                }
                change = StartCoroutine("ChangeMoving");
                move = StartCoroutine("Moving");

            }
        }
    }
}
