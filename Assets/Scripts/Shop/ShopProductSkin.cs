using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopSkin", menuName = "Shopping/Skin", order = 1)]
[System.Serializable]
public class ShopProductSkin : ShopProduct
{
    //[SerializeField]
    //private RuntimeAnimatorController animatorController;
    [SerializeField]
    private GameObject skinProduct;
    [SerializeField]
    public Sprite hubSprite;

    public GameObject GetSkin { get { return skinProduct; } }
    //public RuntimeAnimatorController GetAnimatorSkin { get { return animatorController; } }

    private void OnEnable()
    {
        isConsumable = false;
    }

    public override bool ShouldBeConsume(Player player)
    {
        if (player.playerSkin == this)
            return false;
        return true;
    }

    public override void ConsumeProduct(Player player)
    {
        player.playerSkin = this;
    }
}
