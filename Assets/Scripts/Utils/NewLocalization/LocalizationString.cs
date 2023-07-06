using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyUtils.NewLocalization
{
    [CreateAssetMenu(fileName = "StringTranslate", menuName = "Localization/StringTranslate", order = 1)]
    public class LocalizationString : ScriptableObject
    {
        public List<EntryLocalization> listEntry = new List<EntryLocalization>();
    }

    [System.Serializable]
    public class EntryLocalization
    {
        //public string name;
        public Languages language;
        [Multiline, Tooltip("Numero de entrys de acordo com o plural form da lingua")]
        public string[] entrys;
    }
}