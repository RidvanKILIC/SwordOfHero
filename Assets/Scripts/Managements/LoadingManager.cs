using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] Image iconImage;
    [SerializeField] List<Sprite> iconList = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        setIconImage();
        playPulseAnim();
        changeScene();

    }
    public void setIconImage()
    {
        int index = Random.Range(0, iconList.Count);
        iconImage.overrideSprite = iconList.ElementAt(index);
    }
    async void playPulseAnim()
    {
        LeanTween.scale(iconImage.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 1f).setEase(LeanTweenType.easeInSine).setOnComplete(()=>
        LeanTween.scale(iconImage.gameObject, new Vector3(1f,1f, 1f), 1f).setEase(LeanTweenType.easeOutSine)
        ).setLoopPingPong();
    } 
    //private async UniTask iconPulseEffect()
    //{

    //}
    public void changeScene()
    {
        Invoke("changeSceneDelayed",1f);
    }
    public void changeSceneDelayed()
    {
        StartCoroutine(loadAsyncScene(sceneManager.sceneToLoad));
    }
    IEnumerator loadAsyncScene(string _sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                
                //if (Input.GetMouseButton(0))
                    asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
