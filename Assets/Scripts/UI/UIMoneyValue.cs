using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIMoneyValue : MonoBehaviour {

    public int playerIndex;
    private Text myText;
    private GamePlayMaster gamePlay;
    private PlayerMaster playerMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventUICollectable += UpdateUI;
        UpdateUI();
    }

    private void SetInitialReferences()
    {
        myText = GetComponent<Text>();
        gamePlay = GamePlayMaster.Instance;
        playerMaster = gamePlay.GetPlayer(playerIndex);
    }

    private void UpdateUI()
    {
        int subtotal = playerMaster.subWealth;
        int totalcoins = playerMaster.playerSettings.wealth.Value;
        myText.text = (subtotal + totalcoins).ToString("000");
    }

    private void OnDisable()
    {
        gamePlay.EventUICollectable -= UpdateUI;
    }
}
