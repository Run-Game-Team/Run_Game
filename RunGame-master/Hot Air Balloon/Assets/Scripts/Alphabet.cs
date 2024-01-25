using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet : MonoBehaviour
{
    public AlphabetUI alphabetUI;

    private void Start()
    {      
        alphabetUI = GameObject.Find("Alphabet").GetComponent<AlphabetUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !Player.instance.isDead)
        {
            SoundManager.instance.PlayOnce(SoundManager.instance.getItem);
            alphabetUI.CheckAlphabet(this); // 알파벳을 넘김
            gameObject.SetActive(false);
        }
    }
}
