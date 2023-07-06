using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaCollider : EnemyCollider
{
    protected EffectAreaMaster effectAreaMaster;
    private EffectArea effectArea;
    [SerializeField]
    private float timeToAcess;

    private float timer;

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        effectAreaMaster = GetComponent<EffectAreaMaster>();
        effectArea = (EffectArea)effectAreaMaster.enemy;
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.CompareTag(gameSettings.shootTag) && effectArea.canDestruct)
        {
            HitThis(collision);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.root.CompareTag(gameSettings.playerTag) || collision.GetComponentInParent<PlayerMaster>())
        {
            effectAreaMaster.CallEventExitAreaEffect();
        }
    }

    public override void HitThis(Collider collision)
    {
        effectAreaMaster.IsDestroyed = true;
        PlayerMaster playerMaster = WhoHit(collision);
        enemyMaster.CallEventDestroyEnemy(playerMaster);
        ShouldSavePoint();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.root.CompareTag(gameSettings.playerTag) || collision.GetComponentInParent<PlayerMaster>())
        {
            CollectThis(collision);
        }
    }
    public override void CollectThis(Collider collision)
    {
        Player player = collision.GetComponentInParent<PlayerMaster>().playerSettings;
        if (timer <= 0)
        {
            effectArea.EffectAreaStart(player);
            effectAreaMaster.CallEventAreaEffect();
            timer = timeToAcess;
        }
        timer -= Time.deltaTime;
    }
}
