using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatOnOff : MonoBehaviour
{
    public Canvas canvasChat;

    public void OnClick()
    {
        if (canvasChat.enabled)
        {
            canvasChat.enabled = false;
        }
        else
        {
            canvasChat.enabled = true;
        }
    }
}
