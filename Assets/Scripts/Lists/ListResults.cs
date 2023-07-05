using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "ListResults", menuName = "Variables/Lists/ListResults", order = 2)]
    public class ListResults : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<ItemResults> Value = new List<ItemResults>();

        public void SetValue(List<ItemResults> value)
        {
            Value = value;
        }

        public void Add(ItemResults itemResults)
        {
            Debug.Log("Item result: "+ itemResults.enemy.name);
            Value.Add(itemResults);
        }

        public void Add(List<ItemResults> itemResults)
        {
            Value.AddRange(itemResults);
        }

        public void Remove(ItemResults itemResults)
        {
            Value.Remove(itemResults);
        }

        public int Count
        {
            get { return Value.Count; }
        }
        public bool Contains(ItemResults itemResults)
        {
            return Value.Contains(itemResults);
        }

        public void AddRange(List<ItemResults> enemys)
        {
            int n = enemys.Count;
            for (int i = 0; i < n; i++)
            {
                AddToResults(enemys[i].enemy, enemys[i].quantity);
            }
        }

        public void AddToResults(Enemy enemy, int qnt = 1)
        {
            ItemResults itemResults = Value.Find(x => x.enemy == enemy);
            if (itemResults != null)
            {
                if(enemy.GetType() == typeof(Collectibles))
                {
                    Collectibles collectibles = (Collectibles)enemy;
                    if (itemResults.quantity + qnt < collectibles.maxCollectible)
                    {
                        itemResults.quantity += qnt;
                    }
                }
                else
                itemResults.quantity += qnt;
            }
            else
            {
                itemResults = new ItemResults(enemy, qnt);
                Value.Add(itemResults);
            }
        }
        public void RemoveFromResults(Enemy enemy, int qnt = 1)
        {
            ItemResults itemResults = Value.Find(x => x.enemy == enemy);
            if (itemResults != null)
            {
                itemResults.quantity -= qnt;
                if (itemResults.quantity < 0)
                    itemResults.quantity = 0;
            }
        }

        public int CountEnemys(Enemy enemy)
        {
            ItemResults itemResults = Value.Find(x => x.enemy == enemy);
            if (itemResults != null)
                return itemResults.quantity;
            else
                return 0;
        }
        public ItemResults FindProductInStock(Enemy enemy)
        {
            return Value.Find(x => x.enemy == enemy);
        }

        public bool Contains(Enemy enemy)
        {
            ItemResults itemResults = Value.Find(x => x.enemy == enemy);
            return (itemResults != null) ? true : false;
        }
    }
}
