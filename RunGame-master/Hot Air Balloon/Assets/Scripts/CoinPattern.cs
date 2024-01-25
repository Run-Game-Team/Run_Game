using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPattern : MonoBehaviour
{
    public Vector3[] GetPattern()
    {
        int rand = Random.Range(1, (7+1));

        switch(rand)
        {
            case 1:
                return Pattern1();
            case 2:
                return Pattern2();
            case 3:
                return Pattern3();
            case 4:
                return Pattern4();
            case 5:
                return Pattern5();
            case 6:
                return Pattern6();
            case 7:
                return Pattern7();
        }

        return null;
    }

    Vector3[] Pattern1()
    {
        Vector3[] vec = new[] {
            new Vector3(-1f, 1f, 0f)
            , new Vector3(0f, 1f, 0f)
            , new Vector3(1f, 1f, 0f)
            , new Vector3(-1f, 0f, 0f)
            , new Vector3(0f, 0f, 0f)
            , new Vector3(1f, 0f, 0f)
            , new Vector3(-1f, -1f, 0f)
            , new Vector3(0f, -1f, 0f)
            , new Vector3(1f, -1f, 0f)
        };

        return vec;
    }

    Vector3[] Pattern2()
    {
        Vector3[] vec = new[] {
            new Vector3(0f, 1.4f, 0f)
            , new Vector3(-0.7f, 0.7f, 0f)
            , new Vector3(0.7f, 0.7f, 0f)
            , new Vector3(-1.4f, 0f, 0f)
            , new Vector3(0f, 0f, 0f)
            , new Vector3(1.4f, 0f, 0f)
            , new Vector3(-0.7f, -0.7f, 0f)
            , new Vector3(0.7f, -0.7f, 0f)
            , new Vector3(0f, -1.4f, 0f)
        };

        return vec;
    }

    Vector3[] Pattern3()
    {
        Vector3[] vec = new[] {
            new Vector3(0f, 1f, 0f)
            , new Vector3(2f, 1f, 0f)
            , new Vector3(-1f, 0f, 0f)
            , new Vector3(0f, 0f, 0f)
            , new Vector3(1f, 0f, 0f)
            , new Vector3(2f, 0f, 0f)
            , new Vector3(3f, 0f, 0f)
            , new Vector3(0f, -1f, 0f)
            , new Vector3(2f, -1f, 0f)
        };

        return vec;
    }

    Vector3[] Pattern4()
    {
        Vector3[] vec = new[] {
            new Vector3(0f, 0f, 0f)
            , new Vector3(0.7f, 0f, 0f)
            , new Vector3(1.4f, 0f, 0f)
            , new Vector3(2.1f, 0f, 0f)
            , new Vector3(2.8f, 0f, 0f)
            , new Vector3(3.5f, 0f, 0f)
        };

        return vec;
    }

    Vector3[] Pattern5()
    {
        Vector3[] vec = new[] {
            new Vector3(1.4f, 2f, 0f)
            , new Vector3(4.2f, 2f, 0f)
            , new Vector3(0.7f, 1f, 0f)
            , new Vector3(2.1f, 1f, 0f)
            , new Vector3(3.5f, 1f, 0f)
            , new Vector3(4.9f, 1f, 0f)
            , new Vector3(0f, 0f, 0f)
            , new Vector3(2.8f, 0f, 0f)
            , new Vector3(5.6f, 0f, 0f)
        };

        return vec;
    }

    Vector3[] Pattern6()
    {
        Vector3[] vec = new[] {
            new Vector3(0.7f, 1f, 0f)
            , new Vector3(1.4f, 1f, 0f)
            , new Vector3(2.7f, 1f, 0f)
            , new Vector3(3.4f, 1f, 0f)
            , new Vector3(0f, 0f, 0f)
            , new Vector3(2f, 0f, 0f)
            , new Vector3(4f, 0f, 0f)
            , new Vector3(0.7f, -1f, 0f)
            , new Vector3(1.4f, -1f, 0f)
            , new Vector3(2.7f, -1f, 0f)
            , new Vector3(3.4f, -1f, 0f)
        };

        return vec;
    }

    Vector3[] Pattern7()
    {
        Vector3[] vec = new[] {
            new Vector3(3f, 2f, 0f)
            , new Vector3(3f, 1f, 0f)
            , new Vector3(4f, 1f, 0f)
            , new Vector3(0f, 0f, 0f)
            , new Vector3(1f, 0f, 0f)
            , new Vector3(2f, 0f, 0f)
            , new Vector3(3f, 0f, 0f)
            , new Vector3(4f, 0f, 0f)
            , new Vector3(5f, 0f, 0f)
            , new Vector3(3f, -1f, 0f)
            , new Vector3(4f, -1f, 0f)
            , new Vector3(3f, -2f, 0f)
        };

        return vec;
    }
}
