using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    private bool isCheck = false;
    public Image checkImage;


    public void OnClick()
    {
        Debug.Log("들어옴" + !isCheck);
        isCheck = !isCheck;
        if (isCheck)
            checkImage.enabled = true;
        else
            checkImage.enabled = false;
    }

    // 튜토리얼 표시 유무를 체크
    public void TutorialCheck()
    {
        isCheck = !isCheck;
        if (isCheck)
        {
            checkImage.enabled = true;
            PlayerPrefs.SetInt("seeTutorial", 1); // 설정을 저장
        }
        else
        {
            checkImage.enabled = false;
            PlayerPrefs.SetInt("seeTutorial", 0);
        }
    }
}
