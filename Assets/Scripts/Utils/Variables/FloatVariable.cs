using UnityEngine;

namespace MyUtils.Variables
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/Float", order = 2)]
    public class FloatVariable : Variables
    {

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }
    }
}
