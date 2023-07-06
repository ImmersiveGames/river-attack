using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.NewLocalization;

[System.Serializable]
public struct ShopProductStock
{
    [SerializeField]
    public bool infinity;
    [SerializeField]
    public int quantity;
    [SerializeField]
    public ShopProduct shopProduct;

    public void RemoveStock(int qnt)
    {
        if (!infinity)
            quantity -= qnt;
        if (quantity < 0)
            quantity = 0;
    }

    public bool AvariableToSelect(Player player)
    {
        if (PlayerAlreadyBuy(player) && shopProduct.ShouldBeConsume(player))
            return true;
        return false;
    }

    public bool AvailableInStock()
    {
        if (infinity || (!infinity && quantity > 0 ))
            return true;
        return false;
    }

    public bool AvariableForBuy(Player player)
    {
        if (AvailableInStock() && HaveMoneyToBuy(player) && (PlayerAlreadyBuy(player) && shopProduct.isConsumable || !PlayerAlreadyBuy(player)))
            return true;
        return false;
    }

    public bool HaveMoneyToBuy(Player player)
    {
        if (player.wealth.Value >= shopProduct.priceItem)
            return true;
        return false;
    }
    public bool PlayerAlreadyBuy(Player player)
    {
        if (player.listProducts.Count > 0 && player.listProducts.Contains(shopProduct))
            return true;
        else
            return false;
    }
}
