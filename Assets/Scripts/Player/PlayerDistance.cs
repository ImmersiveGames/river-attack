using System.Collections;
using Utils.Variables;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
public class PlayerDistance : MonoBehaviour
{
    [SerializeField]
    private IntVariable pathDistance;
    [SerializeField]
    private FloatReference cadencyDistance;

    private PlayerMaster playerMaster;

    private void Awake()
    {
        pathDistance.SetValue(0);
    }
    private void OnEnable()
    {
        playerMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        if (playerMaster.ShouldPlayerBeReady)
            pathDistance.SetValue((int)(transform.position.y / cadencyDistance));
    }
}
