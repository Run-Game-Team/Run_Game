using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 곡선 이동을 하는 까마귀의 움직임을 처리하는 스크립트
public class CrowMove : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    public float journeyTime = 1.0f; // 포물선까지 이동하는데 걸리는 시간
    public float distanceToEndParabolic; // 포물선까지의 거리

    private int yAxisOfParabolic = 3; // 포물선의 y축

    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
        startPos = new GameObject("startPos").transform;
        endPos = new GameObject("endPos").transform;
    }
    
    // 다시 활성화 될때마다 시작점과 끝점을 초기화
    private void OnEnable()
    {
        startPos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        endPos.position = new Vector3(startPos.position.x , startPos.position.y, startPos.position.z);
    }

    void Update()
    {
        if (startPos.position != endPos.position)
        {
            // 포물선 이동
            Vector3 center = (endPos.position + startPos.position) * 0.5f;
            center -= new Vector3(0, yAxisOfParabolic, 0);
            Vector3 startRelCenter = startPos.position - center;
            Vector3 endRelCenter = endPos.position - center;
            float fracComplte = (Time.time - startTime) / journeyTime;
            transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplte);
            transform.position += center;
        }
        
        if (transform.position == endPos.position)
        {
            // 포물선의 시작점, 끝점 변경
            startTime = Time.time;
            startPos.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            endPos.position = new Vector3(transform.position.x - distanceToEndParabolic, transform.position.y, transform.position.z);
            yAxisOfParabolic *= -1; // y축 상하 반전                 
        }
    }


}
