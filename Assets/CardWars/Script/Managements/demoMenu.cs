using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoMenu : MonoBehaviour
{
    [SerializeField] GameObject warningPopUP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activatePopUP()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.deniedSFX);
        warningPopUP.SetActive(true);
        Time.timeScale = 0;
    }
    public void deactivatePopUP()
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        warningPopUP.SetActive(false);
        Time.timeScale = 1;
    }
    public void loadScene(string name)
    {
        SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
        Time.timeScale = 1;
        sceneManager.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        sceneManager.sceneToLoad = "main_Scene";
        sceneManager.loadScene("loadingScene");
    }
    public void quit()
    {
        Application.Quit();
    }
}
