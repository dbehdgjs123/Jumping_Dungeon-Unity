using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulWarninBolt : MonoBehaviour
{
    Rigidbody2D rb;
    public float xSpeed;
    public float ySpeed = -30f;
    public static float rt = 1; //시간 배속을 설정(슬로우기능)
    public GameObject warningPre; //경고선 프리팹

    private GameObject warning; //경고선

    private bool isShot = false;

    private void Awake()
    {

        warning = Instantiate(warningPre, transform.position, Quaternion.identity);
        isShot = false;
        warning.SetActive(false);
    }
    void Start()
    {
        isShot = false;
        warning.SetActive(false);
        rb = GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

            rb.velocity = new Vector2(xSpeed, ySpeed) * rt;
        
    }

    IEnumerator Shot()
    {
        warning.transform.position = transform.position;
        warning.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        warning.SetActive(false);
        isShot = true;
    }

}
