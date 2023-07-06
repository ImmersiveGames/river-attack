using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyUtils.Variables
{
    public abstract class Variables : ScriptableObject
    {

#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public float Value;
    }
}
