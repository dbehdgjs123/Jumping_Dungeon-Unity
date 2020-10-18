using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBolt : MonoBehaviour
{
    Rigidbody2D rb;
    public float xSpeed; //x축의 속도
    public float ySpeed; //y축의 속도

    private void OnEnable()
    {
        transform.position = transform.parent.position;
        StartCoroutine(reload());
    }
    void Start()
    {       
        rb = GetComponent<Rigidbody2D>();      
    }

    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(1.4f);
        gameObject.SetActive(false);
        transform.position = transform.parent.position;

    }

}

