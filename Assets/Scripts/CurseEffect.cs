using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseEffect : MonoBehaviour
{
    private Transform tr;
    private Vector2 endScale;
    void Awake()
    {
        tr = GetComponent<Transform>();
        endScale = new Vector2(1, 1);
    }

    private void OnEnable()
    {

        if (tr != null)
        {
            tr.localScale = new Vector2(6, 6);
        }
        StartCoroutine(Effect());

    }
    IEnumerator Effect()
    {
        while (Vector2.Distance(tr.localScale, endScale) > 0)
        {
            tr.localScale = Vector2.MoveTowards(tr.localScale, endScale, 0.5f); //점점 커지는 효과
            yield return new WaitForSeconds(0.1f);
        }
    }
}
