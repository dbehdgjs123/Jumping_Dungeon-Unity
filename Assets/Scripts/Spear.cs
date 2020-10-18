using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    // Start is called before the first frame update

    private float speed = 0.4f; //내려갈때 속도
    private float hspeed = 2.0f; //올라갈때 속도
    public GameObject exp; //폭발 효과를 주기 위해 선언

    private Vector3 maxdistance;// 최대 지점 설정
    private Vector3 startPos; //회유 지점 설정
    private BoxCollider2D box;

    private GameObject bossTr;
    private GameObject fBoss;
    private GameObject iBoss;
    private GameObject sBoss;


    public static float rt = 1; //시간 배속을 설정(슬로우기능)
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        bossTr = GameObject.Find("BossTr");
        fBoss = bossTr.transform.Find("Boss_firetree").gameObject;
        iBoss = bossTr.transform.Find("Boss_snowMan").gameObject;
        sBoss = bossTr.transform.Find("Boss_skul").gameObject;
    }


    void OnEnable()
    {
        transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y - 0.3f);
        box.enabled = true;
        if (exp.active == true)
        {
            exp.SetActive(false);
        }
        
        maxdistance = new Vector3(transform.position.x, transform.parent.position.y - 0.6f, transform.position.z);
        StartCoroutine(UpSpear());

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword")
        {
            box.enabled = false;
            SoundManager.instance.PlaySE("Broken1");
            exp.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    IEnumerator UpSpear()
    {

        while (true) //무한 반복
        {
            yield return new WaitForSeconds(Random.Range(0f, 1.5f));
            while (Vector3.Distance(transform.position, maxdistance) > 0) //거리가 0보다 클경우 실행
            {
                if (fBoss.active == true || iBoss.active == true || sBoss.active == true)
                {
                    gameObject.SetActive(false);
                }
                
                transform.position = Vector3.MoveTowards(transform.position, maxdistance, hspeed * Time.deltaTime * TimeManager.brt);
                yield return new WaitForEndOfFrame(); //이 프레임만 실행

            }
            startPos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z); //돌아가는 위치 업데이트
            yield return new WaitForSeconds(2);
            while (Vector3.Distance(transform.position, startPos) > 0)
            {
                if (fBoss.active == true || iBoss.active == true || sBoss.active == true)
                {
                    gameObject.SetActive(false);
                }
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime * TimeManager.brt);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2);
        }

    }
}
