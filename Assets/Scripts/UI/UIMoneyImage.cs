using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]

public class UIMoneyImage : MonoBehaviour {

    [SerializeField]
    private Sprite moneySprite;

    private void OnEnable()
    {
        if (moneySprite != null)
        {
            GetComponent<Image>().sprite = moneySprite;
        }
       
    }
}
