using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectlePool : MonoBehaviour
{
    public GameObject[] Projectile;
    public static ObjectPool[] ProjectilePool;

    public int ProjectileCnt = 5;
    private int poolCnt;

    private void Start()
    {
        poolCnt = Projectile.Length;

        ProjectilePool = new ObjectPool[poolCnt];
        for (int i = 0; i < poolCnt; i++)
            ProjectilePool[i] = new ObjectPool();

        for (int i = 0; i < poolCnt; i++)
        {
            ProjectilePool[i].InitPool(Projectile[i], ProjectileCnt);
        }
    }
}
