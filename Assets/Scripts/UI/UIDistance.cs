using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using MyUtils.Variables;
using UnityEngine;

public class UIDistance : MonoBehaviour {
    [SerializeField]
    private int indexPlayer;
    [SerializeField]
    private Text textDistance;

    private int m_distance;
    private PlayerMaster playerMaster;
    private PlayerDistance playerDistance;
    private GamePlayMaster gamePlay;

    private void OnEnable()
    {
        m_distance = 0;
        gamePlay = GamePlayMaster.Instance;
        playerMaster = gamePlay.GetPlayerMaster(indexPlayer);
        playerDistance = playerMaster.GetComponent<PlayerDistance>();
        if (GameSettings.Instance.gameMode.modeId == GameSettings.Instance.GetGameModes(0).modeId)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
        textDistance.text = m_distance.ToString();
    }

    private void LateUpdate()
    {
        if (m_distance != playerDistance.pathDistance)
        {
            m_distance = playerDistance.pathDistance;
            textDistance.text = m_distance.ToString();
        }
    }
}
