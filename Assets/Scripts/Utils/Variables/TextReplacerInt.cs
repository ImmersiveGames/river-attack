using UnityEngine.UI;
using UnityEngine;
namespace Utils.Variables
{
    public class TextReplacerInt : MonoBehaviour
    {

        public Text Text;

        public IntVariable Variable;

        public bool AlwaysUpdate;

        private void OnEnable()
        {
            if(Variable != null)
                Text.text = Variable.Value.ToString();
        }

        private void Update()
        {
            if (AlwaysUpdate)
            {
                Text.text = Variable.Value.ToString();
            }
        }
    }
}