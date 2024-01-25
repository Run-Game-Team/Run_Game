using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image magnet;
    public Slider hpBar;
    public Slider ExpBar;
    public Text level;

    public GameObject ItemPos; // 머리위에 획득한 아이템의 아이콘을 띄우는 위치

    private ColorBlock cb; // ColorBlock은 UI의 color를 관리

    public bool isCoroutineStarted = false; // 코루틴을 한번만 실행하기 위한 bool 변수


    private void Start()
    {
        cb = hpBar.colors;
        LevelUpdate();
    }

    void Update()
    {
        hpBar.value = (float)Player.instance.currentHP / (float)Player.instance.maxHP;
        ExpBar.value = (float)PlayerMove.instance.curDistance / (float)PlayerMove.instance.targetDistance;

        // 최대 높이에 도달하면 높이 고정
        if (transform.position.y >= Constant.maxHeight)
            transform.position = new Vector2(transform.position.x, Constant.maxHeight);
        else
            magnet.transform.position = new Vector3(transform.position.x, ItemPos.transform.position.y);

        // 체력이 0이 되면 코루틴 중지
        if (hpBar.value == 0)
        {
            StopCoroutine("AlphaBlink");
            cb.normalColor = new Color32(255, 255, 255, 255); // UI를 원래 색으로 되돌림
            hpBar.colors = cb;
        }

        // 체력이 낮을 때 체력바의 알파값이 깜빡임
        else if (hpBar.value < 0.3)
        {
            if (!isCoroutineStarted) // bool변수가 false일 때 한번만 실행
            {
                StartCoroutine("AlphaBlink");
                hpBar.colors = cb;
            }
        }
        else if (hpBar.value > 0.3) // 체력이 30% 이상인데
        {
            if (isCoroutineStarted) // 체력바가 깜빡거리고 있으면 체력바를 원복
            {
                isCoroutineStarted = false;
                StopCoroutine("AlphaBlink");
                cb.normalColor = new Color32(255, 255, 255, 255); // UI를 원래 색으로 되돌림
                hpBar.colors = cb;
            }
        }
    }
        
    // 플레이어의 레벨을 얻어옴
    public void LevelUpdate()
    {
        level.text = Player.instance.level.ToString();
    }

    // 알파값 깜빡임 처리(빨간색)
    IEnumerator AlphaBlink()
    {
        isCoroutineStarted = true;

        int coolTime = 0;
        while (true)
        {
            if (coolTime % 2 == 0)
            {
                cb.normalColor = new Color32(255, 0, 0, 50);
                hpBar.colors = cb;
            }
            else
            {
                cb.normalColor = new Color32(255, 0, 0, 255);
                hpBar.colors = cb;
            }

            yield return new WaitForSeconds(0.25f);
            coolTime++;
        }
    }

    IEnumerator MagnetOn()
    {
        float magnetDuration = transform.GetComponentInChildren<Magnet>().magneticDuration;
        magnet.enabled = true;        
        // 자석의 효과 종료 3초전부터 깜빡거림
        yield return new WaitForSeconds(magnetDuration - 3f);
        int coolTime = 0;
        while (coolTime < 12)
        {
            if (coolTime % 2 == 0)
                magnet.color = new Color32(255, 255, 255, 50);
            else
                magnet.color = new Color32(255, 255, 255, 255);

            yield return new WaitForSeconds(0.25f);
            coolTime++;
        }
        magnet.enabled = false;
    }
}
