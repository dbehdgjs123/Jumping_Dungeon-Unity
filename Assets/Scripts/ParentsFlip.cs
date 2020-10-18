using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsFlip : MonoBehaviour
{
    private PlayerController pc;
    private SpriteRenderer pf; //부모의 filp을 사용하기 위해 선언
    private SpriteRenderer sf; //아이템의 플립을 사용하기 위해 선언
    // Start is called before the first frame update
    void Awake()
    {
        pf = transform.parent.gameObject.GetComponent<SpriteRenderer>();
        sf = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (pf.flipX == true)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
