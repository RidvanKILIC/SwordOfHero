using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RK.LanguageSystem;
public class StartScene_Controller : MonoBehaviour
{
    //[SerializeField]Slider loadingSlider;
    [SerializeField] GameObject tabScreen;
    [SerializeField] GameObject loadingIcon;
    SaveLoad_Manager SL_manager;
    ObjectsLists objectsLists;
    private void Start()
    {
        tabScreen.SetActive(false);
        loadingIcon.SetActive(true);
        objectsLists = GameObject.FindObjectOfType<ObjectsLists>();
        SL_manager = GameObject.FindObjectOfType<SaveLoad_Manager>();
        ///loadingSlider.maxValue = 1;
        //loadingSlider.value = 0;
        checkGameDatas();
    }
    public void checkGameDatas()
    {

        SaveLoad_Manager.SInstance.readJson();
        LanguageController.LInstance.initLanguage();
        LanguageController.LInstance.setLocal();
        objectsLists.fillLists();
        Invoke("directToNextScene", 1);
        //directToNextScene();
    }
    public void directToNextScene()
    {
        if (!SaveLoad_Manager.gameData.isApeperanceSet)
        {
            sceneManager.currentScene = SceneManager.GetActiveScene().name;
            sceneManager.sceneToLoad = "selection_Scene";
            //sceneManager.loadScene("selection_Scene");
            StartCoroutine(loadAsyncScene("loadingScene"));
            //Direct Tutorial
        }
        else
        {
            sceneManager.currentScene = SceneManager.GetActiveScene().name;
            sceneManager.sceneToLoad = "main_Scene";
            //sceneManager.loadScene("loadinScene");
            StartCoroutine(loadAsyncScene("loadingScene"));
            //Direct Main Scene
        }
    }
    IEnumerator loadAsyncScene(string _sceneName)
    {

        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            //print(asyncOperation.progress);
            //loadingSlider.value = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
            {
                loadingIcon.SetActive(false);
                tabScreen.SetActive(true);
                if(Input.GetMouseButton(0))
                    asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
