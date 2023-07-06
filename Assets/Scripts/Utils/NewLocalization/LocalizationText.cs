using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
namespace MyUtils.NewLocalization
{
    [RequireComponent(typeof(Text))]
    public class LocalizationText : MonoBehaviour
    {
        public LocalizationString localizationString;
        public string prefix;
        public LocalizationTranslate.StringFormat stringFormat;
        public string suffix;
        public bool alwaysUpdate;

        private Text myText;
        private LocalizationSettings localizationSettings;

        private void OnEnable()
        {
            SetInitialReferences();
            localizationSettings.EventTranslate += Translate;
            Translate();
        }
        private void Translate(Languages lng)
        {
            LocalizationTranslate translate = new LocalizationTranslate(lng);
            myText.text = prefix + translate.Translate(localizationString, stringFormat) + suffix;
        }

        public void Translate()
        {
            Translate(localizationSettings.GetActualLanguage());
        }

        private void SetInitialReferences()
        {
            localizationSettings = LocalizationSettings.Instance;
            myText = GetComponent<Text>();
        }
        private void Update()
        {
            if (alwaysUpdate)
                Translate();
        }

        [ContextMenu("UpdateTranslate")]
        public void UpdateText()
        {
            localizationSettings = LocalizationSettings.Instance;
            myText = GetComponent<Text>();
            Translate();
        }
        private void OnDisable()
        {
            localizationSettings.EventTranslate -= Translate;
        }
    }
}
