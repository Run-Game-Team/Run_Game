using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 일시정지를 위한 클래스
public class PauseButton : MonoBehaviour
{
    public GameObject pause_Ui;

    public void Pause()
    {
        Time.timeScale = 0;
        GameManager.instance.isPause = true;
        pause_Ui.SetActive(true);
    }
    
    public void UnPause()
    {
        Time.timeScale = 1;
        GameManager.instance.isPause = false;
        pause_Ui.SetActive(false);
    }
}
