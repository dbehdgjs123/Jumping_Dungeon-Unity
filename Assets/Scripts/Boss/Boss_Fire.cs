using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Fire : MonoBehaviour
{

    public GameObject attackAbolt; //패턴a 총알
    public GameObject attackBbolt; //패턴b 총알
    public GameObject hpBar; //보스 에너지 체력바
    public Slider hpSlider; //보스 체력 조절용 슬라이더
    public GameObject EwaveL; //왼손 에너지파
    public GameObject EwaveR; //오른손 에너지파
    public GameObject Wood; //통나무
    public GameObject bossBox; //클리어시 보상

    public GameObject weather; //날씨를 바꾸주기위해 선언


    //부채꼴 모양의 패턴
    private int BboltAmount = 5; //총알의 양
    [SerializeField] private float startAngle = 90f; //시작 각도
    [SerializeField] private float endAngle = 270f; //끝 각도
    private Vector2 boltMoveDirection; //단위벡터를 위해 선언

    public Transform attackATr;
    public Transform attackBTr;
    public Transform attackCTr;

    private GameObject[] attackApool;
    private GameObject[] attackBpool;
    private GameObject[] attackCpool;

    public static  Animator ani;
    private SpriteRenderer spr; //투명도를 조절하기 위해 사용
    private BossApeearUi bau; //ui를 다시 원래대로 해놓기 위해 선언
    private BoxCollider2D box;
    private PlayerCamera pc;

    public static float rt = 1f;
    public static float art = 1f;


    private void Awake()
    {
        pc = GameObject.Find("Main Camera").gameObject.GetComponent<PlayerCamera>();
        box = GetComponent<BoxCollider2D>();
        hpSlider = hpBar.GetComponent<Slider>();
        spr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        bau = gameObject.GetComponent<BossApeearUi>();
        attackApool = new GameObject[4];
        attackBpool = new GameObject[36];
        attackCpool = new GameObject[5];

        if (attackAbolt != null)
        {
            for (int i = 0; i < 4; i++)
            {
                attackApool[i] = Instantiate(attackAbolt, attackATr.position, Quaternion.identity);
                attackApool[i].SetActive(false);

            }
        }
        if (attackBbolt != null)
        {
            for (int i = 0; i < 36; i++)
            {
                attackBpool[i] = Instantiate(attackBbolt, attackBTr.position, Quaternion.identity);
                attackBpool[i].SetActive(false);

            }
        }
        if (Wood != null)
        {
            for (int i = 0; i < 5; i++)
            {
                attackCpool[i] = Instantiate(Wood, transform.position, Quaternion.identity);
                attackCpool[i].SetActive(false);
            }
        }
    }
    private void OnEnable()
    {

        weather.SetActive(false);
        if(bossBox.active == true)
        {
            bossBox.SetActive(false);
        }
        box.enabled = true;
        hpBar.SetActive(true);
        hpSlider.value = 1f;
        EwaveL.SetActive(false);
        EwaveR.SetActive(false);
        spr.color = new Color(1, 1, 1, 0);
        StartCoroutine("bossAppear");

    }


    void AppearShake()
    {     
        ani.SetTrigger("isAppear");       
        PlayerCamera.shakeTimer = 1f;
        PlayerCamera.shakeAmount = 0.05f;

    }
    void EndShake()
    {
        ani.SetTrigger("isAppearEnd");
        SoundManager.instance.PlayBgm("BossBgm2");
        //PlayerCamera.isBossAppear = false;
    }

    IEnumerator bossAppear()
    {
        SoundManager.instance.BgmStop();
        weather.SetActive(true);
        while (spr.color.a < 1)
        {
            spr.color = new Color(1, 1, 1, spr.color.a + 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine("bossCameraSize");
        StartCoroutine("bossPlayerTr");
        Invoke("AppearShake", 0.6f);    
        yield return new WaitForSeconds(1.4f);
        SoundManager.instance.PlaySE("Bappear1");
        PlayerCamera.isBossAppear = true; //메인카메라의 bool값을 트루로하면 화면 흔들림
        yield return new WaitForSeconds(1f);
        EndShake();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("patternA");
    }
    IEnumerator bossCameraSize()
    { //카메라사이즈를 늘려주기 위해 함수선언, 하늘색 바꿔줌
        pc.Fsky();
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

    IEnumerator patternA()
    {
        while (hpSlider.value > 0.5f)
        {
            int count = 0;
            yield return new WaitForSeconds(2f);
            while (count < 4)
            {
                Debug.Log("패턴a공격");
                ani.SetTrigger("isAttack2");
                yield return new WaitForSeconds(0.5f*art);
                SoundManager.instance.PlaySE("Fattack1");
                attackApool[count].transform.position = attackATr.position;
                attackApool[count].SetActive(true);
                yield return new WaitForSeconds(0.5f * art);
                ani.SetTrigger("isAttack2End");
                yield return new WaitForSeconds(2f * art);
                count++;
            }
            count = 0;
            yield return new WaitForSeconds(2f);

            while (count < 3)
            {
                ani.SetTrigger("isAttackEnergy");
                yield return new WaitForSeconds(0.5f * art);
                if (count == 0 || count == 1)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        EwaveL.SetActive(true);
                    }
                    else
                    {
                        EwaveR.SetActive(true);
                    }
                }
                else
                {
                    EwaveL.SetActive(true);
                    EwaveR.SetActive(true);
                }


                yield return new WaitForSeconds(1.4f);
                if (EwaveL.active == true)
                {
                    EwaveL.SetActive(false);
                }
                if (EwaveR.active == true)
                {
                    EwaveR.SetActive(false);
                }
                ani.SetTrigger("isAttackEnergyEnd");
                count++;
            }
        }
        StartCoroutine("patternB");

    }

    IEnumerator patternB()
    {
        int attackCount = 0;
        int patternCount = Random.Range(1, 4);
        while (attackCount < patternCount)
        {
            ani.SetBool("isAttack1", false);
            int pcount = 0; //오브젝트풀링을 위해 선언 i값을 증가시킴
            int amount = 5; //오브젝트풀링을위해 총알의 배열을 바꾸기 위해 선언
            int count = 0;   //6번씩 쏘도록 선언  
            float angleStep = (endAngle - startAngle) / BboltAmount; //균등하게 나가도록 하기 위해 선언
            float angle = startAngle; //각도
            yield return new WaitForSeconds(2f);
            ani.SetBool("isAttack1", true);
            yield return new WaitForSeconds(1f);
            while (count < 6)
            {
                SoundManager.instance.PlaySE("Fattack3");
                for (int i = pcount; i < amount + 1; i++)
                {
                    float dirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float dirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
                    Vector3 boltMoveVec = new Vector3(dirX, dirY, 0f);
                    Vector2 dirvec = (boltMoveVec - transform.position).normalized;
                    attackBpool[i].transform.position = attackBTr.position;
                    attackBpool[i].transform.rotation = transform.rotation;
                    attackBpool[i].GetComponent<BoltHellBolt>().SetMoveDirection(dirvec);
                    attackBpool[i].SetActive(true);

                    angle += angleStep;
                }
                pcount += 6;
                amount += 6;
                angleStep = (endAngle - startAngle) / BboltAmount;
                angle = startAngle;
                count++;
                yield return new WaitForSeconds(0.5f * art);
            }
            attackCount++;
            yield return new WaitForSeconds(0.3f);
            if (attackCount >= patternCount)
            {
                ani.SetBool("isAttack1", false);
            }


        }
        StartCoroutine("patternC");
    }
    IEnumerator patternC()
    {
        yield return new WaitForSeconds(0.5f);
        int count = 0; //bool값 비슷하게 쓰기위해 선언 1이 되면 whil문을 빠져나가게
        int i = 0;
        while (count < 1)
        {
            
            if (attackCpool[i].active == true) //액티브하려는 오브젝트가 이미 액티브 되어있을때 실행
            {
                if (i == 4) //만약 풀링의 마지막순번이면 실행
                {
                    for (int j = 0; j < 4; j++)
                    {
                        attackCpool[j].SetActive(false);
                    }
                    i = 0;
                }
                else
                {
                    i++;
                }

            }
            else
            {
                ani.SetBool("isAttack3", true);
                yield return new WaitForSeconds(1.3f * art);
                PlayerCamera.shakeTimer = 0.4f; //흔들림 효과를 위해 선언
                PlayerCamera.shakeAmount = 0.05f;
                PlayerCamera.isBossAppear = true;
                attackCpool[i].transform.position = attackCTr.position;
                SoundManager.instance.PlaySE("Fattack4");
                attackCpool[i].SetActive(true);
                yield return new WaitForSeconds(0.5f);
                PlayerCamera.isBossAppear = false;
                ani.SetBool("isAttack3", false);
                count++;
            }



        }
        StartCoroutine("patternB");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Fbullet")
        {
            SoundManager.instance.PlaySE("BossHit");
            collision.gameObject.SetActive(false);
            StartCoroutine("ShackeCamera");
            if(hpSlider.value <= 0f)
            {
                StopAllCoroutines();
                box.enabled = false;
                StartCoroutine("bossDie");
            }
            hpSlider.value -= 0.012f;
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
            hpSlider.value -= 0.035f;
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
        if (PlayerCamera.isBossAppear == true)
        {
            PlayerCamera.isBossAppear = false;
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
        while(gameObject.active == true)
        {
            SoundManager.instance.PlaySE("Bdie");
            yield return new WaitForSeconds(0.2f);
        }
    }
}
