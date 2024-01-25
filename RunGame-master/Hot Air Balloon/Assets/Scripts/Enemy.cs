using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damage = 10;

    private float slowRate;

    // 태그를 확인하고 특수 효과 추가
    private void Start()
    {
        if (transform.tag == "Slow")
            slowRate = 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !Player.instance.isUnbeat)
        {
            // 무적인 대상과 부딪히면 몬스터가 죽고 충돌처리를 하지 않음
            if (Player.instance.isInvincibility)
            {
                gameObject.SetActive(false);
                return;
            }

            if (transform.tag == "Slow") // 특수 효과가 있는 적이면
            {
                // 충돌 이펙트를 충돌 대상들의 중간에 표시하기 위해 Vector3.Lerp를 사용
                EffectManager.instance.StartCoroutine(EffectManager.instance.PlayParticle(Enum.Particle.cloudCollision, 
                    Vector3.Lerp(other.transform.position, transform.position, 0.5f)));
                PlayerMove.instance.StartCoroutine(PlayerMove.instance.SpeedDown(slowRate, 3f));
            }
            else // 까마귀의 충돌
                EffectManager.instance.StartCoroutine(EffectManager.instance.PlayParticle(Enum.Particle.crowCollision,
                    Vector3.Lerp(other.transform.position, transform.position, 0.5f)));
            Player.instance.GetDamage(damage);
            SoundManager.instance.PlayOnce(SoundManager.instance.collision);
        }
    }
}
