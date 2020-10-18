using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCannonBolt : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;

    private void OnEnable()
    {      
        //Invoke("reload", 3f);
        transform.position = transform.parent.position;
        StartCoroutine(reload());
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, 0f) * TimeManager.brt;

    }

    IEnumerator reload()
    {

        yield return new WaitForSeconds(3.0f * TimeManager.art);
        gameObject.SetActive(false);
        transform.position = transform.parent.position;

    }
}
