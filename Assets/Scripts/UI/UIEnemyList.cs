using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyList : MonoBehaviour
{
    [SerializeField]
    private Text enemyName;
    [SerializeField]
    private Text enemyKill;
    [SerializeField]
    private Image enemyIcon;

    public Enemy MyEnemy { get; private set; }

    public void SetDisplay(Enemy myenemy, int kill)
    {
        MyEnemy = myenemy;
        UpdateDisplay(kill);
    }

    public void UpdateDisplay(int kill)
    {
        enemyIcon.sprite = MyEnemy.spriteIcon;
        enemyName.text = MyEnemy.name;
        enemyKill.text = kill.ToString("000000");
    }
}
