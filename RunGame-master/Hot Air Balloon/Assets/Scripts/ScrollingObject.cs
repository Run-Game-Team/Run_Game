using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float speed = 5f;
    public float tempSpeed = 0;
    private readonly float backupOfSpeed; // 속도를 원복하는데 사용

    private float slowRate = 0;

    public bool isSlow = false;
    
    ScrollingObject()
    {
        backupOfSpeed = speed;
    }

    private void Start()
    {
        tempSpeed = speed;
    }

    void Update()
    {        
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if(isSlow) // 슬로우 효과가 적용되고 있는 동안
            speed = tempSpeed * slowRate;
    }

    public void SpeedDown(float rate)
    {
        isSlow = true;
        slowRate = rate;
    }    

    public void Stop()
    {        
        speed = 0;
        tempSpeed = speed;
    }

    public void Restart()
    {
        tempSpeed = backupOfSpeed;
        speed = backupOfSpeed;
    }
}
