using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButtonRight : MonoBehaviour
{
    public GameObject characterObj;
    public void OnClick()
    {
        int nCharacterIndex = characterObj.GetComponent<CharacterSwitch>().nNowCharacterIndex;
        if (nCharacterIndex <= 1)
        {
            nCharacterIndex++;
            characterObj.GetComponent<CharacterSwitch>().nNowCharacterIndex = nCharacterIndex;
            characterObj.GetComponent<CharacterSwitch>().UpdateCharacterImage();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
