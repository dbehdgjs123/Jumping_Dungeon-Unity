using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]private float speed = 75f; //회전 스피드
    private Transform tr;
    private Vector2 endScale;


    


    private void Awake()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        tr.Rotate(Vector3.forward, speed * Time.deltaTime *TimeManager.brt);
    }
}
