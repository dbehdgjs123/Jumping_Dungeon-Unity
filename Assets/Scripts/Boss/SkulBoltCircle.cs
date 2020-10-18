using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulBoltCircle : MonoBehaviour
{
    [SerializeField]private float speed = 75f; //회전 스피드
    private Transform tr;
    private Vector2 endScale;

    public Transform target;

    public static float rt = 1; //시간 배속을 설정(슬로우기능)

    private void Awake()
    {
        tr = GetComponent<Transform>();
        endScale = new Vector2(2.5f, 2.5f);
    }
    private void OnEnable()
    {
        if (tr != null)
        {
            tr.localScale = new Vector2(1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tr.Rotate(Vector3.forward, speed * Time.deltaTime);
        tr.transform.position = target.transform.position;
        tr.localScale = Vector2.MoveTowards(tr.localScale, endScale, 1f * Time.deltaTime); //점점 커지는 효과


    }
}

