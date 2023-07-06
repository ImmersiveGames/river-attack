using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;

public class PlayerBomb : MonoBehaviour, ICommand
{
    [SerializeField]
    private IntVariable quantity;
    [SerializeField]
    private Vector3 bombOffset;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private GameObject prefabBomb;
    [SerializeField]
    private IntReference idButtonMap;
    private PlayerMaster playerMaster;
    private GamePlayMaster gamePlay;

    public int Quantity { get { return (int)quantity.Value; } }

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventCollectItem += UpdateBombs;
    }

    private void UpdateBombs(Collectibles collectibles)
    {
        if (quantity.Value <= collectibles.maxCollectible.Value)
        {
            quantity.ApplyChange(collectibles.ammontColletables);
        }
    }

    public void AddBomb(int ammont)
    {
        quantity.ApplyChange(ammont);
        playerMaster.CallEventPlayerBomb();
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        gamePlay = GamePlayMaster.Instance;
        prefabBomb.SetActive(false);
    }
    private void Update()
    {
        if (playerMaster.playerSettings.controllerMap.ButtonDown(idButtonMap.Value))
        {
            this.Execute();
        }
    }

    public void Execute()
    {
        if (quantity.Value > 0 && gamePlay.ShouldBePlayingGame && playerMaster.HasPlayerReady)
        {
            quantity.ApplyChange(-1);
            GameObject bomb = Instantiate(prefabBomb);
            bomb.transform.localPosition = transform.localPosition + bombOffset;
            bomb.GetComponent<PlayerBombSet>().OwnerShoot = playerMaster;
            bomb.SetActive(true);
            playerMaster.CallEventPlayerBomb();
            LogBomb(1);
        }
    }

    public void UnExecute()
    {
        throw new System.NotImplementedException();
    }
    private void LogBomb(int bomb)
    {
        GamePlaySettings.Instance.bombSpents += Mathf.Abs(bomb);
        if (GameManager.Instance.firebase.MyFirebaseApp != null && GameManager.Instance.firebase.dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Firebase.Analytics.Parameter[] FireBomb = {
            new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, gamePlay.GetActualLevel().GetName),
            new Firebase.Analytics.Parameter("Milstone", gamePlay.GetActualPath()),
            new Firebase.Analytics.Parameter("DropPos_z", transform.position.z)
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("FireBomb", FireBomb);
        }
    }
    private void OnDisable()
    {
        gamePlay.EventCollectItem -= UpdateBombs;
    }
}
