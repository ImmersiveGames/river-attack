using System.Collections.Generic;
using UnityEngine;

public class ShopCarousel
{

    private Transform contentProduct;
    private GameObject panelProduct;
    private RectTransform panelCenter;

    public enum CarouselDirection { Horizontal, Vertical }
    [Header("Carousel")]
    public float gapBeteenPanels = 20;
    public bool infinityLooping = false;
    public float maxposition = 1500;
    public CarouselDirection carouselDirection = CarouselDirection.Horizontal;

    public GameObject[] GetProducts{ get { return products; } }

    #region Internal varables
    private GameObject[] products;
    private float[] distance;
    private float[] distanceReposition;
    private int productDistance;
    private int minProdctIndex;
    private bool dragging = false;
    private bool targetNearestButton = true;
    #endregion

    public ShopCarousel(Transform contentProduct, GameObject panelProduct, RectTransform panelCenter)
    {
        this.contentProduct = contentProduct;
        this.panelProduct = panelProduct;
        this.panelCenter = panelCenter;
    }
    public void CreateShopping(List<ShopProductStock> productStocks)
    {
        ClearShopping(productStocks.Count);
        distance = new float[productStocks.Count];
        distanceReposition = new float[productStocks.Count];
        Vector2 distanceGap = Vector2.zero;
        for (int i = 0; i < productStocks.Count; i++)
        {
            GameObject item = GameObject.Instantiate(panelProduct, contentProduct);
            item.GetComponent<UIItemShop>().SetupDisplay(productStocks[i]);
            item.transform.localPosition += (Vector3)distanceGap;
            distanceGap += VectorCarouselDirection(item.GetComponent<RectTransform>().rect.size, gapBeteenPanels);
            products[i] = item;
        }
        // pegas a distancia entre botões
        productDistance = (int)Mathf.Abs(VectorDistance(products[1].GetComponent<RectTransform>().anchoredPosition, products[0].GetComponent<RectTransform>().anchoredPosition));
    }

    public void Update()
    {
        for (int i = 0; i < products.Length; i++)
        {
            distanceReposition[i] = VectorDistance(panelCenter.position, products[i].GetComponent<RectTransform>().position);
            distance[i] = Mathf.Abs(distanceReposition[i]);
            float curX = products[i].GetComponent<RectTransform>().anchoredPosition.x;
            float curY = products[i].GetComponent<RectTransform>().anchoredPosition.y;
            if (infinityLooping && distanceReposition[i] > maxposition)
            {
                if (carouselDirection == CarouselDirection.Horizontal)
                    curX += products.Length * productDistance;
                if (carouselDirection == CarouselDirection.Vertical)
                    curY += products.Length * productDistance;
            }

            if (infinityLooping && distanceReposition[i] < -maxposition)
            {
                if (carouselDirection == CarouselDirection.Horizontal)
                    curX -= products.Length * productDistance;
                if (carouselDirection == CarouselDirection.Vertical)
                    curY -= products.Length * productDistance;
            }
            products[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(curX, curY);
        }
        if (targetNearestButton)
        {
            float minDistance = Mathf.Min(distance);

            for (int a = 0; a < products.Length; a++)
            {
                if (minDistance == distance[a])
                    minProdctIndex = a;
            }
        }

        if (!dragging)
        {
            if (carouselDirection == CarouselDirection.Horizontal)
                LerpToProduct(-products[minProdctIndex].GetComponent<RectTransform>().anchoredPosition.x);
            else
                LerpToProduct(-products[minProdctIndex].GetComponent<RectTransform>().anchoredPosition.y);
        }
    }

    public void ButtonNavegation(int next)
    {
        targetNearestButton = false;
        minProdctIndex += next;
        if (!infinityLooping && minProdctIndex + next >= products.Length)
            minProdctIndex = products.Length - 1;
        if (!infinityLooping && minProdctIndex + next < 0)
            minProdctIndex = 0;
    }

    private void LerpToProduct(float pos)
    {
        float newX = 0, newY = 0;
        if (carouselDirection == CarouselDirection.Horizontal)
            newX = Mathf.Lerp(contentProduct.GetComponent<RectTransform>().anchoredPosition.x, pos, Time.deltaTime * 5f);
        if (carouselDirection == CarouselDirection.Vertical)
            newY = Mathf.Lerp(contentProduct.GetComponent<RectTransform>().anchoredPosition.y, pos, Time.deltaTime * 5f);

        contentProduct.GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, newY);
    }

    private Vector2 VectorCarouselDirection(Vector2 increment, float distance)
    {
        switch (carouselDirection)
        {
            case CarouselDirection.Horizontal:
                return new Vector2(increment.x + distance, 0);
            case CarouselDirection.Vertical:
                return new Vector2(0, increment.y + distance);
            default:
                return increment;
        }
    }

    private float VectorDistance(Vector3 ancor1, Vector3 ancor0)
    {
        switch (carouselDirection)
        {
            case CarouselDirection.Horizontal:
                return ancor1.x - ancor0.x;
            case CarouselDirection.Vertical:
                return ancor1.y - ancor0.y;
            default:
                return 0;
        }
    }

    private void ClearShopping(int count)
    {
        for (int i = 0; i < contentProduct.childCount; i++)
        {
            GameObject.Destroy(contentProduct.GetChild(i).gameObject);
        }
        products = new GameObject[count];
    }

    public void Drag(bool drag)
    {
        targetNearestButton = true;
        dragging = drag;
    }
}