using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryBolt : MonoBehaviour
{

    private void OnEnable()
    {
        Invoke("reload", 2f);
    }
    void reload()
    {
        gameObject.SetActive(false);
    }
}
