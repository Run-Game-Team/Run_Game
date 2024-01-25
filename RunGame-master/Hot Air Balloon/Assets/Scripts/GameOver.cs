using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void ForceGameOver()
    {
        Time.timeScale = 1;
        Player.instance.isDead = true;
        GameManager.instance.StartCoroutine("GameOver");
    }
}
