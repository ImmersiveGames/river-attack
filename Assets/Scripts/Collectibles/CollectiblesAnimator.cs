﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectiblesMaster))]
public class CollectiblesAnimator : EnemyAnimator {

    public string collectTrigger;

    protected CollectiblesMaster collectiblesMaster;

    protected override void OnEnable()
    {
        base.OnEnable();
        collectiblesMaster.CollectibleEvent += CollectAnimation;
    }

    public void CollectAnimation(PlayerMaster playerMaster)
    {
        //implementar animação de coletar
        RemoveAnimation();
    }
    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        collectiblesMaster = GetComponent<CollectiblesMaster>();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        collectiblesMaster.CollectibleEvent -= CollectAnimation;
    }
}
