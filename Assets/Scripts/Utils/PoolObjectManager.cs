using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectManager : Singleton<PoolObjectManager> {

    private Dictionary<IHasPool, PoolObjects> objectPools = new Dictionary<IHasPool, PoolObjects>();

    public bool isPersistent;

    public bool CreatePool(IHasPool typePool, GameObject prefab, int initialPoolSize, Transform poolRoot, bool isPersistent = false)
    {
        //Check to see if the pool already exists.
        if (Instance.objectPools.ContainsKey(typePool))
            return false;
        else
        {
            //create a new pool using the properties
            PoolObjects nPool = new PoolObjects(prefab, initialPoolSize, poolRoot, isPersistent);
            Instance.objectPools.Add(typePool, nPool);
            return true;
        }
    }
    public GameObject GetObject(IHasPool objName)
    {
        //Find the right pool and ask it for an object.
        return Instance.objectPools[objName].GetObject();
    }

    protected override void OnDestroy() { }
}
