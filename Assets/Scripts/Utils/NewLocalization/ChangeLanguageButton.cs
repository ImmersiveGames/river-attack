using UnityEngine;
using UnityEngine.UI;

namespace Utils.NewLocalization
{
    public class ChangeLanguageButton : MonoBehaviour
    {

        public Languages Languages { get; private set; }
        [SerializeField]
        private Text lebelText;
        [SerializeField]
        private Image flagImage;

        public void SetLanguages(Languages lang)
        {
            Languages = lang;
        }

        private void OnEnable()
        {
            InitializeButton();
        }
        private void Start()
        {
            InitializeButton();
        }
        private void InitializeButton()
        {
            if (Languages != null)
            {
                gameObject.name = Languages.name;
                lebelText.text = Languages.fullName;
                flagImage.sprite = Languages.flag;
                GetComponent<Button>().onClick.AddListener(() => LocalizationSettings.Instance.ChangeActualLanguage(Languages));
            }
        }
    }
}
