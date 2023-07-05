using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

[RequireComponent(typeof(LevelObstacleSpawnMaster))]
public class LevelObstacleSpawnMoviment : ObstacleMoviment
{
    private void OnEnable() { }
    private void SetInitialReferences() { }
    private void Update() { }
    private void OnDisable() { }

    [ContextMenu("LoadPrefab")]
    private void LoadPrefab()
    {
        LevelObstacleSpawnMaster spawnMaster = GetComponent<LevelObstacleSpawnMaster>();
        ObstacleMoviment om = spawnMaster.GetPrefab.GetComponent<ObstacleMoviment>();
        if (om != null)
        {
            moveDirection = om.MoveDirection;
            freeDirection = om.FreeDirection;
            movementSpeed = om.MovementSpeed;
            curveMoviment = om.CurveMoviment;
            canMove = om.canMove;
        }
    }
}
