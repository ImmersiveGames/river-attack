using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemysResults
{
    [SerializeField]
    public Enemy enemy;
    [SerializeField]
    public int quantity;

    public EnemysResults(Enemy enemy, int quantity)
    {
        this.enemy = enemy;
        this.quantity = quantity;
    }

    public int ScoreTotal
    {
        get { return this.quantity * enemy.enemyScore; }
    }
}
