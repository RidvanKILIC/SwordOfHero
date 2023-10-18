using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Variables
    [Header("Audio Sources")]
    [SerializeField]public AudioSource themeAS;
    [SerializeField]public AudioSource SFXS;
    [Header("Audio Clips")]
    [SerializeField] AudioClip themeAudioBattle;
    [SerializeField] AudioClip themeAudioMenu;
    [SerializeField] AudioClip themeCardGame;
    [Header("SFXs")]
    public AudioClip wearSFX;
    public AudioClip buySFX;
    public AudioClip deniedSFX;
    public AudioClip buttonSFX;
    public AudioClip swipeSFX;
    public AudioClip sliderSFX;
    public AudioClip achivementSFX;
    public AudioClip playerWinSFX;
    static bool themePlay = true;
    private static SoundManager instance;
    public static SoundManager SInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("UI instance is null");
            }
            return instance;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        themeAS.volume = SaveLoad_Manager.gameData.themeVolume;
        SFXS.volume = SaveLoad_Manager.gameData.sfxVolume;
        DontDestroyOnLoad(this.gameObject);
        adjustTheme();
        //Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        playPauseTheme(themePlay);
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void adjustTheme()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "miniGame_Scene")
        {
            //Debug.Log("here");
            setThemeAudio(themeAudioBattle);
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "cardGame_Scene")
        {
            //Debug.Log("here");
            setThemeAudio(themeCardGame);
        }
        else
        {
            setThemeAudio(themeAudioMenu);
        }
    }
    /// <summary>
    /// Sets theme audio source's volume with the given value
    /// </summary>
    /// <param name="_value"></param>
    public void adjustThemeValume(float _value)
    {
        themeAS.volume = _value;

    }
    /// <summary>
    /// Sets theme SFX source's volume with the given value
    /// </summary>
    /// <param name="_value"></param>
    public void adjustSFXValume(float _value)
    {
        SFXS.volume = _value;

    }

    /// <summary>
    /// Plays 
    /// SFXa
    /// </summary>
    /// <param name="sfx"></param>
    public void playSfx(AudioClip sfx, bool looping = false)
    {
        if (looping)
        {
            SFXS.loop = true;
            SFXS.clip = sfx;
            SFXS.Play();
        }
        else
        {
            SFXS.loop = false;
            SFXS.PlayOneShot(sfx);
        }
        
    }
    /// <summary>
    /// if true resumes else pauses theme as
    /// </summary>
    /// <param name="state"></param>
    public void playPauseTheme(bool state)
    {
        themePlay = state;
        if (!state)
            themeAS.Pause();
        else if (!themeAS.isPlaying)
            themeAS.UnPause();
    }

    public void setThemeAudio(AudioClip _clip)
    {
        if(_clip != null )
        {
            if(themeAS.clip!= _clip)
            {
                themeAS.clip = _clip;
                themeAS.Play();
            }
        }
    }
}
