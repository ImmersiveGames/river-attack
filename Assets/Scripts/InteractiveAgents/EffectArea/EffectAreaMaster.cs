
public class EffectAreaMaster : EnemyMaster {

    public event GeneralEventHandler AreaEffectEvent;

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        this.tag = GameSettings.Instance.collectionTag;
    }

    public void CallAreaEffectEvent()
    {
        if (AreaEffectEvent != null)
        {
            AreaEffectEvent();
        }
    }
}
