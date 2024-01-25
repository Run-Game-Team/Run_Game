using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private float width;
    private float blank = 0.04f; // 스프라이트 사이의 공백 처리를 위함

    void Start()
    {
        Renderer backgroundRenderer = GetComponent<Renderer>();

        width = backgroundRenderer.bounds.size.x;
    }

    void Update()
    {
        if(transform.position.x <= -width)
        {
            Reposition();
        }
    }

    void Reposition()
    {
        // 현재 위치에서 (오른쪽으로 가로 길이 - 공백) *2 만큼 이동
        Vector2 offset = new Vector2((width - blank) * 2f, 0); // 스프라이트를 공백만큼 붙여놓기 위해 공백을 뺀 거리에서 2를 곱한다
        transform.position = (Vector2)transform.position + offset;
    }
}
