using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowManBolt : MonoBehaviour
{

    private GameObject target;
    private GameObject warning; //경고선
    private Transform targetTr;
    private Rigidbody2D rb;
    private bool isShot = false; //업데이트문이 돌아가다가 true가되면 멈추게

    private Animator ani;

    private Vector3 dir;
    private Vector2 dirVec;
    private float speed = 20;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        warning = transform.GetChild(0).gameObject;
        target = GameObject.Find("PlayerTr").transform.GetChild(PlayerPrefs.GetInt("Character", 0)).gameObject;
        targetTr = target.transform;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        if(warning.active == true)
        {
            warning.SetActive(false);
        }
        transform.position = transform.parent.position;
        isShot = false;
        StartCoroutine("Shot");
    }

    private void FixedUpdate()
    {
        if (isShot == false)
        {
            dir = targetTr.position - transform.position;
            dirVec = dir.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
        else if(isShot == true)
        {
            rb.velocity = new Vector2(dirVec.x, dirVec.y) * speed;
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(5f);
        warning.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        warning.SetActive(false);
        ani.SetTrigger("isShot");
        SoundManager.instance.PlaySE("Iattack1swoosh");
        isShot = true;    
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
