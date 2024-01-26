using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    public Button leftBtn;
    public Button rightBtn;
    public Sprite m_sprite;
    public List<Sprite> imageList;

    Image nowCharacterImage;
    public int nNowCharacterIndex;
    bool isUpdateUI;

    // Start is called before the first frame update
    void Start()
    {
        nowCharacterImage = GetComponent<Image>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(isUpdateUI)
        {
            isUpdateUI = false;
            UpdateUI();
        }
    }
    void UpdateUI()
    {
        leftBtn.interactable = nNowCharacterIndex > 0;
        rightBtn.interactable = nNowCharacterIndex < 2;
        isUpdateUI = true;
    }
    public void UpdateCharacterImage()
    {
        nowCharacterImage.sprite = imageList[nNowCharacterIndex];
    }
}
