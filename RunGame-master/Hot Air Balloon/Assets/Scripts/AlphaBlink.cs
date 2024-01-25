using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 알파값을 서서히 늘어났다가 서서히 줄어들기를 반복함
public class AlphaBlink : MonoBehaviour
{
    private int alpha = 0;
    private int sign = 2; // 알파값이 증가or감소
    private const int maxAlpha = 100;
    private const int minAlpha = 0;

    private SpriteRenderer mySprite;

    IEnumerator Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        while(true)
        {
            if (alpha == 0)
                sign = 2;
            else if (alpha == 100)
                sign = -2;

            alpha += sign;
            mySprite.color = new Color32(255, 0, 0, (byte)alpha);

            yield return new WaitForSeconds(0.015f);
        }
    }

    private void OnEnable()
    {
        StartCoroutine("Start");
    }
}
