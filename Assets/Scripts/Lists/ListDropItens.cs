using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace MyUtils.Variables
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
            //Debug.Log("ItensDrop: OK");
            int n = Value.Count;
            if (n > 0)
            {
                //Debug.Log("ItensDrop: "+ n);
                if (n == 1) return Value[0];
                float totalw = Value.Sum(x=> x.SortChance);
                Value.OrderByDescending(x => x.realchance);
                float realnum = 0;
                sortRange = Random.value;
                for (int i = 0; i < n; i++)
                {
                    Value[i].SetRealChance(totalw);
                    realnum += Value[i].realsort;
                    //Debug.Log("Real Chance: " + Value[i].realsort);
                    if (realnum >= sortRange) return Value[i];                    
                }
                //Debug.Log("Real Numero : " + realnum);
            }
            return new ItensDrop();
        }
    }

    [System.Serializable]
    public class ItensDrop
    {
        [SerializeField]
        public GameObject item;
        [SerializeField]
        [MinMaxRange(0, 1)]
        private FloatRanged sortChance;
        [SerializeField]
        [Range(1, 100)]
        public int itemQuantity;
        public float realchance;

        public float SortChance
        {
            get
            {
                realchance = (sortChance.minValue != 0) ? Random.Range(sortChance.minValue, sortChance.maxValue) : sortChance.maxValue;
                return realchance;
            }
        }


        //public float realchance { get; private set; }
        public float realsort { get;  set; }

        public void SetRealChance(float totalWeight)
        {
            realsort = realchance / totalWeight;
        }
    }
}
