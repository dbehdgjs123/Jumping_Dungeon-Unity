using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceBolt : MonoBehaviour
{
    Rigidbody2D rb;
    public float xSpeed; //x축의 속도
    public float ySpeed; //y축의 속도
    public float seconds; //생성 시간
    public float shotSeconds; //총알이 날아가길 기다리는 시간(3개의 간격차를 위해서)

    private SpriteRenderer Color; //투명도를 조절하기 위해 사용
    private TrailRenderer tR; //트레일렌더러의 길어지는 버그를 방지 (다시 부모의 트랜스폼으로 돌아올대)

    

    void Awake()
    {
        tR = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Color = GetComponent<SpriteRenderer>();
        Color.color = new Color(1, 1, 1, 0);
        rb.isKinematic = true;
    }

    private void OnEnable()
    {
        if (Color != null)
        {
            Color.color = new Color32(255, 255, 255, 0);

        }
        rb.isKinematic = true;
        if(tR.enabled == true) //트레일 렌더러가 길어지는 버그 방지
        {
            tR.enabled = false;
        }
        transform.position = transform.parent.position;
        tR.enabled = true;

        
            StartCoroutine(reload());
        
        
    }
 
    void Update()
    {
        if(rb.isKinematic == false)
        {
            rb.velocity = new Vector2(xSpeed, ySpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(seconds);
        SoundManager.instance.PlaySE("IceBallReload");
        while (Color.color.a <1)
        {
            Color.color = new Color(1, 1, 1, Color.color.a + 0.1f );
            yield return new WaitForSeconds(0.1f);
        }
        if(gameObject.transform.parent.gameObject.name == "IceTr3")
        {
            SoundManager.instance.PlaySE("IceBallSound");
        }

        while (gameObject.active == true)
        {
            yield return new WaitForSeconds(shotSeconds);
            rb.isKinematic = false;
            yield return new WaitForSeconds(1.5f);
            tR.enabled = false;
            gameObject.SetActive(false);

        }
       
    }

}
