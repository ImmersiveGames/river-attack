using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkins : ObstacleSkins {

    private EnemyMaster enemyMaster;
    //Usa o Obstacles
    private void Start()
    {
        SetInitialReferences();
        enemyMaster.SetTagLayer(enemySkins, tag, gameObject.layer);
        enemyMaster.CallEventChangeSkin();
    }

    private void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
    }
}
