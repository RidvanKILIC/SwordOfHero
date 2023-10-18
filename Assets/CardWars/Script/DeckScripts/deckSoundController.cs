using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deckSoundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDealSound()
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.deckScaleUpSFX);
    }
    public void finishDealSound()
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.deckScaleDownSFX);
    }
    public void openDoorSound()
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.openDeckDoorSFX);
    }
    public void closeDoorSound()
    {
        SoundManager.SInstance.playSfx(cardSounds.SInstance.closeDeckDoorSFX);
    }
}
