using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
namespace Utils.NewLocalization
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
        //public enum StringFormat { None, AllUpcase, AllDownCase, FirstLetterUp, AllFirstLetterUp }

        #region Events
        public delegate void TranslateEventHandler(Languages lang);
        public event TranslateEventHandler EventTranslate;
        #endregion

        private void OnEnable()
        {
            if (defaultLanguage == null)
                defaultLanguage = localizations[0];
            if (actualLanguage == null)
                actualLanguage = defaultLanguage;
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