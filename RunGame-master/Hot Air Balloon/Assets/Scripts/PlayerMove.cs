using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance = null;

    public int levitationSpeed = 200;
    public float moveSpeed = 5; // 속도(초당 이동거리)
    public float tempSpeed; // 속도 임시 저장(속도 원복 용도)
    

    public float curDistance = 0;
    public float tempDistance = 0; // 거리 중간 저장
    public float targetDistance = 200; // 목표 이동 거리 = 경험치

    public Text curDistanceText;

    public ScrollingObject background; // 배경 속도는 캐릭터 속도에 비례함

    private bool isClick = false; // 클릭에 대한 물리 이동 처리를 FixedUpdate에서 고정된 프레임으로 처리하기 위한 플래그 변수   

    public Animator anim;
       
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        anim = GetComponent<Animator>();
        tempSpeed = moveSpeed;
    }

    void Update()
    {
        // 이동 높이 제한
        if (transform.position.y >= Constant.maxHeight)
            transform.position = new Vector2(transform.position.x, Constant.maxHeight);

        if (transform.position.y <= Constant.minHeight)
            transform.position = new Vector2(transform.position.x, Constant.minHeight);

        if (Player.instance.isDead) 
        {
            anim.SetBool("isDead", true);

            // 이미 땅과 충돌해 있는 상태로 죽으면 충돌 검사를 할 수 없기 때문에 따로 검사함
            if (anim.GetBool("isGround") && Player.instance.life > 0)
                Player.instance.Resurrection();
            else if(anim.GetBool("isGround") && Player.instance.life == 0) // 라이프가 없으면
                GameManager.instance.StartCoroutine("GameOver"); // 게임 결과창 띄움

            // 죽으면 키 입력을 받지 않음
            return;
        }

        // 키 입력이 불가능한 일시정지 상태
        if (GameManager.instance.isPause)
            return;

        // 거리 체크 표시
        if (anim.GetBool("isGround") == false) // 공중에 떠있을 경우에만 거리가 늘어나도록
        {
            curDistance += moveSpeed * Time.deltaTime;
            tempDistance = curDistance;
            curDistanceText.text = ((int)curDistance).ToString(); // 소수점은 표시하지 않음
            if (tempDistance >= targetDistance)
            {
                tempDistance = 0;
                targetDistance *= 2;
                Player.instance.LevelUp();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject()) // 클릭한 대상이 UI가 아닐 경우만 클릭 처리
            {
                anim.SetBool("isUp", true);
                anim.SetBool("isGround", false);                

                isClick = true;
            }
        }
        else
            anim.SetBool("isUp", false); // 하강                
    }

    // Rigidbody를 다루는 경우 FixedUpdate를 사용해야함(고정된 프레임마다 적용)
    private void FixedUpdate()
    {
       if(isClick)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            gameObject.GetComponent<Rigidbody>().AddForce(0, levitationSpeed, 0);
            isClick = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 땅에 착지시 게임 일시정지(키 입력으로 인해 해제 가능한 일시정지 상태)
        if (collision.transform.tag == "Ground")
        {
            if (Player.instance.isDead) // 죽어서 땅에 떨어지면
            {
                if(Player.instance.life > 0) // 라이프가 있으면
                    Player.instance.Resurrection(); // 부활
                else // 남아있는 라이프가 없으면
                    GameManager.instance.StartCoroutine("GameOver"); // 게임 결과창 띄움

                anim.SetBool("isDead", false);
            }
            anim.SetBool("isGround", true);
            background.Stop();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        background.Restart();
    }

    public IEnumerator SpeedDown(float rate, float time)
    {
        moveSpeed = moveSpeed * rate;
        background.SpeedDown(rate); // 배경 속도도 낮춘다

        // 일정시간 경과 후 속도 원복
        yield return new WaitForSeconds(time);
        moveSpeed = tempSpeed;
        // 캐릭터가 공중에 떠있을때만 배경의 속도 원복(땅에 있을때는 배경이 움직이지 않음)
        if (anim.GetBool("isGround") == false)
            background.Restart();            
        background.isSlow = false;
    }
}
