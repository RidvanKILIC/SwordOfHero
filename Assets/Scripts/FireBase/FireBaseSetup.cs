//using Firebase;
//using Firebase.Analytics;
//using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RK.Firebase
{
    public class FireBaseSetup : MonoBehaviour
    {
    //    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    //    public static bool firebaseInitialized = false;
    //    [SerializeField] FireBaseLevelLogging _loggingEvent;
    //    // Start is called before the first frame update
    //    void Awake()
    //    {
    //        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
    //            dependencyStatus = task.Result;
    //            if (dependencyStatus == DependencyStatus.Available)
    //            {
    //                InitializeFirebase();
    //            }
    //            else
    //            {
    //                Debug.LogError(
    //                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
    //            }
    //        });
    //    }
    //    void InitializeFirebase()
    //    {
    //        //Debug.Log("Enabling data collection.");
    //        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

    //        //Debug.Log("Set user properties.");
    //        // Set the user's sign up method.
    //        FirebaseAnalytics.SetUserProperty(
    //          FirebaseAnalytics.UserPropertySignUpMethod,
    //          "Google");
    //        // Set the user ID.
    //        FirebaseAnalytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
    //        // Set default session duration values.
    //        FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
    //        firebaseInitialized = true;
    //        AnalyticsLogin();
    //    }
    //    public void AnalyticsLogin()
    //    {
    //        // Log an event with no parameters.
    //       // Debug.Log("Logging a login event.");
    //        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLogin);
    //        _loggingEvent.openUpSceneLog();
    //    }
    //    public void AnalyticsProgress()
    //    {
    //        // Log an event with a float.
    //        //Debug.Log("Logging a progress event.");
    //        FirebaseAnalytics.LogEvent("progress", "percent", 0.4f);
    //    }
    //    public void ResetAnalyticsData()
    //    {
    //        //Debug.Log("Reset analytics data.");
    //        FirebaseAnalytics.ResetAnalyticsData();
    //    }
    }
}

