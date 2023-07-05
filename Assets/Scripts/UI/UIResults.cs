using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;
using UnityEngine.UI;

public class UIResults : MonoBehaviour {

    [SerializeField]
    private ListEnemys enemysToResult;
    [SerializeField]
    private Transform tPatent;
    [SerializeField]
    private GameObject lineObject;

    private void Start()
    {
        StartCoroutine("DisplayResults");
    }

    private IEnumerator DisplayResults()
    {
        List<PlayerMaster> playerMasters = GamePlayMaster.Instance.GetAllPlayer();
        int n = playerMasters.Count;
        for (int i = 0; i < n; i++)
        {
            for (int x = 0; x < enemysToResult.Count; x++)
            {
                PlayerMaster playerMaster = GamePlayMaster.Instance.GetPlayer(i);
                ItemResults item = playerMaster.ContainEnemy(enemysToResult.Value[x]);
                if (item != null)
                {
                    int qnt = item.quantity;
                    int pnts = qnt * enemysToResult.Value[x].enemyScore;
                    GameObject go = Instantiate(lineObject, tPatent);
                    go.GetComponent<UIResultLines>().SetDisplay(item.enemy.spriteIcon, qnt, pnts);
                    go.name = item.enemy.name;
                }
                //GameObject go = Instantiate(lineObject);
                yield return null;
            }
        }        
    }
}
