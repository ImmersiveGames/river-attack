using UnityEngine;
using UnityEngine.UI;

namespace Utils.Variables
{
    public class TextReplacer : MonoBehaviour
    {
        public Text fieldText;

        public StringVariable stringVariable;

        public bool AlwaysUpdate;

        protected virtual void OnEnable()
        {
            if (fieldText == null)
                fieldText = GetComponent<Text>();
            if(stringVariable != null)
                fieldText.text = stringVariable.Value;
        }

        protected virtual void Update()
        {
            if (AlwaysUpdate)
            {
                fieldText.text = stringVariable.Value;
            }
        }
    }
}
