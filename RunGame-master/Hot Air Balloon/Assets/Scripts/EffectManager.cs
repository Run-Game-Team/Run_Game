using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    public ParticleSystem[] effects;
    private ParticlePool[] particlePool;

    #region singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        particlePool = new ParticlePool[effects.Length];

        for (int i = 0; i < effects.Length; i++)
        {
            particlePool[i] = new ParticlePool(effects[i], 5);
        }
    }

    // 배열에 있는 파티클들을 구분하기 위해 Enum 타입으로 인자를 받음
    public IEnumerator PlayParticle(Enum.Particle particleType, Vector3 pos)
    {
        ParticleSystem particleToPlay = particlePool[(int)particleType].getAvailableParticle();

        if(particleToPlay != null)
        {
            particleToPlay.gameObject.SetActive(true);
            // 파티클이 실행중이면 실행을 멈추고
            if (particleToPlay.isPlaying)
                particleToPlay.Stop();

            particleToPlay.transform.position = pos;
            particleToPlay.Play();
        }
        yield return new WaitForSeconds(1f); // 1초 뒤에 이펙트 상태를 false로 바꿈
        particleToPlay.Stop();
        particleToPlay.gameObject.SetActive(false);
    }

    // 이펙트를 일정 시간동안만 반복
    public IEnumerator PlayForNSeconds(Enum.Particle particleType, Vector3 pos, float time)
    {
        ParticleSystem particleToPlay = particlePool[(int)particleType].getAvailableParticle();

        // 종료될떄까지 이펙트가 캐릭터를 따라다니게
        if (particleToPlay != null)
        {
            particleToPlay.gameObject.SetActive(true);
            // 파티클이 실행중이면 실행을 멈추고
            if (particleToPlay.isPlaying)
                particleToPlay.Stop();

            // 이펙트가 플레이어의 위치를 따라다니도록 함
            StartCoroutine("FollowingPlayer", particleToPlay);
            particleToPlay.Play();
        }
        yield return new WaitForSeconds(time);
        particleToPlay.Stop();
        particleToPlay.gameObject.SetActive(false);
        
    }

    // 이펙트가 지정한 위치를 따라다니도록 함
    IEnumerator FollowingPlayer(ParticleSystem particle)
    {
        Vector3 pos = new Vector3(0, 0, 0);

        // active 상태가 true인 동안 무한 반복
        while(particle.gameObject.activeSelf)
        {
            // 플레이어가 죽었으면 실행중인 이펙트를 비활성화
            if(Player.instance.isDead)
            {
                particle.Stop();
                particle.gameObject.SetActive(false);
                break;
            }

            // 피벗이 아래에 있는 이펙트의 경우
            if (particle.tag == "PivotIsBelow")
            {
                pos = Player.instance.transform.GetChild(2).position;
                particle.transform.position = pos;
            }
            else
                particle.transform.position = Player.instance.transform.position;
            yield return null;
        }
    }
}
