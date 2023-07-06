using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesMaster : EnemyMaster
{
    public event GeneralEventHandler ShowOnScreen;
    public event EnemyEventHandler CollectibleEvent;
    public Collectibles collectibles { get; set; }

    protected override void OnEnable()
    {
        base.OnEnable();
        gamePlay.EventResetEnemys += DestroyCollectable;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        this.tag = GameSettings.Instance.collectionTag;
        this.gameObject.layer = LayerMask.NameToLayer("Collections");
        collectibles = (Collectibles)enemy;
        if (collectibles.PowerUp != null)
        {
            name += "(" + collectibles.PowerUp.name + ")";
        }
    }

    public void CallCollectibleEvent(PlayerMaster playerMaster)
    {
        if (CollectibleEvent != null)
        {
            CollectibleEvent(playerMaster);
        }
    }
    public void CallShowOnScreen()
    {
        if (ShowOnScreen != null)
        {
            ShowOnScreen();
        }
    }
    private void DestroyCollectable()
    {
        if (collectibles.PowerUp != null)
            Destroy(this.gameObject);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        gamePlay.EventResetEnemys -= DestroyCollectable;
    }
}
