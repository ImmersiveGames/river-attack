using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIMoneyValueShop : MonoBehaviour {
    public int playerIndex;
    private Text myText;
    private Player player;
    private int totalcoins;
    private void OnEnable()
    {
        SetInitialReferences();
        UpdateUI();
    }

    private void SetInitialReferences()
    {
        myText = GetComponent<Text>();
        player = GamePlayMaster.Instance.GetPlayerSettings(0);
    }

    private void UpdateUI()
    {
        totalcoins = (int)player.wealth.Value;
        myText.text = totalcoins.ToString("000");
    }

    private void Update()
    {
        if (totalcoins != player.wealth.Value)
        {
            UpdateUI();
        }
    }
}
