using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyWave : MonoBehaviour
{
    BoxCollider2D box;

    public static float art = 1f;
    public string sound;

    public static Animator ani;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        box.enabled = false;
        SoundManager.instance.PlaySE(sound);
        Invoke("EnergyOn", 0.7f * art);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void EnergyOn()
    {
        box.enabled = true;
        Invoke("EnergyOff", 0.4f * art);
    }
    void EnergyOff()
    {
        box.enabled = false;
    }
}
