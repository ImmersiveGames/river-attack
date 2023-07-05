using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour {

    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private float timetoDestroy;

    private PlayerMaster playerMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerDestroy += ExploseParticule;
        playerMaster.EventPlayerReload += PlayerSetup;
    }

    private void PlayerSetup()
    {
        Utils.Tools.ToggleChildrens(this.transform, true);
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        // find children with Particles
    }

    private void ExploseParticule()
    {
        Utils.Tools.ToggleChildrens(this.transform, false);
        GameObject go = Instantiate(particlePrefab, this.transform);
        Destroy(go, timetoDestroy);
    }


    private void OnDisable()
    {
        playerMaster.EventPlayerDestroy -= ExploseParticule;
        playerMaster.EventPlayerReload -= PlayerSetup;
    }
}