using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static float rt = 1f;
    public static float brt = 1f; //bolt의 시간을 따로 설정
    public static float art = 1f;
    private void OnEnable()
    {
        rt = 1f;
        art = 1f;
        brt = 1f;
    }
}
