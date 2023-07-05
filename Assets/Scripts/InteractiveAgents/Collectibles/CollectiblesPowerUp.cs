using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesPowerUp : MonoBehaviour {

    [SerializeField]
    private PowerUp powerUp;

    public PowerUp PowerUp { get { return powerUp; } }
}
