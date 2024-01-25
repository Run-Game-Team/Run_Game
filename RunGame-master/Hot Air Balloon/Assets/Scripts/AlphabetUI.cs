using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetUI : MonoBehaviour
{
    public GameObject[] alphabet;
    public int alphaCnt;

    public void CheckAlphabet(Alphabet alpha)
    {
        for (int i = 0; i < alphabet.Length; i++)
        {
            if (alphabet[i].CompareTag(alpha.tag)) // 획득한 알파벳의 태그가 UI의 태그와 일치하면
            {
                if(alphabet[i].activeSelf) // 알파벳이 이미 활성화 되어 있으면 무시함
                {
                    break;
                }
                else // 알파벳을 활성화하고 카운트를 늘림
                {
                    alphabet[i].SetActive(true);
                    alphaCnt++;
                }
                
                if(alphaCnt == 7) // 알파벳을 다 모으면
                {                    
                    StartCoroutine("SpecialEffect"); // 무적, 부스터 효과를 받음
                }
            }
        }
    }

    // 플레이어를 무적으로 만들고 게임에 배속을 건다
    IEnumerator SpecialEffect()
    {
        int multiple = 3; // 배속
        Player.instance.isInvincibility = true;
        Time.timeScale = multiple;
        EffectManager.instance.StartCoroutine(EffectManager.instance.PlayForNSeconds(Enum.Particle.booster, transform.position, 10f * multiple));
        yield return new WaitForSeconds(10f * multiple);

        AllAlphabetDisable();
        Player.instance.StartCoroutine("AlphaBlink"); // 무적이 끝난 후 짧은 시간 동안 충돌 방지    
        Player.instance.isInvincibility = false;
        Time.timeScale = 1;       
    }

    // 모든 알파벳을 비활성화
    public void AllAlphabetDisable()
    {        
        for (int i = 0; i < alphabet.Length; i++)
        {
           alphabet[i].SetActive(false);        
        }
        alphaCnt = 0;
    }
}
