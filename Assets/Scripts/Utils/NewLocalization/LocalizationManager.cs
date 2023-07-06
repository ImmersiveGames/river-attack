using UnityEngine;

namespace MyUtils.NewLocalization
{
    public class LocalizationManager : MonoBehaviour
    {
        [SerializeField]
        private LocalizationSettings localizationSettings;

        private void OnEnable()
        {
            localizationSettings.ChangeActualLanguage(GameManagerSaves.Instance.LoadLanguages());   
        }

    }
}
