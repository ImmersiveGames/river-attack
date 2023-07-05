using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaster : MonoBehaviour {

    [SerializeField]
    private int playerIndex;
    [SerializeField]
    private bool cheasePlayer;
    [SerializeField]
    private PlayerMaster target; // identifica o alvo da camera
    [SerializeField]
    private Vector3 offset; // compensação do alvo para a camera

    public Enemy obstacle;

    private void OnEnable()
    {
        SetInitialReferences();

    }
    private void SetInitialReferences()
    {
        if (target == null && GamePlayMaster.Instance.GetPlayer(playerIndex) != null)
            target = GamePlayMaster.Instance.GetPlayer(playerIndex);
    }
    void LateUpdate()
    {
        if (target != null && target.transform.gameObject.activeSelf && cheasePlayer)
        {
            transform.position = new Vector3(offset.x, target.transform.position.y + offset.y, target.transform.position.z + offset.z);
        }
    }
}
