using Utils.Variables;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;
    [SerializeField]
    private IntVariable playerLives;
    [SerializeField]
    private GameObject iconLives;
    [SerializeField, ReadOnly]
    private PlayerMaster playerMaster;
    private int lives;

    private GamePlayMaster gamePlay;

    private void OnEnable()
    {
        SetInitialReferences();
        CreateLiveIcon(this.transform, lives);
        playerMaster.EventPlayerDestroy += UpdateUI;
        playerMaster.EventPlayerAddLive += UpdateUI;
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        if (playerLives != null)
        {
            lives = playerLives.Value;
            playerMaster = FindObjectOfType<PlayerMaster>();
        } else if (gamePlay.GetPlayer(playerIndex))
        {
            lives = gamePlay.GetPlayer(playerIndex).playerSettings.lives.Value;
            playerMaster = gamePlay.GetPlayer(playerIndex);
        }  
    }

    private void OnDisable()
    {
        playerMaster.EventPlayerDestroy -= UpdateUI;
        playerMaster.EventPlayerAddLive -= UpdateUI;
    }

    private void UpdateUI()
    {
        lives = playerLives.Value;
        Invoke("SetLivesUI", .1f);
    }

    private void SetLivesUI()
    {
        int i = this.transform.childCount;

        if (i < lives)
        {
            CreateLiveIcon(this.transform, lives - i);
        }
        for (int x = 0; x < i; x++)
        {
            if (x < lives)
                this.transform.GetChild(x).gameObject.SetActive(true);
            else
                this.transform.GetChild(x).gameObject.SetActive(false);
        }
    }
    private void CreateLiveIcon(Transform parent, int quant)
    {
        for (int x = 0; x < quant; x++)
        {
            GameObject icon = Instantiate<GameObject>(iconLives, parent);
            icon.GetComponent<Image>().sprite = playerMaster.playerSettings.playerSkin.spriteItem;
        }
    }
}
