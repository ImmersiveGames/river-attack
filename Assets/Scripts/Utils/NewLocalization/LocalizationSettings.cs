using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
namespace MyUtils.NewLocalization
{
    [CreateAssetMenu(fileName = "LocalizationSettings", menuName = "Localization/LocalizationSettings", order = 101)]
    public class LocalizationSettings : SingletonScriptableObject<LocalizationSettings>
    {
        [SerializeField]
        public Languages defaultLanguage;
        [SerializeField]
        private Languages actualLanguage;
        [SerializeField]
        public List<Languages> localizations;

        public CultureInfo cultureInfo;

        #region Events
        public delegate void TranslateEventHandler(Languages lang);
        public event TranslateEventHandler EventTranslate;
        #endregion

        private void OnEnable()
        {
            if (defaultLanguage == null)
                defaultLanguage = localizations[0];
            actualLanguage = defaultLanguage;// GameManagerSaves.Instance.LoadLanguages();
        }

        public void ChangeActualLanguage(Languages lang)
        {
            actualLanguage = lang;
            cultureInfo = new CultureInfo(actualLanguage.language);
            CallEventTranslate(lang);
        }

        public Languages GetActualLanguage()
        {
            return actualLanguage;
        }

        public static string Translate(LocalizationString localizationString, float plural = 1, LocalizationTranslate.StringFormat format = LocalizationTranslate.StringFormat.None)
        {
            LocalizationTranslate translate = new LocalizationTranslate(Instance.actualLanguage);
            return translate.Translate(localizationString, plural, format);
        }

        #region CallMethods
        public void CallEventTranslate(Languages lang)
        {
            if (EventTranslate != null)
            {
                EventTranslate(lang);
            }
        }
        #endregion
    }
}