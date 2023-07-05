using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinTrail : MonoBehaviour
{
    private PlayerMaster playerMaster;
    private TrailRenderer[] trailRenderer;

    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventControllerMovement += SetTrail;
        trailRenderer = GetComponentsInChildren<TrailRenderer>();
        DesactiveTrails(false);
    }

    private void SetTrail(Vector3 dir)
    {
        if (dir.y > 0)
            DesactiveTrails(true);
        if (dir.y <= 0)
            DesactiveTrails(false);
    }

    private void DesactiveTrails(bool setting)
    {
        for (int i = 0; i < trailRenderer.Length; i++)
        {
            trailRenderer[i].enabled = setting;
        }
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponentInParent<PlayerMaster>();
    }
    private void OnDisable()
    {
        if (playerMaster)
            playerMaster.EventControllerMovement -= SetTrail;
    }
}
