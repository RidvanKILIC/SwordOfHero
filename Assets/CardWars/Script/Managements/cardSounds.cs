using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardSounds : MonoBehaviour
{
    #region Variables
    [Header("Audio Sources")]
    //[SerializeField] AudioSource themeAS;
    //[SerializeField] AudioSource SFXS;
    [SerializeField] AudioSource DialogSource;
    [Header("Audio Clips")]
    [SerializeField] AudioClip themeAudio;
    [Header("SFXs")]
    public AudioClip cardDealingSFX;
    public AudioClip cardsMatchedSFX;
    public AudioClip cardMoveToDemonstrationFX;
    public AudioClip cardsNotMatchedSFX;
    public AudioClip cardsTurningSFX;
    public AudioClip buttonSFX;
    public AudioClip cardMoveToHandSFX;
    public AudioClip deckScaleUpSFX;
    public AudioClip deckScaleDownSFX;
    public AudioClip openDeckDoorSFX;
    public AudioClip closeDeckDoorSFX;
    public AudioClip cardPlacedSFX;
    public AudioClip attackMovementSFX;
    public AudioClip healMovementFX;
    public AudioClip attackSFX;
    public AudioClip healSfx;
    private static cardSounds instance;
    public static cardSounds SInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("UI instance is null");
            }
            return instance;
        }
    }
    static bool themePlay = true;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
           
        }
            
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    ///// <summary>
    ///// Sets theme audio source's volume with the given value
    ///// </summary>
    ///// <param name="_value"></param>
    //public void adjustThemeValume(float _value)
    //{
    //    themeAS.volume = _value;

    //}
    ///// <summary>
    ///// Sets theme SFX source's volume with the given value
    ///// </summary>
    ///// <param name="_value"></param>
    //public void adjustSFXValume(float _value)
    //{
    //    SFXS.volume = _value;

    //}

    ///// <summary>
    ///// Plays 
    ///// SFXa
    ///// </summary>
    ///// <param name="sfx"></param>
    //public void playSfx(AudioClip sfx)
    //{
    //    SFXS.PlayOneShot(sfx);
    //}

    ///// <summary>
    ///// Plays 
    ///// Dialog Audio
    ///// </summary>
    ///// <param name="sfx"></param>
    //public void playDialog(AudioClip sfx)
    //{
    //    SFXS.PlayOneShot(sfx);
    //}

    ///// <summary>
    ///// if true resumes else pauses theme as
    ///// </summary>
    ///// <param name="state"></param>
    //public void playPauseTheme(bool state)
    //{
    //    themePlay = state;
    //    if (!state)
    //        themeAS.Pause();
    //    else if (!themeAS.isPlaying)
    //        themeAS.UnPause();
    //}
}
