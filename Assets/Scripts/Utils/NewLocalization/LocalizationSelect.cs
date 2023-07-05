using UnityEngine;

namespace Utils.NewLocalization
{
    public class LocalizationSelect : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonLanguages;
        [SerializeField]
        private RectTransform panelLanguage;

        private LocalizationSettings localizationSettings;

        private void OnEnable()
        {
            SetInitialReferences();
            CreateButtonsLanguages();
        }

        private void SetInitialReferences()
        {
            localizationSettings = LocalizationSettings.Instance;
        }

        private void CreateButtonsLanguages()
        {
            bool btnrept = false;
            int totallanguage = localizationSettings.localizations.Count;
            for (int i = 0; i < totallanguage; i++)
            {
                ChangeLanguageButton[] btnlng = panelLanguage.GetComponentsInChildren<ChangeLanguageButton>();
                if (btnlng.Length > 1)
                {
                    for (int y = 0; y < btnlng.Length; y++)
                    {
                        if (panelLanguage.GetChild(y).GetComponent<ChangeLanguageButton>() == null) return;
                        if (panelLanguage.GetChild(y).GetComponent<ChangeLanguageButton>().Languages == localizationSettings.localizations[i])
                        {
                            btnrept = true;
                        }
                    }
                }
                if (!btnrept)
                {
                    GameObject btn = Instantiate(buttonLanguages, panelLanguage);
                    btn.GetComponent<ChangeLanguageButton>().SetLanguages(localizationSettings.localizations[i]);
                }
                btnrept = false;
            }
        }
    }
}
