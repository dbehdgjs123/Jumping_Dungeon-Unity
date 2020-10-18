using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowHandBoltLuncher : MonoBehaviour
{

    //손에서 나가는 반원패턴
    private int BboltAmount = 5; //총알의 양
    [SerializeField] private float startAngle = 90f; //시작 각도
    [SerializeField] private float endAngle = 270f; //끝 각도
    private Vector2 boltMoveDirection; //단위벡터를 위해 선언

    private GameObject[] attackBpool;
    public Transform attackBTr;
    public GameObject attackBbolt; //패턴b 총알

    private void Awake()
    {
        attackBpool = new GameObject[36];
        if (attackBbolt != null)
        {
            for (int i = 0; i < 36; i++)
            {
                attackBpool[i] = Instantiate(attackBbolt, attackBTr.position, Quaternion.identity);
                attackBpool[i].SetActive(false);

            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine("patternB");
    }


    IEnumerator patternB()
    {
        int attackCount = 0;
        int patternCount = 2;
        while (attackCount < patternCount)
        {
            //ani.SetBool("isAttack1", false);
            int pcount = 0; //오브젝트풀링을 위해 선언 i값을 증가시킴
            int amount = 5; //오브젝트풀링을위해 총알의 배열을 바꾸기 위해 선언
            int count = 0;   //6번씩 쏘도록 선언  
            float angleStep = (endAngle - startAngle) / BboltAmount; //균등하게 나가도록 하기 위해 선언
            float angle = startAngle; //각도
            yield return new WaitForSeconds(2f);
            //ani.SetBool("isAttack1", true);
            yield return new WaitForSeconds(1f);
            while (count < 6)
            {
                SoundManager.instance.PlaySE("Iattack2");
                for (int i = pcount; i < amount + 1; i++)
                {
                    float dirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                    float dirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
                    Vector3 boltMoveVec = new Vector3(dirX, dirY, 0f);
                    Vector2 dirvec = (boltMoveVec - transform.position).normalized;
                    attackBpool[i].transform.position = attackBTr.position;
                    attackBpool[i].transform.rotation = transform.rotation;
                    attackBpool[i].GetComponent<BoltHellBolt>().SetMoveDirection(dirvec);
                    attackBpool[i].SetActive(true);

                    angle += angleStep;
                }
                pcount += 6;
                amount += 6;
                angleStep = (endAngle - startAngle) / BboltAmount;
                angle = startAngle;
                count++;
                yield return new WaitForSeconds(0.5f);
            }
            attackCount++;
            yield return new WaitForSeconds(0.3f);
            if (attackCount >= patternCount)
            {
                //ani.SetBool("isAttack1", false);
            }


        }
    }
}
