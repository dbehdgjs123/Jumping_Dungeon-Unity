using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBoundary : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 view = Camera.main.WorldToViewportPoint(transform.position);//월드 좌표를 뷰포트 좌표로 변형한다.

        if (view.y < -0.2)

        {

            gameObject.SetActive(false);//뷰포트 카메라에서 -0.5보다 더 멀어지면 일정간 거리가 유지되도록 풀링.
            gameObject.transform.position += new Vector3(0, 22.75f, 0);
            

            gameObject.SetActive(true); 



        }
    }
}
