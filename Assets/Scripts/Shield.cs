using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update
    public float seconds; //활성 시간
    public float superSeconds; //몇 초부터 깜빡일지
    int count = 0;
    SpriteRenderer shieldColor;
    private void OnEnable()
    {
        StartCoroutine(Disabled(6.0f));
        StartCoroutine(SuperAnimation());
    }
    void Start()
    {
        shieldColor = gameObject.GetComponent<SpriteRenderer>();
    }


    IEnumerator Disabled(float waitTime) //무적시간
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    IEnumerator SuperAnimation() //깜빡임 애니메이션
    {
        count = 0;


        while (count < 12)
        {
            count++;
            yield return new WaitForSeconds(0.5f);
            if (count > 6)
            {
                shieldColor.color = new Color32(255, 255, 255, 130);
                yield return new WaitForSeconds(0.2f);
                shieldColor.color = new Color32(255, 255, 255, 255);
                yield return new WaitForSeconds(0.2f);
            }

            if (gameObject.active == false)
            {
                count = 0;
            }
        }

    }


}
