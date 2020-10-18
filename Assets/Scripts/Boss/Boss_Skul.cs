using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Skul : MonoBehaviour
{
    public static Animator ani;
    private BossApeearUi bau; //ui를 다시 원래대로 해놓기 위해 선언
    private SpriteRenderer spr; //투명도를 조절하기 위해 사용
    private BoxCollider2D box;

    public static float rt = 1f;
    public static float art = 1f;

    public GameObject bossBox; //클리어시 보상
    public GameObject hpBar; //보스 에너지 체력바
    public Slider hpSlider; //보스 체력 조절용 슬라이더

    public GameObject warningPre;
    public GameObject[] warning;
    public GameObject warningBoltPre;
    public GameObject[] warningBolt;

    public GameObject aBolt_L_pre;
    public GameObject aBolt_R_pre;   
    public Transform aTr_L;
    public Transform aTr_R;
    public Transform bTr;
    public Transform cTr; // 원형 패턴 총알이 나가는 입

    public GameObject Bboltpre; //b볼트 프리팹
    public GameObject Cboltpre; //c볼트 프리팹

    public GameObject deathWaveL; // 에너지파
    public GameObject deathWaveM;
    public GameObject deathWaveR;

    public GameObject weather; //날씨를 바꾸주기위해 선언

    private GameObject Cbolt; // c볼트를 담아놓을 변수
    private GameObject[] Bbolt; 
    private GameObject[] aBolt_L;
    private GameObject[] aBolt_R;
    private Transform[] Atr;
    private PlayerCamera pc;




    private void Awake()
    {
        pc = GameObject.Find("Main Camera").gameObject.GetComponent<PlayerCamera>();
        Cbolt = Instantiate(Cboltpre, cTr.position, Quaternion.identity);
        Cbolt.SetActive(false);
        bau = gameObject.GetComponent<BossApeearUi>();
        hpSlider = hpBar.GetComponent<Slider>();
        box = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        //양손에서 나가는 abolt 총 8개를 선언
        aBolt_L = new GameObject[4];
        aBolt_R = new GameObject[4];
        warningBolt = new GameObject[5];
        warning = new GameObject[5];
        Atr = new Transform[5];
        Bbolt = new GameObject[100];
        for (int i = 0; i<4; i++)
        {
            aBolt_L[i] = Instantiate(aBolt_L_pre, aTr_L.position, Quaternion.identity);
            aBolt_L[i].SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            aBolt_R[i] = Instantiate(aBolt_R_pre, aTr_R.position, Quaternion.identity);
            aBolt_R[i].SetActive(false);
        }

        for (int i = 0; i < 5; i++)
        {
            warningBolt[i] = Instantiate(warningBoltPre, aTr_R.position, warningBoltPre.transform.rotation);
            warningBolt[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            warning[i] = Instantiate(warningPre, aTr_R.position, Quaternion.identity);
            warning[i].SetActive(false);
        }
        for (int i = 0; i < 5; i++)
        {
            Atr[i] = gameObject.transform.GetChild(i).gameObject.transform;          
        }
        for(int i = 0; i<100; i++)
        {
            Bbolt[i] = Instantiate(Bboltpre, bTr.position, Quaternion.identity);
            Bbolt[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        weather.SetActive(false);

        if (bossBox.active == true)
        {
            bossBox.SetActive(false);
        }
        deathWaveL.SetActive(false);
        deathWaveM.SetActive(false);
        deathWaveR.SetActive(false);
        hpBar.SetActive(true);
        hpSlider.value = 1f;
        box.enabled = true;
        spr.color = new Color(1, 1, 1, 0);
        StartCoroutine("bossAppear");

    }

    void AppearShake()
    {
        PlayerCamera.shakeTimer = 2.0f;
        PlayerCamera.shakeAmount = 0.05f;

    }
    void EndShake()
    {
        PlayerCamera.isBossAppear = false;
    }

    IEnumerator bossAppear()
    {
        weather.SetActive(true);
        SoundManager.instance.BgmStop();
        SoundManager.instance.PlaySE("Bappear2");
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.PlayBgm("BossBgm3");
        AppearShake();
        while (spr.color.a < 1)
        {
            spr.color = new Color(1, 1, 1, spr.color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("bossCameraSize");
        StartCoroutine("bossPlayerTr");
        PlayerCamera.isBossAppear = true; //메인카메라의 bool값을 트루로하면 화면 흔들림
        yield return new WaitForSeconds(2f);
        EndShake();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("patternA");
    }
    IEnumerator bossCameraSize()
    { //카메라사이즈를 늘려주기 위해 함수선언
        pc.Ssky();
        
        while (PlayerCamera.camera.orthographicSize < 9)
        {
            PlayerCamera.camera.orthographicSize += 0.05f;
            yield return new WaitForSeconds(0.025f);
        }

    }
    IEnumerator bossPlayerTr()
    { //너무 가까이 있으면 안되므로 함수선언
        while (PlayerCamera.bossMaxY < 1.5f)
        {
            PlayerCamera.bossMaxY += 0.05f;
            yield return new WaitForSeconds(0.025f);
        }
    }



    IEnumerator bossCameraSizeBack()
    { //카메라사이즈를 줄여주기 위해 함수선언
        pc.Nsky();
        while (PlayerCamera.camera.orthographicSize > 7)
        {
            PlayerCamera.camera.orthographicSize -= 0.05f;
            yield return new WaitForSeconds(0.025f);
        }

    }

    IEnumerator bossPlayerTrBack()
    { //원래 카메라로 돌아옴
        while (PlayerCamera.bossMaxY > 0f)
        {
            PlayerCamera.bossMaxY -= 0.05f;
            yield return new WaitForSeconds(0.025f);
        }
    }

    private void ShotWarning()
    {
        for(int i = 0; i <5; i++)
        {
            if(Random.Range(0,3) == 0)
            {
                warning[i].transform.position = Atr[i].position;
                warning[i].SetActive(true);
            }
        }
    }

    private void ShotWarningBolt()
    {
        for (int i = 0; i < 5; i++)
        {
            if (warning[i].active == true)
            {
                warning[i].SetActive(false);
                warningBolt[i].transform.position = Atr[i].position;
                SoundManager.instance.PlaySE("Sattack1");
                warningBolt[i].SetActive(true);
                
            }
        }
    }
    private void ShotBbolt()
    {
        int acount = 1;
        for(int i = 0; i<100; i++)
        {
            Bbolt[i].transform.position = bTr.position;
            Bbolt[i].transform.rotation = Quaternion.identity;         
            Bbolt[i].SetActive(true);
            Rigidbody2D rigid = Bbolt[i].GetComponent<Rigidbody2D>();          
            Vector2 dirVec = new Vector2(Mathf.Sin((float)acount / 100), -1);           
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
            
            acount++;
        }
    }

    IEnumerator patternA()
    {
        int count = 0;
        while (count < 5)
        {
            ani.SetBool("isAttack2", true);
            yield return new WaitForSeconds(0.4f);
            ShotWarning();
            yield return new WaitForSeconds(0.1f);
            ShotWarningBolt();
            yield return new WaitForSeconds(0.6f);
            ani.SetBool("isAttack2", false);
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 5; i++)
            {
                if(warningBolt[i].active == true)
                {
                    warningBolt[i].SetActive(false);
                }
            }
            count++;
            

        }
        yield return new WaitForSeconds(2f);
        StartCoroutine("patternB");
    }
    IEnumerator patternB()
    {
        int count = 1;
        ani.SetBool("isAttack1", true);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 69; i++)
        {
            Bbolt[i].transform.position = bTr.position;
            Bbolt[i].transform.rotation = Quaternion.identity;
            SoundManager.instance.PlaySE("Sattack2");
            Bbolt[i].SetActive(true);
            Rigidbody2D rigid = Bbolt[i].GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 10 * count / 99), -1);
            rigid.AddForce(dirVec.normalized * 7, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.12f);
            count++;
        }
        ani.SetBool("isAttack1", false);
        yield return new WaitForSeconds(2f);
        if(hpSlider.value <= 0.6f)
        {
            StartCoroutine("patternC");
        }
        else
        {
            StartCoroutine("patternA");
        }
        
    }
    IEnumerator patternC()
    {
        yield return new WaitForSeconds(2f);
        ani.SetTrigger("isCircleAttack");
        yield return new WaitForSeconds(0.2f);
        Cbolt.transform.position = cTr.transform.position;
        Cbolt.SetActive(true);
        yield return new WaitForSeconds(12f);
        StartCoroutine("patternD");
    }

    IEnumerator patternD()
    {
        //SoundManager.instance.PlaySE("Sattack3Appear");
        int count = 0;
        ani.SetBool("isAttack3", true);
        while (count < 5)
        {

            int randPos = Random.Range(0, 3);
            if (randPos == 0)
            {
                yield return new WaitForSeconds(0.3f);
                deathWaveL.SetActive(true);
                yield return new WaitForSeconds(1.4f);
                deathWaveL.SetActive(false);             
            }
            else if (randPos == 1)
            {
                yield return new WaitForSeconds(0.3f);
                deathWaveM.SetActive(true);
                yield return new WaitForSeconds(1.4f);
                deathWaveM.SetActive(false);             
            }
            else if (randPos == 2)
            {
                yield return new WaitForSeconds(0.3f);
                deathWaveR.SetActive(true);
                yield return new WaitForSeconds(1.4f);
                deathWaveR.SetActive(false);              
            }
            count++;

        }
        yield return new WaitForSeconds(1f);
        ani.SetBool("isAttack3", false);
        yield return new WaitForSeconds(2f);
        StartCoroutine("patternB");
        
    }
   


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Fbullet")
        {
            SoundManager.instance.PlaySE("BossHit");
            collision.gameObject.SetActive(false);
            StartCoroutine("ShackeCamera");
            if (hpSlider.value <= 0f)
            {
                StopAllCoroutines();
                box.enabled = false;
                StartCoroutine("bossDie");
            }
            hpSlider.value -= 0.02f;
            onDamageColor();
        }
        if (collision.tag == "Ibullet")
        {
            SoundManager.instance.PlaySE("BossHit");
            collision.gameObject.SetActive(false);
            StartCoroutine("ShackeCamera");
            if (hpSlider.value <= 0f)
            {
                StopAllCoroutines();
                box.enabled = false;
                StartCoroutine("bossDie");
            }
            hpSlider.value -= 0.02f;
            onDamageColor();
        }
    }

    private void onDamageColor()
    {
        spr.color = new Color(1, 0, 0, 1);
        Invoke("BackColor", 0.1f);
    }
    private void BackColor()
    {
        spr.color = new Color(1, 1, 1, 1);
    }

    IEnumerator bossDie()
    {
        weather.SetActive(false);
        StartCoroutine("bossCameraSizeBack");
        StartCoroutine("bossPlayerTrBack");
        bau.Die();
        hpBar.SetActive(false);
        StartCoroutine("DieSound");
        ani.SetTrigger("isDie");
        while (spr.color.a > 0f)
        {
            spr.color = new Color(1, 1, 1, spr.color.a - 0.025f);
            yield return new WaitForSeconds(0.1f);
        }

        if (bossBox.active == true)
        {
            bossBox.SetActive(false);
            bossBox.transform.position = transform.position;
            bossBox.SetActive(true);
        }
        else
        {
            bossBox.transform.position = transform.position;
            bossBox.SetActive(true);
        }
        SoundManager.instance.PlayBgm("MainBgm");
        if (GameManager.instance.isFb == 0)
        {
            GameManager.instance.FbSave();
        }
        GoogleManager.instance.UnlockAchiv16();
        GoogleManager.instance.UnlockAchiv17();
        GoogleManager.instance.UnlockAchiv18();
        gameObject.SetActive(false);
    }

    IEnumerator ShackeCamera()
    {
        if (PlayerCamera.shakeTimer <= 0f)
        {
            PlayerCamera.shakeTimer = 0.1f; //흔들림 효과를 위해 선언
            PlayerCamera.shakeAmount = 0.3f;
            PlayerCamera.isBossAppear = true;
            yield return new WaitForSeconds(0.1f);

        }
    }

    IEnumerator DieSound()
    {
        while (gameObject.active == true)
        {
            SoundManager.instance.PlaySE("Bdie");
            yield return new WaitForSeconds(0.2f);
        }
    }

}
