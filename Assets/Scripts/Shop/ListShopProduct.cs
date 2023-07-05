using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "ListShopProducts", menuName = "Shopping/List/ListProduct", order = 101)]
    public class ListShopProduct : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<ShopProduct> Value = new List<ShopProduct>();

        public void SetValue(List<ShopProduct> value)
        {
            Value = value;
        }

        public void Add(ShopProduct shopProduct)
        {
            Value.Add(shopProduct);
        }

        public void Remove(ShopProduct shopProduct)
        {
            Value.Remove(shopProduct);
        }

        public int Count
        {
            get { return Value.Count; }
        }

        public bool Contains(ShopProduct shopProduct)
        {
            return Value.Contains(shopProduct);
        }
    }
}
