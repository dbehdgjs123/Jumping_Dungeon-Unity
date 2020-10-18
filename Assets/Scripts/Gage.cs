using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gage : MonoBehaviour
{
    private GameObject Hud; //head up display (플레이어 머리 위)
    public GameObject sword;

    private Transform tr;

    private void OnEnable()
    {
        Hud = GameObject.Find("SwordGage");
        StartCoroutine(Disabeld());
    }

    void Start()
    {
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        tr.position = Hud.transform.position; 
        if(sword.active == false)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Disabeld()
    {
        while(gameObject.active == true)
        {
            yield return new WaitForSeconds(40f);
            gameObject.SetActive(false);
        }
    }
}
