using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBMaster : Singleton<HUBMaster>
{
    [SerializeField]
    private HUBSettings hubSettings;
    public bool hubPause;
    [SerializeField]
    private LayerMask iconLayers;
    [SerializeField]
    public float timeMoveCam = 1;
    [SerializeField]
    private HUBIconPlayer playerIcon;
    [SerializeField]
    private Transform startPlayerPosition;

    public HUBIconPlayer PlayerIcon { get { return playerIcon; } }
    private GameManager gameManager;
    private GameManagerSaves gameSave;

    public delegate void LevelHUBEventHandler(Levels level);
    public event LevelHUBEventHandler EventOnSelectMission;
    public event LevelHUBEventHandler EventCompleteMission;
    public event LevelHUBEventHandler EventUnlockMission;

    public delegate void GeneralHUBEventHandler();
    public event GeneralHUBEventHandler EventOnUnselectMission;

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        hubSettings = HUBSettings.Instance;
        gameSave = GameManagerSaves.Instance;
        playerIcon.SetPosition(startPlayerPosition);
    }

    private void Update()
    {
        CheckIconFocus();
    }

    private void CheckIconFocus()
    {
        if (Input.GetMouseButtonUp(0) && !hubPause)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, iconLayers);
            if (hit.collider != null)
            {
                HUBIconMaster iconmaster = hit.collider.GetComponent<HUBIconMaster>();
                if (iconmaster != null)
                {
                    hubPause = true;
                    StartCoroutine(HUBCameraMaster.Instance.MoveCam(hit.point, timeMoveCam, true));
                    playerIcon.GoIcone(hit.point);
                    if (iconmaster.GetLevels)
                    {
                        gameManager.actualLevel = iconmaster.GetLevels;
                        hubSettings.levelToUnlock.Clear();
                        CallEventOnSelectMission(gameManager.actualLevel);
                    }
                    else
                    {
                        gameManager.actualLevel = null;
                        CallEventOnUnselectMission();
                    }
                }
            }
        }
    }

    public void CallEventOnSelectMission(Levels level)
    {
        if (EventOnSelectMission != null)
        {
            EventOnSelectMission(level);
        }
    }

    public void CallEventOnUnselectMission()
    {
        if (EventOnUnselectMission != null)
        {
            EventOnUnselectMission();
        }
    }

    public void CallEventCompleteMission(Levels level)
    {
        if (EventCompleteMission != null)
        {
            EventCompleteMission(level);
        }
    }

    public void CallEventUnlockMission(Levels level)
    {
        if (EventUnlockMission != null)
        {
            EventUnlockMission(level);
        }
    }

    private void OnDisable()
    {
        gameSave.SaveLevelComplete("HubLevel", hubSettings.levelHubComplete);
        gameSave.SaveLevelComplete("levelComplete", hubSettings.levelHubComplete);
    }

    protected override void OnDestroy() { }
}