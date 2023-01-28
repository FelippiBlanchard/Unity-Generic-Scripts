using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Language
{
    public class LanguageFirstTime : MonoBehaviour
    {
        private void Awake()
        {
            if(PlayerPrefs.GetInt("FirstTimeLanguage", 0) == 0)
            {
                SetLanguageBySystem();
                PlayerPrefs.SetInt("FirstTimeLanguage", 1);
            }
        }
        private void SetLanguageBySystem()
        {
            var languageIndex = 0;
            switch (Application.systemLanguage)
            {
                case SystemLanguage.English:
                    languageIndex = (int)LanguageText.Language.English;
                    break;

                case SystemLanguage.French:
                    languageIndex = (int)LanguageText.Language.French;
                    break;

                case SystemLanguage.Portuguese:
                    languageIndex = (int)LanguageText.Language.Portuguese;
                    break;

                case SystemLanguage.Spanish:
                    languageIndex = (int)LanguageText.Language.Spanish;
                    break;

                default:
                    languageIndex = (int)LanguageText.Language.English;
                    break;
            }
            Debug.Log(Application.systemLanguage);
            PlayerPrefs.SetInt("Language", languageIndex);
        }
    }
}
