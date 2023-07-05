using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "Variables/String", order = 3)]
    public class StringVariable : ScriptableObject
    {
        [SerializeField]
        private string value = "";

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
