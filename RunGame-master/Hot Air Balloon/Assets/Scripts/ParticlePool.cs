using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticlePool : MonoBehaviour
{
    int particleAmount;
    ParticleSystem[] particle;

    public ParticlePool(ParticleSystem particlePrefab, int amount = 5)
    {
        particleAmount = amount;
        particle = new ParticleSystem[particleAmount];

        for(int i=0; i<particleAmount; i++)
        {
            particle[i] = Instantiate(particlePrefab, new Vector3(0, 0, 0), particlePrefab.transform.rotation);
            particle[i].Stop();
            particle[i].gameObject.SetActive(false);
        }
    }
    
    public ParticleSystem getAvailableParticle()
    {
        ParticleSystem firstObject = null;

        // 배열에서 파티클을 하나 꺼내고
        firstObject = particle[0];
        // 배열을 시프트 시킨다
        shiftUp();

        return firstObject;
    }

    public int getAmount()
    {
        return particleAmount;
    }

    // 배열을 왼쪽으로 시프트 시킨 후 첫번째 오브젝트를 맨 끝으로 이동
    private void shiftUp()
    {
        ParticleSystem firstObject;

        firstObject = particle[0];
        Array.Copy(particle, 1, particle, 0, particle.Length - 1);

        particle[particle.Length - 1] = firstObject;
    }


}
