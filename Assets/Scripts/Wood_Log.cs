using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Log : MonoBehaviour
{
    public GameObject woodPrefab; // 부숴지는 효과

    private int count;
    private Animator ani;
    private GameObject woodExp;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        woodExp = Instantiate(woodPrefab, transform.position, Quaternion.identity);
        

    }
    private void OnEnable()
    {
        woodExp.SetActive(false);
        count = 6;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (count == 0)
            {
                SoundManager.instance.PlaySE("Broken1");
                ani.SetTrigger("isAttack");
                woodExp.transform.position = transform.position;
                woodExp.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                SoundManager.instance.PlaySE("WoodHit");
                ani.SetTrigger("isAttack");
                count--;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Sword")
        {
            SoundManager.instance.PlaySE("Broken1");
            ani.SetTrigger("isAttack");
            woodExp.transform.position = transform.position;
            woodExp.SetActive(true);
            gameObject.SetActive(false);
        }

        if (collision.tag == "Fbullet" || collision.tag == "Ibullet")
        {
            if (count == 0)
            {
                SoundManager.instance.PlaySE("Broken1");
                ani.SetTrigger("isAttack");
                woodExp.transform.position = transform.position;
                woodExp.SetActive(true);
                gameObject.SetActive(false);
            }
            else
            {
                SoundManager.instance.PlaySE("WoodHit");
                collision.gameObject.SetActive(false);
                ani.SetTrigger("isAttack");
                count--;
            }
        }
        
    }
}
