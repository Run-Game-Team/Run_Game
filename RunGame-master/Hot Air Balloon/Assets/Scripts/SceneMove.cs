using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public int sceneNum;
    public bool hasCheckCondition = true; // 체크 해야할 조건을 가지고 있는지
    public GameObject tutorialView;    

    public void NextScene()
    {
        if (hasCheckCondition) // 체크 해야할 조건이 있으면
        {
            if (GetComponent<UserNameCheck>().TextCheck())
            {
                if (PlayerPrefs.GetInt("seeTutorial") == 1) // 튜토리얼을 더 이상 표시되지 않도록 체크 해놨다면
                    SceneManager.LoadScene(sceneNum); // 씬을 넘긴다
                else // 튜토리얼을 보지 않은 경우 입력창을 닫고 튜토리얼 창을 띄운다
                {
                    gameObject.GetComponentInParent<Canvas>().enabled = false;
                    tutorialView.SetActive(true);
                }
            }
        }
        else
            SceneManager.LoadScene(sceneNum);

        // 씬 이동 전에 걸어둔 일시정지 상태를 해제
        Time.timeScale = 1;
    }
}
