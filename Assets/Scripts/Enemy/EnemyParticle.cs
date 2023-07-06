using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
public class EnemyParticle : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private float timetoDestroy;
    private EnemyMaster enemyMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventDestroyEnemy += ExploseParticule;
    }

    private void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        // find children with Particles
    }

    private void ExploseParticule()
    {
        MyUtils.Tools.ToggleChildrens(this.transform, false);
        GameObject go = Instantiate(particlePrefab, transform);
        Destroy(go, timetoDestroy);
    }

    private void OnDisable()
    {
        enemyMaster.EventDestroyEnemy -= ExploseParticule;
    }
}
