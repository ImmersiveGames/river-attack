using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class HUBIconMaster : MonoBehaviour
{
    [SerializeField]
    private Levels levels;
    [SerializeField]
    private bool isComplete;
    [SerializeField]
    private AnimationClip animFinish;
    [SerializeField]
    private bool isLocked;
    [SerializeField]
    private AnimationClip animUnlock;


    private Animator animator;
    private HUBMaster hubMaster;
    private HUBSettings hubSettings;

    public Levels MyLevel { get { return levels; } }
    public bool IsLocked { get { return isLocked; } }
    public float GetTimeAnimationFinish { get { return animFinish.length; } }
    public float GetTimeAnimationUnlock { get { return animUnlock.length; } }

    private void Awake()
    {
        if (levels != null)
            transform.name = levels.levelName;
        SetInitialReferences();
        hubMaster.AddIconPosition(MyLevel, this.transform);
        hubMaster.AddIconMaster(MyLevel, this);
    }

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void Start()
    {
        CheckIfLocked();
        CheckIfAlreadyComplete();
    }

    private void SetInitialReferences()
    {
        animator = GetComponent<Animator>();
        hubMaster = HUBMaster.Instance;
        hubSettings = HUBSettings.Instance;
    }

    public void LevelBeat()
    {
        animator.SetInteger("IconState", (int)HUBSettings.IconStatAnimation.Finish);
        CheckIfAlreadyComplete();
    }

    public void UnLockedLevel()
    {
        animator.SetInteger("IconState", (int)HUBSettings.IconStatAnimation.UnLocked);
        CheckIfLocked();
    }

    private void CheckIfAlreadyComplete()
    {
        isComplete = levels.CheckIfCompleate(hubSettings.HUBFinishLevels.Value);
        if (isComplete)
            animator.SetInteger("IconState", (int)HUBSettings.IconStatAnimation.Complete);
    }

    private void CheckIfLocked()
    {
        isLocked = levels.CheckIfLocked(hubSettings.HUBFinishLevels.Value);
        if (isLocked)
            animator.SetInteger("IconState", (int)HUBSettings.IconStatAnimation.Locked);
    }

}
