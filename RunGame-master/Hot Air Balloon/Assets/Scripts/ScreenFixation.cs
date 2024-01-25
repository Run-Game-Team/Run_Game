using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFixation : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(800, 480, false);
    }
}
