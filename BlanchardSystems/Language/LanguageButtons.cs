using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlanchardSystems.Language
{
    public class LanguageButtons : MonoBehaviour
    {
        [SerializeField] private List<Image> _imageButtons;

        [SerializeField] private Color _desactive;
        [SerializeField] private Color _active;


        private void OnEnable()
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            int languageIndex = PlayerPrefs.GetInt("Language", 0);
            for (int i = 0; i < _imageButtons.Count; i++)
            {
                _imageButtons[i].color = i == languageIndex ? _active : _desactive;
            }
        }

        public void SetLanguage(int language)
        {
            LanguageTextController.SetLanguage(language);
            UpdateButtons();
        }

    }
}
