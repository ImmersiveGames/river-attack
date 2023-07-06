using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIMoneyValue : MonoBehaviour {

    public int playerIndex;
    private Text myText;
    private int total;
    private GamePlayMaster gamePlay;
    private PlayerMaster playerMaster;
    private Animator animator;

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
        playerMaster = gamePlay.GetPlayerMaster(playerIndex);
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
    }

    private void UpdateUI()
    {
        
        int subtotal = playerMaster.subWealth;
        int mywealth = (int)playerMaster.playerSettings.wealth.Value;
        if (animator != null && total != (subtotal + mywealth)) animator.SetTrigger("Bounce");
        total = subtotal + mywealth;
        myText.text = total.ToString("0000");
    }

    private void OnDisable()
    {
        gamePlay.EventUICollectable -= UpdateUI;
    }
}
