using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class HUBIconMaster : MonoBehaviour
{
    [SerializeField]
    private Levels level;
    private HUBMaster hubMaster;
    private HUBSettings hubSettings;
    private GameManager gameManager;
    private Animator animator;

    public Levels GetLevels { get { return level; } }

    private void OnEnable()
    {
        SetInitialReferences();
        hubMaster.EventOnSelectMission += OnSelect;
        hubMaster.EventCompleteMission += BeatMission;
        hubMaster.EventUnlockMission += UnlockMission;
    }

    private void SetInitialReferences()
    {
        gameManager = GameManager.Instance;
        hubMaster = HUBMaster.Instance;
        hubSettings = HUBSettings.Instance;
        animator = GetComponent<Animator>();
        if (level != null)
            level.levelIconPos = transform.position;
    }

    private void Start()
    {
        int length = gameManager.levelsFinish.Count;
        if ((gameManager.actualLevel != null && level == gameManager.actualLevel) ||
            (gameManager.actualLevel == null && length > 0 && gameManager.levelsFinish[length -1]==level))
        {
            hubMaster.PlayerIcon.SetPosition(this.transform);
            StartCoroutine(HUBCameraMaster.Instance.MoveCam(this.transform.position, 0, true));
        }
        OnLoked();
        OnComlplete();
    }

    private void OnSelect(Levels outlevel)
    {
        if (level != null && level == outlevel)
        {
            animator.SetBool("IconSelect", true);
            animator.SetBool("IconUnSelect", false);
            gameManager.actualLevel = outlevel;
        }
        else
        {
            animator.SetBool("IconSelect", false);
            animator.SetBool("IconUnSelect", true);
        }
        if (level != null && level.PreviousLevel.Contains(outlevel))
        {
            hubSettings.levelToUnlock.Add(level);
        }
    }

    private void OnComlplete()
    {
        if (level != null && level.CheckIfCompleate(hubSettings.levelHubComplete))
        {
            animator.SetInteger("IconState", 4);
        }
    }

    private void UnlockMission(Levels outlevel)
    {
        if (level != null && outlevel == level)
        {
            animator.SetInteger("IconState", 2);
        }
    }

    private void BeatMission(Levels outlevel)
    {
        if (level != null && outlevel == level)
        {
            if (!hubSettings.levelHubComplete.Contains(level))
                hubSettings.levelHubComplete.Add(level);
            animator.SetInteger("IconState", 3);
        }
    }
    //Use On Animator
    public void AnimationCompleted()
    {
        HUBManager.isCompleting = false;
        animator.SetInteger("IconState", 4);
    }
    public void AnimationUnloked()
    {
        HUBManager.isUnloked = false;
        animator.SetInteger("IconState", 0);
    }
    private void OnLoked()
    {
        if (level != null && level.CheckIfLocked(hubSettings.levelHubComplete))
        {
            animator.SetInteger("IconState", 1);
        }
    }

    private void OnDisable()
    {
        hubMaster.EventOnSelectMission -= OnSelect;
        hubMaster.EventCompleteMission -= BeatMission;
        hubMaster.EventUnlockMission -= UnlockMission;
    }
}
