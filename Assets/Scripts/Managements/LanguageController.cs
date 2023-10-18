using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
namespace RK.LanguageSystem
{
    public class LanguageController : MonoBehaviour
    {
        int localID;
        bool active;
        private static LanguageController instance;
        public static LanguageController LInstance
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
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
            {
                instance = this;

            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void initLanguage()
        {
            localID = SaveLoad_Manager.gameData.localID;
        }
        public void setLocal()
        {
            if (!active)
                StartCoroutine(setLocalRoutine());
        }
        public void changeLocal()
        {
            if (!active)
            {
                int IDCounter = LocalizationSettings.AvailableLocales.Locales.Count;
                localID++;
                if (localID >= IDCounter)
                {
                    localID = 0;

                }
                SaveLoad_Manager.gameData.localID = localID;

                StartCoroutine(setLocalRoutine());
            }

        }
        IEnumerator setLocalRoutine()
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localID];
            SaveLoad_Manager.SInstance.saveJson();
            active = false;
            
        }
    }
}

