using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetActive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !Player.instance.isDead)
        {
            // 플레이어의 자기장을 활성화
            SoundManager.instance.PlayOnce(SoundManager.instance.getItem);
            Player.instance.GetComponentInChildren<Magnet>().StartCoroutine("MagnetFieldActive");
            gameObject.SetActive(false);
        }
    }

    

}
