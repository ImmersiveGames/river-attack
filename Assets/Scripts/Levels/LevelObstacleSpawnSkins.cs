﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelObstacleSpawnMaster))]
public class LevelObstacleSpawnSkins : ObstacleSkins
{
    private void OnEnable(){}
    private void LoadDefaultSkin() { }
    [ContextMenu("LoadPrefab")]
    private void LoadPrefab()
    {
        LevelObstacleSpawnMaster spawnMaster = GetComponent<LevelObstacleSpawnMaster>();
        ObstacleSkins oskin = spawnMaster.GetPrefab.GetComponent<ObstacleSkins>();
        if (oskin != null)
        {
            this.indexStartSkin = oskin.IndexSkin;
            this.randomSkin = oskin.RandomSkin;
            this.enemySkins = oskin.EnemySkins;
        }
    }
}
