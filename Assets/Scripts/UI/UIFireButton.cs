using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFireButton : MonoBehaviour {

    [SerializeField]
    private int player;
    private GamePlayMaster gamePlay;
    private ICommand fireCommand;


    private void Start()
    {
        gamePlay = GamePlayMaster.Instance;
        if (gamePlay.GetAllPlayer().Count > 0)
        {
            fireCommand = gamePlay.GetPlayer(player).GetComponent<PlayerShoot>();
        }
    }

    public void Fire()
    {
        if (fireCommand != null)
            fireCommand.Execute(); 
    }

}
