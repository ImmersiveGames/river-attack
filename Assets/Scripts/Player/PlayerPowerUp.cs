using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerPowerUp : MonoBehaviour
{
    public Dictionary<PowerUp, float> activePowerups = new Dictionary<PowerUp, float>();
    private List<PowerUp> keys = new List<PowerUp>();

    private PlayerMaster playerMaster;
    private Collectibles collectibles;
    private GamePlayPowerUps gamePlayPowerUps;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
    }

    private void HandleGlobalPowerUps()
    {
        bool changed = false;

        if (activePowerups.Count > 0)
        {
            foreach (PowerUp powerup in keys)
            {
                if (activePowerups[powerup] > 0)
                {
                    activePowerups[powerup] -= Time.deltaTime;
                }
                else
                {
                    changed = true;
                    activePowerups.Remove(powerup);
                    powerup.PowerUpEnd(GetComponent<PlayerMaster>().playerSettings);
                }
            }
        }

        if (changed)
        {
            keys = new List<PowerUp>(activePowerups.Keys);
        }
    }

    // checks for disabling global powerups
    void Update()
    {
        HandleGlobalPowerUps();
    }

    public void ActivatePowerup(PowerUp powerup)//, Player target)
    {
        //Debug.Log("AQUI: "+ powerup.name);
        ClearActivePowerups(powerup.canAccumulateEffects);
        if (!activePowerups.ContainsKey(powerup))
        {
            powerup.PowerUpStart(playerMaster.playerSettings);
            activePowerups.Add(powerup, powerup.duration.Value);
        }
        else
        {
            if (powerup.canAccumulateDuration)
                activePowerups[powerup] += powerup.duration;
            else
                activePowerups[powerup] = powerup.duration;
        }
        keys = new List<PowerUp>(activePowerups.Keys);
    }

    // Calls the end action of each powerup and clears them from the activePowerups
    public void ClearActivePowerups(bool onlyeffect = false)//(Player target, bool onlyeffect = false)
    {
        foreach (KeyValuePair<PowerUp, float> powerup in activePowerups)
        {
            if (onlyeffect && !powerup.Key.canAccumulateEffects)
                return;
            powerup.Key.PowerUpEnd(playerMaster.playerSettings);
        }
        if (!onlyeffect)
            activePowerups.Clear();
    }
}
