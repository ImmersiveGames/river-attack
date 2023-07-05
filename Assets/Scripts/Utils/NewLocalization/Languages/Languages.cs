using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utils.NewLocalization
{
    [CreateAssetMenu(fileName = "NewLanguage", menuName = "Localization/NewLanguage", order = 100)]
    public class Languages : ScriptableObject
    {
        new public string name;
        public string fullName;
        public string abrevName;
        public string language;
        public string charset = "UTF-8";
        public int nplural = 2;
        public string pluralForm = "(n != 1)";
        public Sprite flag;
    }
}
