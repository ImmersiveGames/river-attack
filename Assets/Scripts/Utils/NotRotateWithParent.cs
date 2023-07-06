using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotRotateWithParent : MonoBehaviour {

    private Quaternion rot;

    private void OnEnable()
    {
        rot = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = rot;
    }
}
