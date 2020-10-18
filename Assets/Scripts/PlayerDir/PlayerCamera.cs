using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    Transform tr;
    private Transform target;
    private GameObject playerTr;
    PlatformSpawner ps = new PlatformSpawner();
    float maxY = 0;
    Touch touch;

    public static Camera camera;

    private float duration = 4f; 
    private float smoothness = 0.05f; 

    public static float shakeTimer; //흔들릴 시간
    public static float shakeAmount; //흔들리는 양
    public static float bossMaxY = 0; //보스전때 플레이어의 위치

    public static bool isBossAppear = false; // 보스가 출현했는지 확인 

    private Color32 nColor = new Color32(59, 118, 174, 255);
    private Color32 fColor = new Color32(189, 71, 46, 255);
    private Color32 iColor = new Color32(176, 195, 232, 255);
    private Color32 sColor = new Color32(57, 28, 69, 255);



    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.Find("PlayerTr");
        target = playerTr.transform.GetChild(PlayerPrefs.GetInt("Character", 0)).transform;
        Debug.Log(target.name);
        tr = GetComponent<Transform>();
        camera = GetComponent<Camera>();
        offset = transform.position - target.transform.position;
        camera.orthographicSize = 3.75f;
        camera.backgroundColor = nColor;
        
            
    }

    private void LateUpdate()
    {
        if (GameManager.instance.isGameOver == false)
        {
            followCamera();
        }
       
    }

    void followCamera()
    {
        if (target.transform.position.y > maxY)
        {
            maxY = target.transform.position.y;
            float height = maxY * (1 / maxY);


            //GameManager.instance.Addscore(height * 0.1f); //점수 추가
            GameManager.instance.Addscore(maxY); //점수 추가

        }
        if (isBossAppear == false)
        {
            tr.position = new Vector3(0f, maxY + bossMaxY, tr.position.z);
        }
        else if(isBossAppear == true)
        {          
            if (shakeTimer >= 0)
            {
                
                Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
                tr.position = new Vector3(transform.position.x + ShakePos.x, maxY + bossMaxY, tr.position.z);
                shakeTimer -= Time.deltaTime;
            }

            if (shakeTimer <= 0)
            {
                isBossAppear = false;
            }

        }

        

    }
        
   
    void shakeCamera()
    {
        if (shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;

            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    IEnumerator FlerpColor() //화염보스용 하늘색
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.

        while (progress < 1)
        {
            camera.backgroundColor = Color32.Lerp(camera.backgroundColor, fColor, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }

    IEnumerator IlerpColor() //얼음보스용 하늘색
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.

        while (progress < 1)
        {
            camera.backgroundColor = Color32.Lerp(camera.backgroundColor, iColor, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }

    IEnumerator SlerpColor() //해골보스용 하늘색
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.

        while (progress < 1)
        {
            camera.backgroundColor = Color32.Lerp(camera.backgroundColor, sColor, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }

    IEnumerator LerpColorback() //원래 하늘색
    {
        float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
        float increment = smoothness / duration; //The amount of change to apply.

        while (progress < 1)
        {
            camera.backgroundColor = Color32.Lerp(camera.backgroundColor, nColor, progress);
            progress += increment;
            yield return new WaitForSeconds(smoothness);
        }
    }
    public void Fsky()
    {
        StartCoroutine("FlerpColor");
    }

    public void Isky()
    {
        StartCoroutine("IlerpColor");
    }

    public void Ssky()
    {
        StartCoroutine("SlerpColor");
    }

    public void Nsky()
    {
        StartCoroutine("LerpColorback");
    }



}
