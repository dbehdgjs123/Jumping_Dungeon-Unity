using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{

    public float seconds; //활성 시간
    public float superSeconds; //몇 초부터 깜빡일지
    public byte r = 255;
    public byte g = 255;
    public byte b = 255;
    SpriteRenderer Color;
    private void OnEnable()
    {
        if(Color != null)
        {
            Color.color = new Color32(r, g, b, 255);
        }
        
        StartCoroutine(Disabled(seconds));
        StartCoroutine(SuperAnimation());
    }
    void Start()
    {
        Color = gameObject.GetComponent<SpriteRenderer>();
    }


    IEnumerator Disabled(float waitTime) //무적시간
    {
        yield return new WaitForSeconds(waitTime);
        if(gameObject.tag == "Sword")
        {
            CircleCollider2D cir = gameObject.GetComponent<CircleCollider2D>();
            cir.enabled = false;            
        }
        gameObject.SetActive(false);
    }

    IEnumerator SuperAnimation() //깜빡임 애니메이션
    {
        
        yield return new WaitForSeconds(superSeconds);
        while (gameObject.active == true)
        {          
            Color.color = new Color32(r, g, b, 130);
            yield return new WaitForSeconds(0.2f);
            Color.color = new Color32(r, g, b, 255);
            yield return new WaitForSeconds(0.2f);

        }
    }

}
