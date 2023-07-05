using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "ListShopStock", menuName = "Shopping/List/ListShopStock", order = 102)]
    public class ListShopStock : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<ShopProductStock> Value = new List<ShopProductStock>();

        public void SetValue(List<ShopProductStock> value)
        {
            Value = value;
        }

        public void Add(ShopProductStock shopStock)
        {
            Value.Add(shopStock);
        }

        public void Remove(ShopProductStock shopStock)
        {
            Value.Remove(shopStock);
        }

        public void AddToStock(ShopProduct shopProduct, int qnt = 1)
        {

            ShopProductStock inStock = Value.Find(x => x.shopProduct == shopProduct);
            if (inStock.shopProduct != null)
            {
                inStock.quantity += qnt;
            }
        }
        public void RemoveFromStock(ShopProduct shopProduct, int qnt = 1)
        {
            ShopProductStock inStock = Value.Find(x => x.shopProduct == shopProduct);
            if (inStock.shopProduct != null)
            {
                inStock.quantity -= qnt;
            }
        }

        public int Count
        {
            get { return Value.Count; }
        }

        public ShopProductStock FindProductInStock(ShopProduct shopProduct)
        {
            return Value.Find(x => x.shopProduct == shopProduct);
        }

        public bool Contains(ShopProduct shopProduct)
        {
            ShopProductStock inStock = Value.Find(x => x.shopProduct == shopProduct);
            return (inStock.shopProduct != null) ? true : false;
        }

        public bool Contains(ShopProductStock shopStock)
        {
            return Value.Contains(shopStock);
        }
    }
}
