using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    private Transform tr;
    private SpriteRenderer spr;
    private Vector2 endScale;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        spr = GetComponent<SpriteRenderer>();       
        endScale = new Vector2(4, 4);
        
        
    }
    private void OnEnable()
    {
        
       
        if (tr != null)
        {
            tr.localScale = new Vector2(1, 1);
        }
        if (spr != null)
        {
            spr.color = new Color32(255, 255, 255, 190);
        }
        Invoke("Clear", 1.5f);
        
    }

    void Update()
    {
        tr.localScale = Vector2.MoveTowards(tr.localScale, endScale, 2.5f * Time.deltaTime); //점점 커지는 효과
        spr.color = new Color(255, 255, 255, spr.color.a -0.5f * Time.deltaTime); //점점 사라지는 효과
    }
    void Clear()
    {
        gameObject.SetActive(false); //오브젝트 비활성
    }
}
