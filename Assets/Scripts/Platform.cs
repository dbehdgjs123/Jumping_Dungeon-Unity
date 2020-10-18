using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    public Transform[] Spiketr; //스파이크 위치
    public Transform[] Springtr; //스프링 위치
    public GameObject[] spring;
    public GameObject[] spike;
    public GameObject[] movingSpike;
    public GameObject[] Monstertr;
    public GameObject[] monster;
    public GameObject[] chest;
    public GameObject[] movingMonster;
    public GameObject[] coin;

    private GameObject Fboss;
    private GameObject iBoss;
    private GameObject sBoss;

    private void Awake()
    {
        Fboss = GameObject.Find("BossTr").transform.Find("Boss_firetree").gameObject;
        iBoss = GameObject.Find("BossTr").transform.Find("Boss_snowMan").gameObject;
        sBoss = GameObject.Find("BossTr").transform.Find("Boss_skul").gameObject;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        for(int i = 0; i<5; i++)
        {
            spike[i].SetActive(false);

            
        }
        for (int i = 0; i < 2; i++)
        {
            movingSpike[i].SetActive(false);
        }


            for (int i = 0; i < 3; i++)
        {
            spring[i].SetActive(false);
            


        }
        for (int i = 0; i < 4; i++)
        {
            monster[i].SetActive(false);



        }
        for (int i = 0; i < 3; i++)
        {
            chest[i].SetActive(false);



        }
        for (int i = 0; i < 2; i++)
        {
            movingMonster[i].SetActive(false);

        }
        for (int i = 0; i < 5; i++)
        {
            coin[i].SetActive(false);

        }

        if (Fboss.active == false && iBoss.active == false && sBoss.active == false)
        {
            SpikeOn();
            MovingSpikeOn();
            MonsterOn();
            MovingMonsterOn();
        }
        ChestOn();
        CoinOn();
        SpringOn();


        /* for (int i = 0; i < 5; i++)
         {
             if (Random.Range(0, 3) == 0)
             {
                 spike[i].SetActive(true);
                 break;
             }

         }

         for (int i = 0; i < 3; i++)
         {
             if (Random.Range(0, 40) == 0 )
             {

                 spring[i].SetActive(true);


                 break;
             }

         }
         for (int i = 0; i < 2; i++)
         {
             if (Random.Range(0, 10) == 0)
             {

                 monster[i].SetActive(true);
                 break;



             }
         }*/


    }
    private void Update()
    {
        if (monster[0].active == true && spring[0].active == true) //동시에 있을 수 없도록
        {
            if (Random.Range(0, 2) == 0)
            {
                monster[0].SetActive(false);
            }
            else
            {
                spring[0].SetActive(false);
            }

        }

        if (monster[1].active == true && spring[2].active == true)
        {
            if (Random.Range(0, 2) == 0)
            {
                monster[1].SetActive(false);
            }
            else
            {
                spring[2].SetActive(false);
            }

        }
    }

    void SpikeOn()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Random.Range(0, 10) == 0)
            {
                spike[i].SetActive(true);
                //break;
            }

        }
    }

    void MovingSpikeOn()
    {
        if (Mathf.Floor(GameManager.instance.Nowscore) <= 400f && Random.Range(0, 15) == 0)
        {
            movingSpike[0].SetActive(true);
        }
        else if (Mathf.Floor(GameManager.instance.Nowscore) > 400f)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Random.Range(0, 20) == 0)
                {
                    movingSpike[i].SetActive(true);
                    break;
                }

            }
        }
    }
    void CoinOn()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Random.Range(0, 8) == 0)
            {
                coin[i].SetActive(true);
                //break;
            }

        }
    }
    void SpringOn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 150) == 0)
            {
                spring[i].SetActive(true);
                break;
            }

        }
    }
    void MonsterOn()
    {
        if (Mathf.Floor(GameManager.instance.Nowscore) <= 550f)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Random.Range(0, 20) == 0)
                {

                    monster[i].SetActive(true);
                    break;



                }
            }
        }
        else if (Mathf.Floor(GameManager.instance.Nowscore) > 550f)
        {


            for (int i = 0; i < 4; i++)
            {
                if (Random.Range(0, 30) == 0)
                {

                    monster[i].SetActive(true);
                    break;



                }
            }
        }
    }
    void MovingMonsterOn()
    {
        if (Mathf.Floor(GameManager.instance.Nowscore) <= 200f && Random.Range(0, 30) == 0)
        {
            movingMonster[1].SetActive(true);
        }
        else if (Mathf.Floor(GameManager.instance.Nowscore) > 200f)
        {
            for (int i = 0; i < 2; i++)
            {
                if (Random.Range(0, 30) == 0)
                {

                    movingMonster[i].SetActive(true);
                    break;



                }
            }
        }
    }
    void ChestOn()
    {
        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 35) == 0 && Fboss.active == false && iBoss.active == false && sBoss.active == false)
            {

                chest[i].SetActive(true);
                break;



            }
            else if(Fboss.active == true || iBoss.active == true || sBoss.active == true)
            {
                if (Random.Range(0, 8) == 0)
                {
                    chest[i].SetActive(true);
                    break;
                }
            }
        }
    }
}
