using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class CollectiblesOnScreen : MonoBehaviour {
    private CollectiblesMaster collectiblesMaster;
    private void OnEnable()
    {
        collectiblesMaster = GetComponentInParent<CollectiblesMaster>();
    }

    private void OnBecameVisible()
    {
        collectiblesMaster.CallShowOnScreen();
    }
}
