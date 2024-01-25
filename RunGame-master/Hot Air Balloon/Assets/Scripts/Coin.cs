using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinScore; // 코인별 점수 구분

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !Player.instance.isDead)
        {
            GameManager.instance.GetCoin(coinScore);
            SoundManager.instance.PlayOnce(SoundManager.instance.getCoin);
            gameObject.SetActive(false);
        }
    }
}
