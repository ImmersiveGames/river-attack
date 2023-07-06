using UnityEngine;

namespace MyUtils.Variables
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/Int", order = 1)]
    public class IntVariable : Variables
    {

        public void SetValue(int value)
        {
            Value = value;
        }

        public void SetValue(IntVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(int amount)
        {
            Value += amount;
        }

        public void ApplyChange(IntVariable amount)
        {
            Value += amount.Value;
        }
    }
}
