using UnityEngine;
namespace Utils.Variables
{
    [System.Serializable]
    public class ItemResults
    {
        [SerializeField]
        public Enemy enemy;
        [SerializeField]
        public int quantity;

        public ItemResults(Enemy enemy, int quantity)
        {
            this.enemy = enemy;
            this.quantity = quantity;
        }
    }
}
