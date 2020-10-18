using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeasureChest : MonoBehaviour
{
    private Animator ani;
    private int percent; //아이템의 확률계산(빈도수)을 위해 사용
    private bool isOpen = false;

    public GameObject[] Item;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        isOpen = false;
        percent = Random.Range(0, 100); //100중에 하나를 반환
        if (ani != null)
        {
            ani.SetBool("IsOpen", false);
        }
        for(int i = 0; i <7; i++)
        {
            Item[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isOpen == false)
        {
            SoundManager.instance.PlaySE("ChestBox");
            ani.SetBool("IsOpen", true);
            //box.isTrigger = true; //플레이어가 한 번 닿으면 보물상자가 열리면서 트루가 됨.
            if (percent < 30)
            {
                if (Random.Range(0, 2) == 0)
                {
                    Item[0].SetActive(true);
                    
                }
                else
                {
                    Item[1].SetActive(true);
                    
                }

            }
            else if (percent < 45)
            {
                Item[2].SetActive(true);
                
            }
            else if (percent < 60)
            {
                Item[3].SetActive(true);
                
            }
            else if (percent < 75)
            {
                Item[4].SetActive(true);
                
            }
            else if (percent < 90)
            {
                Item[5].SetActive(true);
                
            }
            else if (percent < 100)
            {
                Item[6].SetActive(true);
                
            }

            isOpen = true;
        }
    }
}
