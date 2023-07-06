using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjects
{

    private List<GameObject> pooledObjects;

    private GameObject pooledObj;

    //private int initialPoolSize;

    private Transform myroot;
    private Transform target;

    public PoolObjects(GameObject prefab, int initialPoolSize, Transform myroot, bool persistent = false)
    {
        pooledObjects = new List<GameObject>();
        if (this.myroot == null)
        {
            this.myroot = new GameObject("Pool(" + myroot.root.name + ")").transform;
            //this.myroot.SetParent(myroot);
            //this.myroot.SetAsLastSibling();
            this.myroot.transform.localPosition = Vector3.zero;
            this.myroot.transform.localRotation = Quaternion.identity;

        }
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateObject(prefab, this.myroot);
        }
        if (persistent)
            GameObject.DontDestroyOnLoad(myroot);
        this.pooledObj = prefab;
        //this.initialPoolSize = initialPoolSize;
    }

    private GameObject CreateObject(GameObject prefab, Transform poolroot)
    {
        GameObject nObj = GameObject.Instantiate(prefab, this.myroot.transform.position, this.myroot.transform.rotation, poolroot);
        nObj.SetActive(false);
        pooledObjects.Add(nObj);
        return nObj;
    }

    internal GameObject GetObject()
    {
        int lenght = pooledObjects.Count;
        for (int i = 0; i < lenght; i++)
        {
            //look for the first one that is inactive.
            if (pooledObjects[i].activeSelf == false)
            {
                ResetPosition(pooledObjects[i]);
                //set the object to active.
                pooledObjects[i].SetActive(true);
                //return the object we found.
                //pooledObjects[i].transform.parent = null;
                return pooledObjects[i];
            }
        }
        return CreateObject(pooledObj, this.myroot);
    }

    private void ResetPosition(GameObject gameObject)
    {
        gameObject.transform.localPosition = new Vector3(this.myroot.transform.localPosition.x, this.myroot.transform.localPosition.y, this.myroot.transform.localPosition.z);
        gameObject.transform.localRotation = new Quaternion(this.myroot.transform.localRotation.x, this.myroot.transform.localRotation.y, this.myroot.transform.localRotation.z, this.myroot.transform.localRotation.w);
    }
}
