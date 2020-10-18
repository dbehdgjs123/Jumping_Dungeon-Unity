using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltHellBolt : MonoBehaviour
{
    private Rigidbody2D rb;

    private Transform tr;
    private Vector2 moveDirection;
    private float speed;

    public static float rt = 1f;
    public static float art = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 4f;
        
    }
    private void OnEnable()
    {
        StartCoroutine("reload");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(moveDirection * speed * Time.deltaTime); //정해진 위치로 날아감
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y)  * speed  * rt;
    }

    public void SetMoveDirection( Vector2 dir)
    {
        moveDirection = dir;
        float angle = Mathf.Atan2(moveDirection.x,-moveDirection.y); //방향벡터를 구해서 그 방향의 각도를 구함
        transform.localEulerAngles = new Vector3(0, 0, (angle * 180) / Mathf.PI);
    }
    
    IEnumerator reload()
    {


        yield return new WaitForSeconds(4.0f + art);
        gameObject.SetActive(false);
        

    }
}
