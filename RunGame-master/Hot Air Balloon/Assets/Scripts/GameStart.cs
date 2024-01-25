using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Image checkImage; // 체크리스트
    public GameObject nameInput;

    public void OnClick()
    { 
        if(checkImage.enabled) // 체크리스트가 활성화되어 있으면 게임 시작 가능
        {
            nameInput.SetActive(true);
        }
        else
        {
            return;
        }
    }
}
