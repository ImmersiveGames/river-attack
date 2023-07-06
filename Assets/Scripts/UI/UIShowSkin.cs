using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowSkin : MonoBehaviour {

    public Player player;

    private ShopProduct shopProduct;
    private Image img_skin;

    private void OnEnable()
    {
        shopProduct = player.playerSkin;
        img_skin = GetComponent<Image>();
        img_skin.sprite = shopProduct.spriteItem;
    }
    private void Update()
    {
        if (img_skin != player.playerSkin.spriteItem)
        {
            shopProduct = player.playerSkin;
            img_skin.sprite = shopProduct.spriteItem;
        }
    }
}
