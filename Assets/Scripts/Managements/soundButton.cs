using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundButton : MonoBehaviour
{
    [SerializeField] Image buttonImnage;
    [SerializeField] Sprite open;
    [SerializeField] Sprite close;
    public static bool isOpen;
    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.SInstance.adjustTheme();
        if (SaveLoad_Manager.gameData.sfxVolume > 0 || SaveLoad_Manager.gameData.themeVolume > 0f)
            isOpen = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void adjustButtonImage()
    {
        if (isOpen)
            buttonImnage.overrideSprite = open;
        else
            buttonImnage.overrideSprite = close;
    } 
    public void soundClick()
    {
        if (isOpen)
        {
            SaveLoad_Manager.gameData.sfxVolume=0f;
            SaveLoad_Manager.gameData.themeVolume = 0f;
            SoundManager.SInstance.adjustSFXValume(SaveLoad_Manager.gameData.sfxVolume);
            SoundManager.SInstance.adjustThemeValume(SaveLoad_Manager.gameData.themeVolume);
            isOpen = false;
            adjustButtonImage();
        }
        else
        {
            SaveLoad_Manager.gameData.sfxVolume = 0.5f;
            SaveLoad_Manager.gameData.themeVolume = 0.5f;
            SoundManager.SInstance.adjustSFXValume(SaveLoad_Manager.gameData.sfxVolume);
            SoundManager.SInstance.adjustThemeValume(SaveLoad_Manager.gameData.themeVolume);
            isOpen = true;
            adjustButtonImage();
        }

    }
}
