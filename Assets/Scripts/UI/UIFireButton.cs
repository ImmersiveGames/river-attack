using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFireButton : UIButtonCommand
{
    private void Start()
    {
        if (gamePlay.ListPlayer.Count > 0)
        {
            Init<PlayerShoot>(playerIndex);
        }
    }
}
