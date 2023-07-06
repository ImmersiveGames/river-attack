using UnityEngine;
using UnityEngine.UI;

namespace MyUtils.NewLocalization
{
    public class ChangeLanguageButton : MonoBehaviour
    {
        public Languages Languages { get; private set; }
        [SerializeField]
        private Text lebelText;
        [SerializeField]
        private Image flagImage;
        [SerializeField]
        private GameObject cursor;
        [SerializeField]
        private bool abbreviation;

        private LocalizationSettings localization;

        public void SetLanguages(Languages lang)
        {
            Languages = lang;
        }

        private void OnEnable()
        {
            localization = LocalizationSettings.Instance;
            localization.EventTranslate += ManagerCursor;
            //InitializeButton();
        }
        private void Start()
        {
            InitializeButton();
        }
        private void InitializeButton()
        {
            //Debug.Log("Language: "+ localization.GetActualLanguage().name);
            if (Languages != null)
            {
                gameObject.name = Languages.name;
                lebelText.text = (abbreviation) ? Languages.abrevName : Languages.fullName;
                flagImage.sprite = Languages.flag;
                Button btn = GetComponent<Button>();
                btn.onClick.AddListener(() => localization.ChangeActualLanguage(Languages));
                ManagerCursor(Languages);
                //cursor.SetActive(localization.GetActualLanguage() == Languages);
            }
        }

        private void ManagerCursor(Languages lang)
        {
            cursor.SetActive(localization.GetActualLanguage() == Languages);
        }
    }
}
