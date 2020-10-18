using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : MonoBehaviour
{

    public GameObject Monster;
    private Monster_Moving mm;
    public GameObject fireEffect;
    public GameObject iceEffect;
    public GameObject iceExplosionPre;


    private GameObject iceExplosion;
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private BoxCollider2D box;
    private Animator ani;

    private void Awake()
    {
        iceExplosion = Instantiate(iceExplosionPre, gameObject.transform);
        mm = transform.parent.gameObject.GetComponent<Monster_Moving>();
        rb = Monster.GetComponent<Rigidbody2D>();
        spr = Monster.GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        ani = Monster.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        iceExplosion.SetActive(false);
        box.enabled = true;
        spr.color = new Color32(255, 255, 255, 255);
        ani.speed = 1f;
        fireEffect.SetActive(false);
        iceEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            mm.rt = 0;
            SoundManager.instance.PlaySE("MonsterHit");
            StartCoroutine(Death());
            StartCoroutine(SuperAnimation());
            
        }

        if (collision.tag == "Fbullet")
        {
            mm.rt = 0;
            ani.speed = 0f;
            collision.gameObject.SetActive(false);
            box.enabled = false;          
            spr.color = new Color32(50, 50, 50, 255);
            SoundManager.instance.PlaySE("FireHit");
            fireEffect.SetActive(true);
            StartCoroutine(Death());

        }
        if (collision.tag == "Ibullet")
        {
            collision.gameObject.SetActive(false);
            box.enabled = false;
            spr.color = new Color32(50, 50, 50, 255);
            SoundManager.instance.PlaySE("CreateIce");
            iceEffect.SetActive(true);
            ani.speed = 0f;
            mm.rt = 0;
            mm.surveillance.SetActive(false);
            StartCoroutine(Broken());

        }
    }

    IEnumerator Death()
    {
        while(gameObject.active == true)
        {
            GameManager.instance.AchivementMonster();
            box.enabled = false;
            damageMotion();
            yield return new WaitForSeconds(1f);
            Monster.SetActive(false);
        }
    }

    IEnumerator SuperAnimation() //깜빡임 애니메이션
    {

        while (gameObject.active == true)
        {
            spr.color = new Color32(255, 255, 255, 130);
            yield return new WaitForSeconds(0.1f);
            spr.color = new Color32(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);

        }

    }
    public void damageMotion() //피격 모션
    {
        if (spr.flipX == true)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(-5, 10);   
        }

        if (spr.flipX == false)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity = new Vector2(5, 10);
        }
    }

    IEnumerator Broken()
    {
        yield return new WaitForSeconds(5.0f);
        box.enabled = true;
        spr.color = new Color32(255, 255, 255, 255);
        SoundManager.instance.PlaySE("IceBreaking");
        iceExplosion.gameObject.transform.position = gameObject.transform.position;
        iceExplosion.SetActive(true);
        iceEffect.SetActive(false);
        ani.speed = 1f;
        mm.rt = 1;
        mm.surveillance.SetActive(true);
    }
}
