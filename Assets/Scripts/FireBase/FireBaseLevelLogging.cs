//using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RK.Firebase;
public class FireBaseLevelLogging : MonoBehaviour
{
    //int _sceneBuildIndex;
    //string _sceneName;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    if(SceneManager.GetActiveScene().name != "StartScene")
    //    {
    //        if(FireBaseSetup.firebaseInitialized)
    //            openUpSceneLog();
    //    }
            
    //}
    //public void openUpSceneLog()
    //{
        
    //    Scene activeScene = SceneManager.GetActiveScene();
    //    _sceneBuildIndex = activeScene.buildIndex;
    //    _sceneName = activeScene.name;
    //    //Debug.Log(_sceneName + " opened");
    //    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, new Parameter(FirebaseAnalytics.ParameterLevel,_sceneBuildIndex),
    //    new Parameter(FirebaseAnalytics.ParameterLevelName,_sceneName));
    //}


    //// Update is called once per frame
    //void OnDestroy()
    //{
    //    if (FireBaseSetup.firebaseInitialized)
    //    {
    //        //Debug.Log(_sceneName + " closed");
    //        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, new Parameter(FirebaseAnalytics.ParameterLevel, _sceneBuildIndex),
    //        new Parameter(FirebaseAnalytics.ParameterLevelName, _sceneName));
    //    }

    //}
}
