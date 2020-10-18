using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterController : MonoBehaviour
{
    // Start is called before the first frame update

    private float speed = 2.4f;
    public static float rt = 1f; //시간 배속을 설정(슬로우기능)
    private Transform target;
    private GameObject playerTr;
    public Text waveText; //웨이브를 표시 시켜주기 위해 선언
    public Text waveSecondsText;

    private int waveScore;
    private int waveSeconds;
    private bool isWaveStart = false;
    
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        playerTr = GameObject.Find("PlayerTr");
        target = playerTr.transform.GetChild(PlayerPrefs.GetInt("Character", 0)).gameObject.transform;
        //player = playerTr.transform.GetChild(charCurrent).gameObject;
        waveSecondsText.enabled = false;
        isWaveStart = false;
        //StartCoroutine("WaveTimer");
        Invoke("FirstWave", 2.5f);
    }

    void Update()
    {
        if (GameManager.instance.isGameOver == false && isWaveStart == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target.transform.position.y), speed * Time.deltaTime * TimeManager.brt);
        }
    }  
    IEnumerator WaveTimer()
    {
        isWaveStart = false;
        waveSecondsText.enabled = true;
        StartCoroutine("WaveSecondsTimer");
        yield return new WaitForSeconds(5f);
        if(target.transform.position.y - transform.position.y <= 30f)
        {
            SoundManager.instance.PlaySE("Ocean");
        }
        StartCoroutine("Wave");
    }
    IEnumerator Wave()
    {
        waveSecondsText.enabled = false;
        waveScore++;
        waveText.text = "WAVE : " + waveScore;
        if(speed < 4.4f)
        {
            speed += 0.1f;
        }
        else
        {
            speed = 4.4f;
        }
        Debug.Log(speed);
        
        isWaveStart = true;
        yield return new WaitForSeconds(10f);
        StartCoroutine("WaveTimer");

    }
    IEnumerator WaveSecondsTimer()
    {
        waveSeconds = 5;
        waveSecondsText.text = "NEXT WAVE: " + waveSeconds + "s";
        while (waveSecondsText.enabled == true)
        {
            yield return new WaitForSeconds(1f);
            if (waveSeconds > 0)
            {
                waveSeconds--;
            }
            waveSecondsText.text = "NEXT WAVE: " + waveSeconds + "s";
        }
    }
    private void FirstWave()
    {
        if (target.transform.position.y - transform.position.y <= 30f)
        {
            SoundManager.instance.PlaySE("Ocean");
        }
        StartCoroutine("Wave");
    }


}
