using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Test : MonoBehaviour {

    public GameObject explosionEffect;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.H))
        {
            Explode();
        }
	}

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
