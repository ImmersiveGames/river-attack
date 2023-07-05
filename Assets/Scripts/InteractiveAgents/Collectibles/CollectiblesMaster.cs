using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesMaster : EnemyMaster {

    public event EnemyEventHandler CollectibleEvent;
    public Collectibles collectibles { get; set; }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        this.tag = GameSettings.Instance.collectionTag;
        collectibles = (Collectibles)enemy;
        if(collectibles.PowerUp != null)
        {
            name += "("+ collectibles.PowerUp.name+ ")";
        }
    }

    public void CallCollectibleEvent(PlayerMaster playerMaster)
    {
        if (CollectibleEvent != null)
        {
            CollectibleEvent(playerMaster);
        }
    }
}
