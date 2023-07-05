using UnityEngine;
using UnityEngine.UI;
using Utils.Variables;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform contentProduct;
    [SerializeField]
    private GameObject panelProduct;
    [SerializeField]
    private RectTransform panelCenter;
    
    [SerializeField]
    public ListShopStock productStock;

    private ShopCarousel shop;

    private Player activePlayer;

    private void OnEnable()
    {
        SetInitialreferences();
    }

    private void SetInitialreferences()
    {
        activePlayer = GameManager.Instance.Players[0];
        SetupButtons();
    }

    private void Start()
    {
        shop = new ShopCarousel(contentProduct, panelProduct, panelCenter);
        SetupButtons();
    }

    private void SetupButtons()
    {
        if (shop != null)
        {
            shop.CreateShopping(productStock.Value);
            for (int i = 0; i < shop.GetProducts.Length; i++)
            {
                UIItemShop item = shop.GetProducts[i].GetComponent<UIItemShop>();
                item.SetupButtons(activePlayer);
                item.GetBuyButton.onClick.AddListener(delegate { BuyThisItem(activePlayer, item.GetBuyButton, item.stockProduct); });
                item.GetSelectButton.onClick.AddListener(delegate { SelectThisItem(activePlayer, item.GetSelectButton, item.stockProduct); });
            }
        }
        
    }  

    private void BuyThisItem(Player player, Button buyButton, ShopProductStock productStock)
    {
        player.GetShopList.Add(productStock.shopProduct);
        player.wealth.ApplyChange(-productStock.shopProduct.priceItem);
        productStock.RemoveStock(1);
        buyButton.interactable = false;
        buyButton.gameObject.SetActive(false);
        ShopMaster.Instance.CallEventButtonBuy(player, productStock.shopProduct);
        //item.GetSelectButton.interactable = true;
    }

    private void SelectThisItem(Player player, Button selectButton, ShopProductStock productStock)
    {
        ShopProduct olditem = player.playerSkin;
        productStock.shopProduct.ConsumeProduct(player);
        selectButton.interactable = false;
        ShopMaster.Instance.CallEventButtonSelect(player, olditem);
    }
    public void ButtonNavegation(int next)
    {
        shop.ButtonNavegation(next);
    }

    private void Update()
    {
        shop.Update();
    }
}
