using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyUtils.NewLocalization;

public class BuildVersion : MonoBehaviour
{

    [SerializeField]
    private Text textVersion;
    [SerializeField]
    private LocalizationString versionString;

    private LocalizationSettings localizationSettings;

    private void OnEnable()
    {
        
        localizationSettings = LocalizationSettings.Instance;
        localizationSettings.EventTranslate += GetBuildVersion;
        GetBuildVersion(localizationSettings.GetActualLanguage());
    }

    private void GetBuildVersion(Languages languages)
    {
        LocalizationTranslate translate = new LocalizationTranslate(languages);
        string text = translate.Translate(versionString, 1);
        textVersion.text = text + ": v." + Application.version.ToString();
    }

    private void OnDisable()
    {
        localizationSettings.EventTranslate -= GetBuildVersion;
    }
}
