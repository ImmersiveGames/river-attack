using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;

public class GamePlayPowerUps : Singleton<GamePlayPowerUps>
{
    //ATENÇÃO NÃO ACEITA EVENTS PORQUE ELE NÃO VAI APRA A MEMORIA CHAMANDO PELO SCRIPTABLE
    public static Player target;
    [Header("RapidFire PowerUp")]
    [Range(0.1f, 2)]
    public float minRapidFire = 0.1f;

    public void SetTarget(Player player)
    {
        target = player;
    }

    public void RapidFireStart(float ammont)
    {
        if (target != null)
        {
            float buff = (target.speedyShoot.Value + ammont < minRapidFire) ? minRapidFire : target.speedyShoot.Value + ammont;
            target.speedyShoot.SetValue(buff);
            GamePlayMaster.Instance.CallEventRapidFire(true);
        }
    }

    public void RapidFireEnd(float ammont)
    {
        if (target != null)
        {
            target.speedyShoot.SetValue(ammont);
            GamePlayMaster.Instance.CallEventRapidFire(false);
        }
    }

    public void RecoveryFuel(int ammont)
    {
        if (target != null)
        {
            if (target.actualHP.Value < target.maxHP)
                target.actualHP.ApplyChange((int)ammont);
        }
    }


    public void GainBomb(int ammont)
    {
        if (target != null)
        {
            target.bombs.ApplyChange(ammont);
        }
    }
    protected override void OnDestroy() { }
}
