using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 조건을 검사하는 클래스
public class UserNameCheck : MonoBehaviour
{
    public Text text; // 검사할 텍스트

    // 텍스트에 입력한 문자열이 올바른지 체크를 하는 함수
    public bool TextCheck()
    {
        // 텍스트 검사하는 작업, 경고 UI를 띄우는 작업 필요
        // 이름을 저장하는 작업 필요

        if (text == null) // 검사할 텍스트가 없으면
            return true; 
        
        else if (text.text.ToString().Length > 0) // 텍스트가 최소 글자수를 만족하는지 검사
        {
            DataManager.instance.Initialize(text.text.ToString()); // 등록한 유저명으로 파일명을 지정
            return true;            
        }

        return false;
    }
}
