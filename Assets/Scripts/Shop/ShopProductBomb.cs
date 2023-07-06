using MyUtils.NewLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopBomb", menuName = "Shopping/Bombs", order = 3)]
public class ShopProductBomb : ShopProduct
{

    [SerializeField]
    private int quntity;

    private void OnEnable()
    {
        isConsumable = true;
    }
    public override string GetDescription()
    {
        if (translateDesc != null)
        {
            LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
            return string.Format(translate.Translate(translateDesc, LocalizationTranslate.StringFormat.FirstLetterUp, quntity), quntity);
        }
        return this.desciptionItem;
    }
    public override bool ShouldBeConsume(Player player)
    {
        if (player.bombs.Value + quntity > player.maxBombs)
            return false;
        return true;
    }

    public override void ConsumeProduct(Player player)
    {
        player.bombs.ApplyChange(quntity);
    }
}
