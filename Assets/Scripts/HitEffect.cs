using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public GameObject fireEffect; // 불을 맞았을때 피격
    public GameObject iceEffect; // 아이스를 맞았을때 피격
    public GameObject iceExplosion;

    private SpriteRenderer spr;
    private BoxCollider2D box; //충돌을 없애주기 위해 선언
    private Animator ani; //아이스를 맞았을때 애니메이션을 중지시키기 위해 선언


    private void OnEnable()
    {
        if (box != null && box.enabled == false)
        {
            box.enabled = true;
        }      
        fireEffect.SetActive(false);
        iceEffect.SetActive(false);
        iceExplosion.SetActive(false);
        if (spr != null)
        {
            spr.color = new Color32(255, 255, 255, 255);
        }
        if (ani != null)
        {
            ani.speed = 1f;
        }
    }

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fbullet")
        {
            
            collision.gameObject.SetActive(false);
            box.enabled = false;           
            spr.color = new Color32(50, 50, 50, 255);
            SoundManager.instance.PlaySE("FireHit");
            fireEffect.SetActive(true);
            GameManager.instance.AchivementMonster();
            Invoke("damage", 1f);
                       
        }
        if (collision.tag == "Ibullet")
        {
            collision.gameObject.SetActive(false);
            box.enabled = false;           
            spr.color = new Color32(50, 50, 50, 255);
            SoundManager.instance.PlaySE("CreateIce");
            iceEffect.SetActive(true);
            ani.speed = 0f;
            StartCoroutine(Broken());

        }
    }

    public void damage()
    {
        gameObject.SetActive(false);
    }
    IEnumerator Broken()
    {
        yield return new WaitForSeconds(5.0f);
        box.enabled = true;    
        spr.color = new Color32(255, 255, 255, 255);
        SoundManager.instance.PlaySE("IceBreaking");
        iceExplosion.SetActive(true);
        iceEffect.SetActive(false);
        ani.speed = 1f;

    }

}
