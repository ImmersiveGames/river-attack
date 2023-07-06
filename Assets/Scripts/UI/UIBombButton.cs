using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBombButton : UIButtonCommand
{
    private PlayerBomb playerBomb;
    [SerializeField]
    private Text textNBomb;
    private int quntity;

    private Animator animator;

    private void Start()
    {
        if (gamePlay.ListPlayer.Count > 0)
        {
            animator = textNBomb.GetComponent<Animator>();
            playerBomb = Init<PlayerBomb>(playerIndex);
            UpdateUI();
        }
        playerMaster.EventPlayerBomb += UpdateUI;
        gamePlay.EventUICollectable += UpdateUI;
    }

    public override void Fire()
    {
        GetComponent<Button>().interactable = false;
        base.Fire();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (animator != null && quntity != playerBomb.Quantity)
        {
            animator.SetTrigger("Bounce");
        }
        quntity = playerBomb.Quantity;
        
        textNBomb.text = quntity.ToString("0");
        if (quntity <= 0)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }

    private void OnDisable()
    {
        playerMaster.EventPlayerBomb -= UpdateUI;
        gamePlay.EventUICollectable -= UpdateUI;
    }
}
