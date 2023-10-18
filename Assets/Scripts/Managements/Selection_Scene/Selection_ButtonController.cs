using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Selection_ButtonController : MonoBehaviour
{

    [SerializeField]Selection_MenuController selection_MenuController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void leftNationButton()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.swipeSFX);
        selection_MenuController.decrementNationIndex();
    }
    public void rightNationButton()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.swipeSFX);
        selection_MenuController.incrementNationIndex();
    }
    public void selectNation()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        selection_MenuController.selectNation();
    }
    public void registerNameBtn()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        selection_MenuController.registerName();
    }
    public void leftAppeareanceButton()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.swipeSFX);
        selection_MenuController.decrementApearanceIndex();
    }
    public void rightAppeareanceButton()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.swipeSFX);
        selection_MenuController.incrementApearanceIndex();
    }
    public void selectAppeareance()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        selection_MenuController.selectApearances();
    }
    public void onInputTextCChange()
    {

        selection_MenuController.CleanInput();
    }
    public void appearanceBackBtn()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        selection_MenuController.appearanceBack();
    }
    public void nationBckBtn()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        selection_MenuController.nationBack();
    }
}
