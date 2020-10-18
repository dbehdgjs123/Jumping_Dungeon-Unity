using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_bolt : MonoBehaviour
{
    Rigidbody2D rb;
    public float xSpeed;
    public float ySpeed = -1.5f;
    public static float rt = 1; //시간 배속을 설정(슬로우기능)

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void OnEnable()
    {
       
        Invoke("reload", 3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed) * rt;
    }

    void reload()
    {
        gameObject.SetActive(false);
    }

}
