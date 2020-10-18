using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerBolt : MonoBehaviour
{
    private Transform tr;

    public float xSpeed;
    public float ySpeed;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //tr.position = new Vector2(tr.position.x + xSpeed, tr.position.y + ySpeed) * Time.deltaTime;
    }
}
