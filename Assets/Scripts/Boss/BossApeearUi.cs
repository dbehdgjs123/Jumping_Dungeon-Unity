using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossApeearUi : MonoBehaviour
{
    public GameObject pause;
    public GameObject scoreText;
    public GameObject bscoreText;
    public GameObject coin;
    public GameObject coinIcon;
    public GameObject coinText;
    public GameObject waveText;
    public GameObject waveSeconds;

    private Image[] image;
    private Text[] spr;

    private void Awake()
    {
        image = new Image[3];

        image[0] = pause.GetComponent<Image>();
        image[1] = coin.GetComponent<Image>();
        image[2] = coinIcon.GetComponent<Image>();
        spr = new Text[5];
        spr[0] = scoreText.GetComponent<Text>();     
        spr[1] = bscoreText.GetComponent<Text>();
        spr[2] = coinText.GetComponent<Text>();
        spr[3] = waveText.GetComponent<Text>();
        spr[4] = waveSeconds.GetComponent<Text>();



    }
    private void OnEnable()
    {
        StartCoroutine("bossAppearUi");
    }

    IEnumerator bossAppearUi()
    {
        while (spr[0].color.a > 0.2f && spr[1].color.a > 0.2f && spr[2].color.a > 0.1f && spr[3].color.a > 0.1f && spr[4].color.a > 0.1f && image[0].color.a > 0.1f && image[1].color.a > 0.1f && image[2].color.a > 0.1f)
        {
            for (int i = 0; i < 3; i++)
            {
                image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, image[i].color.a - 0.05f);
            }
            for (int i = 0; i < 5; i++)
            {
                spr[i].color = new Color(spr[i].color.r, spr[i].color.g, spr[i].color.b, spr[i].color.a - 0.05f);
                
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator bossDieUi()
    {
        while (spr[0].color.a < 1f && spr[1].color.a < 1f && spr[2].color.a < 1f && spr[3].color.a < 1f && spr[4].color.a < 1f && image[0].color.a < 1f && image[1].color.a < 1f && image[2].color.a < 1f)
        {
            for (int i = 0; i < 3; i++)
            {
                image[i].color = new Color(image[i].color.r, image[i].color.g, image[i].color.b, image[i].color.a + 0.05f);
            }
            for (int i = 0; i < 5; i++)
            {
                spr[i].color = new Color(spr[i].color.r, spr[i].color.g, spr[i].color.b, spr[i].color.a + 0.05f);

            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Die()
    {
        StartCoroutine("bossDieUi");
    }

}
