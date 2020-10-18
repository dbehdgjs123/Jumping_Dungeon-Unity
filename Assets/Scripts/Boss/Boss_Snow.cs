using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Snow : MonoBehaviour
{

    public static Animator ani;
    private BossApeearUi bau; //ui를 다시 원래대로 해놓기 위해 선언
    private SpriteRenderer spr; //투명도를 조절하기 위해 사용
    private BoxCollider2D box;
    private PlayerCamera pc;

    public static float rt = 1f;
    public static float art = 1f;

    public GameObject bossBox; //클리어시 보상
    public GameObject iceEnergyL; // 얼음에너지파
    public GameObject iceEnergyM;
    public GameObject iceEnergyR;
    public GameObject attackHole_L;
    public GameObject attackHole_R;

    public GameObject hpBar; //보스 에너지 체력바
    public Slider hpSlider; //보스 체력 조절용 슬라이더

    public GameObject SnowManBoltLuncher; // 패턴 b 총알 오브젝트

    public GameObject weather; //날씨를 바꾸주기위해 선언




    private void Awake()
    {
        pc = GameObject.Find("Main Camera").gameObject.GetComponent<PlayerCamera>();
        bau = gameObject.GetComponent<BossApeearUi>();
        hpSlider = hpBar.GetComponent<Slider>();
        box = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        weather.SetActive(false);

        if (bossBox.active == true)
        {
            bossBox.SetActive(false);
        }
        hpBar.SetActive(true);
        hpSlider.value = 1f;        
        SnowManBoltLuncher.SetActive(false);
        iceEnergyL.SetActive(false);
        iceEnergyM.SetActive(false);
        iceEnergyR.SetActive(false);
        attackHole_L.SetActive(false);
        attackHole_R.SetActive(false);
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
        SoundManager.instance.BgmStop();
        SoundManager.instance.PlaySE("Winter");
        yield return new WaitForSeconds(1.5f);
        SoundManager.instance.PlayBgm("BossBgm1");
        weather.SetActive(true);

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
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("patternA");
    }
    IEnumerator bossCameraSize()
    { //카메라사이즈를 늘려주기 위해 함수선언
        pc.Isky();
        
        while (PlayerCamera.camera.orthographicSize < 8)
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

    IEnumerator patternA() //얼음파
    {
        int count = 0;      
        
        while (count < 5)
        {
            
            int randPos = Random.Range(0, 3);
            if (randPos == 0)
            {
                ani.SetBool("isAttack1", true);
                yield return new WaitForSeconds(0.2f);
                iceEnergyL.SetActive(true);
                yield return new WaitForSeconds(1.4f);
                iceEnergyL.SetActive(false);
                ani.SetBool("isAttack1", false);
            }
            else if(randPos == 1)
            {
                ani.SetBool("isAttack1", true);
                yield return new WaitForSeconds(0.2f);
                iceEnergyM.SetActive(true);
                yield return new WaitForSeconds(1.4f);
                iceEnergyM.SetActive(false);
                ani.SetBool("isAttack1", false);
            }
            else if(randPos == 2)
            {
                ani.SetBool("isAttack1", true);
                yield return new WaitForSeconds(0.2f);
                iceEnergyR.SetActive(true);
                yield return new WaitForSeconds(1.4f);
                iceEnergyR.SetActive(false);
                ani.SetBool("isAttack1", false);
            }
            count++;
                      

        }
        StartCoroutine("patternB");
        
    }

    IEnumerator patternB()
    {
        SnowManBoltLuncher.SetActive(true);
        yield return new WaitForSeconds(12f);
        SnowManBoltLuncher.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        if(hpSlider.value < 0.7f)
        {
            ani.SetBool("isAttack2", true);
            attackHole_L.SetActive(true);
            attackHole_R.SetActive(true);
            yield return new WaitForSeconds(14f);
            attackHole_L.SetActive(false);
            attackHole_R.SetActive(false);
            ani.SetBool("isAttack2", false);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("patternA");
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
            hpSlider.value -= 0.03f;
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
            hpSlider.value -= 0.025f;
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
        if(SnowManBoltLuncher.active == true)
        {
            SnowManBoltLuncher.SetActive(false);
        }
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
        if(GameManager.instance.isFb == 0)
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
            yield return new WaitForSeconds(0.3f);
        }
    }

}
