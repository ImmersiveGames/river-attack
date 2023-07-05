using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "ListEnemys", menuName = "Agents/EnemysList", order = 202)]
    public class ListEnemys : ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public List<Enemy> Value = new List<Enemy>();

        public void SetValue(List<Enemy> value)
        {
            Value = value;
        }

        public void Add(Enemy enemy)
        {
            Value.Add(enemy);
        }

        public void AddRange(List<Enemy> enemy)
        {
            Value.AddRange(enemy);
        }

        public void Remove(Enemy enemy)
        {
            Value.Remove(enemy);
        }

        public int Count
        {
            get { return Value.Count; }
        }

        public int CountHitEnemy(string enemyName)
        {
            int total = 0;
            for (int x = 0; x < Value.Count; x++)
            {
                if (Value[x].name == enemyName)
                    total++;
            }
            return total;
        }

        public int CountHitEnemy(Enemy enemyName)
        {
            int total = 0;
            for (int x = 0; x < Value.Count; x++)
            {
                if (Value[x] == enemyName)
                    total++;
            }
            return total;
        }

        public void RemoveEnemy(Enemy enemyName, int qnt)
        {
            if (CountHitEnemy(enemyName) >= qnt)
            {
                int total = Value.Count;
                for (int x = total - 1; x >= 0; x--)
                {
                    if (Value[x] == enemyName && qnt > 0)
                    {
                        Value.RemoveAt(x);
                        qnt--;
                    }
                }
            }
        }
    }
}
