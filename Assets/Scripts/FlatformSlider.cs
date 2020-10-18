using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatformSlider : MonoBehaviour
{

    private float Speed = 0f;
    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
        
        
    }
}
