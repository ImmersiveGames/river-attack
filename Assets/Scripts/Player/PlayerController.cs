using System.Collections;
using System.Collections.Generic;
using MyUtils.Variables;
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private IntReference idAxesMap;

    private PlayerMaster playerMaster;
    private Vector3 actualController;
    private Player playerStats;

    private void Start()
    {
        SetinitialReferences();
        playerMaster.EventPlayerReload += ResetMoviment;
    }

    private void SetinitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerStats = playerMaster.playerSettings;
    }
    void FixedUpdate()
    {
        //actualController = (GameManager.Instance.uiController) ? playerStats.controllerMap.InputDirection(Ui_Joystick.InputDirection, idAxesMap) : playerStats.controllerMap.InputDirection(idAxesMap);
        actualController = playerStats.controllerMap.InputDirection(idAxesMap);
        Move();
        //SetMeReady();
    }
    private void Move()
    {
        Vector3 horizontalMovement = Vector3.right * playerStats.myAgility.Value * Time.deltaTime * playerStats.speedHorizontal * actualController.x;
        Vector3 verticalMovement = Vector3.forward * playerStats.mySpeedy.Value * Time.deltaTime;// * actualController.y;
        if (playerMaster.ShouldPlayerBeReady)
        {
            if (actualController.y > 0)
                playerStats.mySpeedy.SetValue(playerStats.speedVertical * playerStats.multiplyVelocityUp);
            if (actualController.y < 0)
                playerStats.mySpeedy.SetValue(playerStats.speedVertical * playerStats.multuplyVelocityDown);
            if (actualController.y == 0)
                playerStats.mySpeedy.SetValue(playerStats.speedVertical);
            transform.position += horizontalMovement;
            transform.position += verticalMovement;
            playerMaster.CallEventControllerMovemant(actualController);
        }
        else
        {
            if (Input.anyKeyDown && !GamePlayMaster.Instance.IsPlayGamePause && !playerMaster.HasPlayerReady)
                GamePlayMaster.Instance.CallEventUnPausePlayGame();
            //playerMaster.SetPlayerReady();
        }
    }
    private void ResetMoviment()
    {
        playerMaster.CallEventControllerMovemant(Vector3.zero);

    }
    private void OnDisable()
    {
        playerMaster.EventPlayerReload -= ResetMoviment;
    }
}
