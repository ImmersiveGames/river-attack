
public class ShopMaster : Singleton<ShopMaster>
{
    public delegate void GeneralUpdateButtons(Player player, ShopProduct item);
    public GeneralUpdateButtons EventButtonSelect;
    public GeneralUpdateButtons EventButtonBuy;

    public void CallEventButtonSelect(Player player, ShopProduct item)
    {
        if (EventButtonSelect != null)
        {
            EventButtonSelect(player, item);
        }
    }
    public void CallEventButtonBuy(Player player, ShopProduct item)
    {
        if (EventButtonBuy != null)
        {
            EventButtonBuy(player, item);
        }
    }
   // protected override void OnDestroy() { }
}
