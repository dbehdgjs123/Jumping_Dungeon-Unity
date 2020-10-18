using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTr : MonoBehaviour
{
    private GameObject target;

    private Transform tr;

    void Start()
    {
        target = GameObject.Find("SwordTr");
        tr = GetComponent<Transform>();
    }
    void Update()
    {
        tr.position = target.transform.position;
    }
}
