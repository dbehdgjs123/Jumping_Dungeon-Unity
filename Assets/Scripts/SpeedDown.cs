using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : MonoBehaviour
{
    public GameObject player;

    private SpriteRenderer spr;
    private Transform tr;
    // Update is called once per frame
    private void Awake()
    {
        tr = GetComponent<Transform>();
        spr = player.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
    }
}
