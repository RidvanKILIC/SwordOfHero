using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RK.LanguageSystem;
public class settingsController : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] Slider themeSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] GameObject warningPanel;
    // Start is called before the first frame update
    void Start()
    {
        themeSlider.value=SoundManager.SInstance.themeAS.volume;
        SFXSlider.value=SoundManager.SInstance.SFXS.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activatePopup()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.deniedSFX);
        warningPanel.SetActive(true);
    }
    public void deActivatePopup()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        warningPanel.SetActive(false);

    }
    public void changeLanguage()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        LanguageController.LInstance.changeLocal();
    }
    public void setThemeVolume()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.sliderSFX);
        SoundManager.SInstance.themeAS.volume = themeSlider.value;
        SaveLoad_Manager.gameData.themeVolume = themeSlider.value;
        SaveLoad_Manager.SInstance.saveJson();
    }
    public void setSFXASVolume()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.sliderSFX);
        SoundManager.SInstance.SFXS.volume = SFXSlider.value;
        SaveLoad_Manager.gameData.sfxVolume = themeSlider.value;
        SaveLoad_Manager.SInstance.saveJson();
    }
    public void resetGame()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        SaveLoad_Manager.SInstance.deleteJson();
        sceneManager.loadScene("StartScene");
    }
    public void quitGame()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        SaveLoad_Manager.SInstance.saveJson();
        Application.Quit();
    }
}
