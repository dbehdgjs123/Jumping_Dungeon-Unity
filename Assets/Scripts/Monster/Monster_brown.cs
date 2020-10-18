using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster_brown : MonoBehaviour
{
    Animator ani;

    public GameObject bolt;
    public GameObject iceEffect; // 아이스를 맞았을때 피격
    public GameObject explosion; //죽을때 효과 발동
    public Transform str; //총알의 발사 위치를 지정
    public float rt = 1; //시간 배속을 설정(슬로우기능)
    public float art = 1;
    
    private int count = 5; //총알 생성수
    private GameObject[] bullet; //오브젝트 풀링을 하기 위해 배열에 선언.

    private GameObject bossTr;
    private GameObject fBoss;
    private GameObject iBoss;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        bullet = new GameObject[count]; //총알 생성

        for (int i = 0; i < count; i++) //총알을 str포지션 오브젝트의 자식으로 설정한다.
        {
            bullet[i] = Instantiate(bolt, str);
            bullet[i].SetActive(false);
        }

        bossTr = GameObject.Find("BossTr");
        fBoss = bossTr.transform.Find("Boss_firetree").gameObject;
        iBoss = bossTr.transform.Find("Boss_snowMan").gameObject;
    }
    private void OnEnable()
    {
        
        for (int i = 0; i < count; i++) //총알을 str포지션 오브젝트의 자식으로 설정한다.
        {
            bullet[i].SetActive(false);
        }
        if (explosion.active == true)
        {
            explosion.SetActive(false); //생성될때 파티클 실행 방지
        }
        ani.SetBool("isAppear", true);
        ani.SetBool("isShot", true);
        StartCoroutine(ShotAni()); //샷
        //StartCoroutine(Shot());   
    }
    void Start()
    {
        
        


    }

    // Update is called once per frame
    /*void Update()
    {
        if (ani != null)
        {
            ani.speed = rt;
        }
    }*/


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Sword")
        {
            SoundManager.instance.PlaySE("MonsterHit");
            GameManager.instance.AchivementMonster();
            explosion.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    IEnumerator ShotAni()
    {

        int count = 0;
        while (gameObject.active == true)
        {
            if (fBoss.active == true || iBoss.active == true)
            {
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(Random.Range(2f,3f) * TimeManager.art);        
            ani.SetBool("isAppear", false);
            while(ani.GetCurrentAnimatorStateInfo(0).normalizedTime <1.0)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.5f * TimeManager.art);
            ani.SetBool("isShot", false);
            while (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.7f * TimeManager.art);
            while (count < 5)
            {
                ani.SetTrigger("Shot");
                
                while (iceEffect.active == true)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(0.2f);
                SoundManager.instance.PlaySE("CannonShotSound");
                if (bullet[count].active == true)
                {
                    bullet[count].SetActive(false);
                    bullet[count].SetActive(true);
                }
                else
                {
                    bullet[count].SetActive(true);
                }
                count++;
                yield return new WaitForSeconds(0.3f * TimeManager.art);
            }
            
            count = 0;
            ani.SetBool("isShot", true);
            while (ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0)
            {
                yield return new WaitForEndOfFrame();
            }
            ani.SetBool("isAppear", true);
            yield return new WaitForSeconds(1.5f * TimeManager.art);
            
        }




    }

}
