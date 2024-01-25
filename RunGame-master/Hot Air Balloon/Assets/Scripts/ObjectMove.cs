using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    public float speed = 5f;
    public float afterTimeDisable = 6f; 

    private void OnEnable()
    {
        StartCoroutine(Disabled(afterTimeDisable));
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    // 화면을 벗어나는 오브젝트들의 SetActive 상태를 변경하기 위함
    IEnumerator Disabled(float waitTime)
    {        
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }
}
