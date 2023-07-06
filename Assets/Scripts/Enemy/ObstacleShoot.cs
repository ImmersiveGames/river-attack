using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleShoot : MonoBehaviour, IShoot
{
    public GameObject prefab;
    public float bulletSpeedy;
    public float cadencyShoot;
    public bool holdShoot;    
    protected Renderer myrenderer;

    protected virtual void OnEnable()
    {
        myrenderer = GetComponentInChildren<Renderer>();       
    }

    public void SetTarget(Transform toTarget){}

    public virtual bool ShouldFire()
    {
        if (GamePlayMaster.Instance.ShouldBePlayingGame && GamePlayMaster.Instance.ShouldPlayReady && gameObject.activeInHierarchy && myrenderer.isVisible && !holdShoot)
            return true;
        return false;
    }

    public void Fire(){}
}
