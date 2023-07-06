using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIButtonCommand : MonoBehaviour
{

    [SerializeField]
    protected int playerIndex;
    protected ICommand fireCommand;
    protected GamePlayMaster gamePlay;
    protected PlayerMaster playerMaster;

    private void OnEnable()
    {
        gamePlay = GamePlayMaster.Instance;
    }

    protected T Init<T>(int playerindex)
    {
        playerMaster = gamePlay.GetPlayer(playerindex).GetComponent<PlayerMaster>();
        T action = playerMaster.GetComponent<T>();
        fireCommand = (ICommand)action;
        return action;
    }

    public virtual void Fire()
    {
        if (fireCommand != null)
            fireCommand.Execute();
    }
}
