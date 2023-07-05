using UnityEngine;
using Utils.Variables;
using Utils.NewLocalization;

public abstract class ShopProduct : ScriptableObject
{

    [SerializeField]
    public new string name;
    [SerializeField]
    public LocalizationString translateName;
    [SerializeField]
    public Sprite spriteItem;
    [SerializeField]
    [Multiline]
    public string desciptionItem;
    [SerializeField]
    public LocalizationString translateDesc;
    [SerializeField]
    public int priceItem;
    [SerializeField]
    public bool isConsumable;

    public string GetName
    {
        get
        {
            if (translateName != null)
            {
                LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
                return translate.Translate(translateName, LocalizationTranslate.StringFormat.FirstLetterUp);
            }
            return this.name;
        }
    }
    public string GetDescription
    {
        get
        {
            if (translateDesc != null)
            {
                LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
                return translate.Translate(translateDesc, LocalizationTranslate.StringFormat.FirstLetterUp);
            }
            return this.desciptionItem;
        }
    }

    public virtual bool ShouldBeConsume(Player player) { return false; }
    public virtual void ConsumeProduct(Player player) { Debug.Log("Raiz"); }
}
