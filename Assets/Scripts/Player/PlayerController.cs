using System.Collections;
using System.Collections.Generic;
using Utils.Variables;
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ControllerMap controllerMap;
    [SerializeField]
    private IntReference idAxesMap;
    [SerializeField]
    private bool uiController;

    #region Variable privte
    private PlayerMaster playerMaster;
    private Vector3 actualController;
    private Player playerStats;
    #endregion
    public ControllerMap ControllerMap { get { return controllerMap; } }

    private void Awake()
    {
        
#if UNITY_IPHONE || UNITY_ANDROID
        uiController = true;
#endif
    }
    private void OnEnable()
    {
        SetinitialReferences();
    }

    private void SetinitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerStats = playerMaster.playerSettings;
    }

    // Update is called once per frame
    void Update()
    {
        actualController = (uiController) ? controllerMap.InputDirection(Ui_Joystick.InputDirection, idAxesMap) : controllerMap.InputDirection(idAxesMap);
        Move();
        SetMeReady();
    }

    private void SetMeReady()
    {
        if (Input.anyKeyDown && GamePlayMaster.Instance.ShouldBePlayingGame && !playerMaster.HasPlayerReady)
            playerMaster.SetPlayerReady(true);        
    }

    private void Move()
    {
        Vector3 horizontalMovement = Vector3.right * playerStats.myAgility.Value * Time.deltaTime * actualController.x;
        Vector3 verticalMovement = Vector3.up * playerStats.mySpeedy.Value * Time.deltaTime;// * actualController.y;
        if (playerMaster.ShouldPlayerBeReady)
        {
            if (actualController.y > 0)
                playerStats.mySpeedy.SetValue(playerStats.speedVertical.Value * playerStats.multiplyVelocityUp);
            if (actualController.y < 0)
                playerStats.mySpeedy.SetValue(playerStats.speedVertical.Value * playerStats.multuplyVelocityDown);
            if (actualController.y == 0)
                playerStats.mySpeedy.SetValue(playerStats.speedVertical);

            transform.position += horizontalMovement;
            transform.position += verticalMovement;
            playerMaster.CallEventControllerMovemant(actualController);
        } 
    }
}
