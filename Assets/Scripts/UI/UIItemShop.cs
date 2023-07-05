using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Variables;

public class UIItemShop : MonoBehaviour
{

    [SerializeField]
    private Text itemTitle;
    [SerializeField]
    private Text itemDescription;
    [SerializeField]
    private Text itemPrice;
    [SerializeField]
    private Text itemQuantity;
    [SerializeField]
    private GameObject obQuantity;
    [SerializeField]
    private GameObject obPrice;
    [SerializeField]
    private Image imgItemPrice;
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Button btnBuy;
    [SerializeField]
    private Button btnUnlocked;
    [SerializeField]
    private Button btnSelect;
    [SerializeField]
    public ShopProductStock stockProduct;

    public Button GetBuyButton { get { return btnBuy; } }
    public Button GetSelectButton { get { return btnSelect; } }
    public Button GetUnlockedButton { get { return btnUnlocked; } }

    private ShopMaster shopManager;

    private void OnEnable()
    {
        SetInitialReferences();
        shopManager.EventButtonSelect += UnSelect;
        shopManager.EventButtonBuy += UpdateBuy;
    }

    private void SetInitialReferences()
    {
        shopManager = ShopMaster.Instance;
    } 

    public void SetupDisplay(ShopProductStock stockProduct)
    {
        this.stockProduct = stockProduct;
        ShopProduct shopProduct = stockProduct.shopProduct;
        itemTitle.text = shopProduct.GetName;
        itemDescription.text = shopProduct.GetDescription;
        itemPrice.text = shopProduct.priceItem.ToString();
        itemQuantity.text = "x "+stockProduct.quantity.ToString();
        itemImage.sprite = shopProduct.spriteItem;
        obQuantity.SetActive(!stockProduct.infinity);
        obPrice.SetActive(stockProduct.shopProduct.priceItem > 0);
        btnBuy.gameObject.SetActive(false);
        btnUnlocked.gameObject.SetActive(false);
        btnSelect.gameObject.SetActive(false);
    }

    public void SetupButtons(Player player)
    {
        SetupBuyButton(player);
        SetupSelectButton(player);
    }

    private void SetupBuyButton(Player player)
    {
        btnBuy.gameObject.SetActive(true);
        btnBuy.interactable = false;
        //btnBuy.onClick.RemoveAllListeners();
        //btnBuy.onClick.AddListener(delegate { BuyThisItem(activePlayer, buyButton, productStock); });
        if (stockProduct.AvariableForBuy(player))
            btnBuy.interactable = true;
        else if (stockProduct.PlayerAlreadyBuy(player) && !stockProduct.shopProduct.isConsumable)
            btnBuy.gameObject.SetActive(false);
    }

    private void SetupSelectButton(Player player)
    {
        btnSelect.gameObject.SetActive(true);
        btnSelect.interactable = false;
        //btnSelect.onClick.RemoveAllListeners();
        //selectButton.onClick.AddListener(delegate { SelectThisItem(activePlayer, selectButton, productStock); });
        if (stockProduct.PlayerAlreadyBuy(player) && stockProduct.shopProduct.ShouldBeConsume(player))
            btnSelect.interactable = true;
    }

    private void UnSelect(Player player, ShopProduct item)
    {
        if(item == this.stockProduct.shopProduct)
            btnSelect.interactable = true;
        //SetupSelectButton(player);
    }

    private void UpdateBuy(Player player, ShopProduct item)
    {
        if (item == this.stockProduct.shopProduct)
        {
            btnSelect.interactable = true;
            itemQuantity.text = "x " + stockProduct.quantity.ToString();
        }
        SetupButtons(player);
    }

    private void OnDisable()
    {
        shopManager.EventButtonSelect -= UnSelect;
        shopManager.EventButtonBuy -= UpdateBuy;
    }
}
