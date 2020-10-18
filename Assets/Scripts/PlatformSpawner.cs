using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

        public Transform target;

    private Vector3 offset1;
    private Vector3 followtr;
    private Vector3 yPos;

    public GameObject platformPrefab;
    public GameObject[] platforms;
    public int count = 10;






    private Vector2 poolPositioin = new Vector2(0,9.75f);
    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[count];
        for(int i = 0; i<count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPositioin, Quaternion.identity);
            poolPositioin.y += 3.25f;
        }

        followtr = new Vector3(0, 3.25f, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

}
