using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "ListItemDrop", menuName = "Variables/Lists/Dropitens", order = 1)]
    public class ListDropItens : ScriptableObject
    {

#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<ItensDrop> Value = new List<ItensDrop>();

        public void SetValue(List<ItensDrop> value)
        {
            Value = value;
        }

        public void Add(ItensDrop itens)
        {
            Value.Add(itens);
        }

        public void Remove(ItensDrop itens)
        {
            Value.Remove(itens);
        }

        public int Count
        {
            get { return Value.Count; }
        }

        public ItensDrop TakeRandomItem(float sortRange)
        {
            int n = Value.Count;
            if (n > 0)
            {
                if (n == 1) return Value[0];
                float totalw = 0;
                for (int i = 0; i < n; i++)
                {
                    float chance = (Value[i].sortChance.minValue != 0) ? Random.Range(Value[i].sortChance.minValue, Value[i].sortChance.maxValue) : Value[i].sortChance.maxValue;
                    Value[i].SetRealSort(chance);
                    totalw += chance;
                }
                for (int i = 0; i < n; i++)
                {
                    Value[i].SetRealChance(Value[i].realsort, totalw);
                }
                Value.OrderByDescending(x => x.realchance);
                sortRange = Random.value;
                float check = 0;
                for (int i = 0; i < n; i++)
                {
                    if (sortRange <= check + Value[i].realchance)
                        return Value[i];
                    check += Value[i].realchance;
                }
                return Value[0];
            }
            return new ItensDrop();
        }
    }

    [System.Serializable]
    public struct ItensDrop
    {
        [SerializeField]
        public GameObject item;
        [SerializeField]
        [MinMaxRange(0, 1)]
        public FloatRanged sortChance;
        [SerializeField]
        [MinMaxRange(1, 100)]
        public FloatRanged itemQuantity;

        public float realchance { get; private set; }
        public float realsort { get; private set; }

        public void SetRealChance(float weight, float totalWeight)
        {
            realchance = (weight / totalWeight) * 100;
        }

        public void SetRealSort(float newValor)
        {
            realsort = newValor;
        }
    }
}
