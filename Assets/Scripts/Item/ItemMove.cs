using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private float speed = 0.25f;
    private Vector3 endPos;
    private Vector3 startPos;
    // Start is called before the first frame update
    private void OnEnable()
    {
        startPos = new Vector2(transform.position.x, transform.parent.position.y + 0.5f);
        endPos = new Vector2(transform.position.x, transform.parent.position.y + 0.6f);
        transform.position = startPos;
        StartCoroutine(move());
    }


    // Update is called once per frame

    IEnumerator move()
    {
        while(gameObject.active == true)
        {

            while (Vector2.Distance(transform.position, endPos) > 0) //거리가 0보다 클경우 실행
            {

                transform.position = Vector2.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame(); //이 프레임만 실행

            }            
            yield return new WaitForSeconds(0.1f);
            while (Vector2.Distance(transform.position, startPos) > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.1f);
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            gameObject.SetActive(false);
        }
    }
}
