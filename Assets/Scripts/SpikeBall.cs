using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{

    public GameObject expPre; //폭발 효과프리팹
    private GameObject exp; //폭발 효과를 주기 위해 선언
    private Transform expTr;

    private BoxCollider2D box;
    private void Awake()
    {
        expTr = transform.GetChild(0).gameObject.transform;
        exp = Instantiate(expPre, transform.position, Quaternion.identity);
        box = GetComponent<BoxCollider2D>();
    }


    void OnEnable()
    {
        box.enabled = true;
        if (exp.active == true)
        {
            exp.SetActive(false);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sword")
        {
            box.enabled = false;
            SoundManager.instance.PlaySE("Broken1");
            exp.transform.position = expTr.position;
            exp.SetActive(true);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
