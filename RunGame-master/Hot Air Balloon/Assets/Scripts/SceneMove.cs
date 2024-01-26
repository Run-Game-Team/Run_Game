using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using System; //
using System.Security.Cryptography; //
using UnityEngine.UI;
using System.Runtime.CompilerServices; //

public class SceneMove : MonoBehaviour
{
    public int sceneNum;
    public bool hasCheckCondition = true; // 체크 해야할 조건을 가지고 있는지
    public GameObject tutorialView;

    public void NextScene()
    {
        StartCoroutine(NextSceneCoroutine());
    }

    public IEnumerator NextSceneCoroutine()
    {
        if (hasCheckCondition) // 체크 해야할 조건이 있으면
        {
            if (GetComponent<UserNameCheck>().TextCheck())
            {
                // start 버튼 누를 때

                yield return StartCoroutine(UserCheck.CheckSameName());

                // 코루틴이 끝난 후
                if (DataManager.instance.successLogin)
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
        }
        else
            SceneManager.LoadScene(sceneNum);
        // 씬 이동 전에 걸어둔 일시정지 상태를 해제
        Time.timeScale = 1;
    }

    public void CreateUser()
    {
        if (GetComponent<UserNameCheck>().newNameTextCheck())
            StartCoroutine(UserCheck.CreateUser());
    }
}