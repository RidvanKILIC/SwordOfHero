using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] GameObject winContainer;
    [SerializeField] GameObject lostContainer;
    [SerializeField] GameObject tieContainer;
    [SerializeField] List<GameObject> starList = new List<GameObject>();
    [SerializeField] List<GameObject> lostStarList = new List<GameObject>();
    [SerializeField] TMP_Text earnedGold;
    [SerializeField] TMP_Text earnedXP;
    [SerializeField] TMP_Text Lose_earnedGold;
    [SerializeField] TMP_Text Lose_earnedXP;
    [SerializeField] TMP_Text tie_earnedGold;
    [SerializeField] TMP_Text tie_earnedXP;
    [SerializeField] AudioClip _winAudioClip;
    [SerializeField] AudioClip _looseAudioClip;
    int xp;
    int gold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gameOver(int starCount,int _earnedGold,int _earnedXP)
    {
        gold = _earnedGold;
        xp = _earnedXP;
        if (starCount > 0)
        {
            SoundManager.SInstance.playSfx(_winAudioClip);
            earnedGold.text = gold.ToString();
            earnedXP.text = xp.ToString();
            winContainer.SetActive(true);
            //StartCoroutine(starAnimations(starCount,starList));
        }
        else if(starCount == 0)
        {
            SoundManager.SInstance.playSfx(_looseAudioClip);
            Lose_earnedGold.text = gold.ToString();
            Lose_earnedXP.text = gold.ToString();
            lostContainer.SetActive(true);
            //StartCoroutine(starAnimations(3, lostStarList));
        }
        else
        {
            SoundManager.SInstance.playSfx(_looseAudioClip);
            tie_earnedGold.text = gold.ToString();
            tie_earnedXP.text = gold.ToString();
            tieContainer.SetActive(true);
        }
        //Debug.Log(starCount);

    }
    IEnumerator starAnimations(int starCount, List<GameObject> list)
    {
        //Debug.Log("Star List Count: " + starList.Count);
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < starCount; i++)
        {
            //Debug.Log("Problem");
            list.ElementAt(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void CollectButton()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        SaveLoad_Manager.gameData.playerInfos.expPoint += xp;
        SaveLoad_Manager.gameData.playerInfos.earnedGold = gold;
        SaveLoad_Manager.SInstance.saveJson();
        leaveButton();
    }

    public void retryButton()
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1;

        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        sceneManager.reloadGame();
    }
    public void leaveButton()
    {
        if(Time.timeScale ==0)
            Time.timeScale = 1;

        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        sceneManager.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneManager.sceneToLoad = "main_Scene";
        sceneManager.loadScene("loadingScene");
    }
}
