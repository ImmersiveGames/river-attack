using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

public class CollectiblesScore : EnemyScore {

    [SerializeField]
    protected IntReference scoreCollect;
    protected CollectiblesMaster collectiblesMaster;

    protected override void OnEnable()
    {
        base.OnEnable();
        collectiblesMaster.CollectibleEvent += SetCollScore;
    }
    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        collectiblesMaster = GetComponent<CollectiblesMaster>();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        collectiblesMaster.CollectibleEvent -= SetCollScore;
    }

    private void SetCollScore(PlayerMaster playerMaster)
    {
        playerMaster.playerSettings.score.ApplyChange((scoreCollect.Value));
    }
}
