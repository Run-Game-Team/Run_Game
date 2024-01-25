using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재 UI를 부모를 포함해 전부 비활성화 시키고 다른 UI를 활성화
public class ActivateOtherUI : MonoBehaviour
{
    public GameObject otherUI;

    public void OnClick()
    {
        gameObject.GetComponentInParent<Canvas>().enabled = false;
        otherUI.SetActive(true);
    }
}
