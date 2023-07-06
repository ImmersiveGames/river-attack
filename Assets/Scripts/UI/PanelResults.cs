using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelResults : MonoBehaviour {

    [SerializeField]
    private EnemyList enemysToResult;
    [SerializeField]
    private Transform tPatent;
    [SerializeField]
    private GameObject lineObject;

    private GamePlaySettings playSettings;

    private void OnEnable()
    {
        playSettings = GamePlaySettings.Instance;
        EnemyKillsResults(playSettings.HitEnemys);
    }

    private void EnemyKillsResults(List<EnemysResults> hitEnemys)
    {
        ClearPanel(tPatent);
        foreach (Enemy enemy in enemysToResult.Value)
        {
            EnemysResults item = ContainEnemy(enemy, hitEnemys);
            int qnt = (item != null) ? item.quantity :0;
            //int pnts = (item != null)? qnt * enemy.enemyScore: enemy.enemyScore;
            Sprite sprite = (item != null) ? item.enemy.spriteIcon : enemy.spriteIcon;
            string ename = (item != null) ?item.enemy.GetName:enemy.GetName;
            GameObject go = Instantiate(lineObject, tPatent);
            go.GetComponent<UIResultLines>().SetDisplay(sprite, qnt, ename);
            go.name = enemy.name;
        }
    }
    public EnemysResults ContainEnemy(Enemy enemy, List<EnemysResults> list)
    {
        return list.Find(x => x.enemy == enemy);
    }

    private void ClearPanel(Transform t)
    {
        if (tPatent.childCount > 0)
        {
            MyUtils.Tools.TransformClear(t);
        }
    }
}
