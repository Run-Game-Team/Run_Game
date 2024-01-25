using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderMove : MonoBehaviour
{
    Enum.MoveDir moveDir;
    public float speed = 10f;
    public float afterTimeDisable = 6f;

    Vector3 upVec;
    Vector3 downVec;

    private void Awake()
    {
        upVec = new Vector3(0, 0.3f, 0);
        downVec = new Vector3(0, -0.3f, 0);
    }

    private void OnEnable()
    {
        StartCoroutine(Disabled(afterTimeDisable));
    }

    void Update()
    {
        if(moveDir == Enum.MoveDir.up)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.Translate(upVec * speed * Time.deltaTime);
        }
        else if(moveDir == Enum.MoveDir.down)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.Translate(downVec * speed * Time.deltaTime);
        }
        else if(moveDir == Enum.MoveDir.left)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    // 화면을 벗어나는 오브젝트들의 SetActive 상태를 변경하기 위함
    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
    }

    // 나아갈 방향 설정
    public void InitDir(Enum.MoveDir dir)
    {
        moveDir = dir;
    }
}
