using MyUtils.NewLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopLife", menuName = "Shopping/Lives", order = 2)]

public class ShopProductLives : ShopProduct
{
    [SerializeField]
    private int quntity;

    public override string GetName()
    {
        if (translateName != null)
        {
            LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
            return string.Format(translate.Translate(translateName, LocalizationTranslate.StringFormat.FirstLetterUp, quntity),quntity);
        }
        return this.name;
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
    private void OnEnable()
    {
        isConsumable = true;
    }
    public override bool ShouldBeConsume(Player player)
    {
        if (player.lives.Value + quntity > player.maxLives)
            return false;
        return true;
    }

    public override void ConsumeProduct(Player player)
    {
        player.lives.ApplyChange(quntity);
    }
}
