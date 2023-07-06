using UnityEngine;
using UnityEngine.UI;
using MyUtils.Variables;
using System;
using System.Collections;
using System.Threading.Tasks;
using Firebase.RemoteConfig;
using BayatGames.SaveGameFree;

public class ShopMaster : Singleton<ShopMaster>
{
    [SerializeField]
    private Transform contentShop;
    [SerializeField]
    private GameObject objProduct;
    [SerializeField]
    private RectTransform refCenter;
    [SerializeField]
    private ListShopStock productStock;
    [SerializeField]
    private GameObject productFoward, productBackward;

    [Header("Carousel"), SerializeField]
    private bool infinityLooping;
    [SerializeField]
    private float gapBeteenPanels = 0, maxposition = 0;

    private Player activePlayer;
    private ShopCarousel shop;
    private Task task;

    public delegate void GeneralUpdateButtons(Player player, ShopProductStock item);
    public GeneralUpdateButtons EventButtonSelect;
    public GeneralUpdateButtons EventButtonBuy;

    private void OnEnable()
    {
        //SaveGame.DeleteAll();
        SetInitialReferences();
        SetupShop();
    }

    private void Start()
    {
        productFoward.SetActive(true);
        productBackward.SetActive(true);
        if (shop.GetActualProduct == 0 && !infinityLooping)
        {
            productBackward.SetActive(false);
        }
    }

    private void SetInitialReferences()
    {
        activePlayer = GameManager.Instance.GetFirstPlayer(0);
        GameManagerSaves.Instance.LoadPlayer(ref activePlayer);
        //activePlayer.LoadValues();
    }

    private void SetupShop()
    {
        shop = new ShopCarousel(contentShop, objProduct, refCenter);
        shop.maxposition = maxposition;
        shop.gapBeteenPanels = gapBeteenPanels;
        shop.infinityLooping = infinityLooping;
        if (shop != null)
        {
            shop.CreateShopping(productStock.Value);
            for (int i = 0; i < shop.GetProducts.Length; i++)
            {
                UIItemShop item = shop.GetProducts[i].GetComponent<UIItemShop>();
                if (item)
                {
                    item.SetupButtons(activePlayer);
                    item.GetBuyButton.onClick.AddListener(delegate { BuyThisItem(activePlayer, item.myproductStock); });
                    item.GetSelectButton.onClick.AddListener(delegate { SelectThisItem(activePlayer, item.myproductStock); });
                }
            }
        }
    }

    public void BuyThisItem(Player player, ShopProductStock product)
    {
        player.listProducts.Add(product.shopProduct);
        player.wealth.ApplyChange(-product.shopProduct.priceItem);
        product.shopProduct.ConsumeProduct(player);
        product.RemoveStock(1);
        CallEventButtonBuy(player, product);
    }
    private void SelectThisItem(Player player, ShopProductStock productStock)
    {
        productStock.shopProduct.ConsumeProduct(player);
        CallEventButtonSelect(player, productStock);
    }

    private void LateUpdate()
    {
        if (shop != null)
            shop.Update();
    }

    public void ButtonNavegation(int next)
    {
        shop.ButtonNavegation(next);
        if (!infinityLooping)
        {
            if (shop.GetActualProduct == 0)
            {
                productBackward.SetActive(false);
                productFoward.SetActive(true);
            }
            else
            {
                productBackward.SetActive(true);
                productFoward.SetActive(true);
            }
            if (shop.GetProducts.Length - 1 == shop.GetActualProduct)
            {
                productBackward.SetActive(true);
                productFoward.SetActive(false);
            }
        }
        else
        {
            productBackward.SetActive(true);
            productFoward.SetActive(true);
        }

    }

    public void CallEventButtonSelect(Player player, ShopProductStock item)
    {
        if (EventButtonSelect != null)
        {
            EventButtonSelect(player, item);
        }
    }
    public void CallEventButtonBuy(Player player, ShopProductStock item)
    {
        if (EventButtonBuy != null)
        {
            EventButtonBuy(player, item);
        }
    }

    private void OnDisable()
    {
        GameManagerSaves.Instance.SavePlayer(activePlayer);
    }
    protected override void OnDestroy() { }
}