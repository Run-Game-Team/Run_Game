using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnable : MonoBehaviour
{
    public GameObject UI;

    public void Enable()
    {
        UI.SetActive(true);
    }

    public void Disable()
    {
        UI.SetActive(false);
    }
}
