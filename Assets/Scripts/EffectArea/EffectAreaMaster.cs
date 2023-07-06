
public class EffectAreaMaster : EnemyMaster {

    public event GeneralEventHandler EventAreaEffect;
    public event GeneralEventHandler EventExitAreaEffect;

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        this.tag = GameSettings.Instance.collectionTag;
    }

    public void CallEventAreaEffect()
    {
        if (EventAreaEffect != null)
        {
            EventAreaEffect();
        }
    }

    public void CallEventExitAreaEffect()
    {
        if (EventExitAreaEffect != null)
        {
            EventExitAreaEffect();
        }
    }
}
