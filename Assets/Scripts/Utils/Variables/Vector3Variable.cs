using UnityEngine;

namespace MyUtils.Variables
{
    [CreateAssetMenu(fileName = "Vector3Variable", menuName = "Variables/Vector3", order = 5)]
    public class Vector3Variable : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public Vector3 Value;

        public void SetValue(Vector3 value)
        {
            Value = value;
        }

        public void ApplyChange(Vector3 amount)
        {
            Value += amount;
        }
    }
}
