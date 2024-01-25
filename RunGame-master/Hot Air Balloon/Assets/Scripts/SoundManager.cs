using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private static float timeChk;

    // 효과음
    public AudioClip collision;
    public AudioClip getCoin;
    public AudioClip getItem;
    public AudioClip airplaneWarning;
    public AudioClip airplaneDeparture;
    public AudioClip levelUp;
    public AudioClip Thunder;

    // 효과음 처리용 AudioSource
    public AudioSource EFXSource;
    public AudioSource EFXSourceRepeat;

    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public void PlayOnce(AudioClip clip)
    {
        // 플레이 할 사운드가 getItem이고, 이전 소리 재생 후 0.05초가 지나지 않은 경우 종료
        if (Time.time - timeChk < 0.05f && clip == getItem)
            return;
        EFXSource.PlayOneShot(clip);
        timeChk = Time.time;
    }

    // 사운드를 일정 시간동안만 반복
    public IEnumerator PlayForNSeconds(AudioClip clip, float time)
    {       
        EFXSourceRepeat.loop = true;
        EFXSourceRepeat.clip = clip;
        EFXSourceRepeat.Play();
        yield return new WaitForSeconds(time);

        EFXSourceRepeat.loop = false;
    }
}
