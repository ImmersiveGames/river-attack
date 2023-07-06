using MyUtils.Variables;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour
{
    [SerializeField]
    private int playerIndex;
    [SerializeField]
    private GameObject iconLives;
    [SerializeField]
    private Sprite defaultSprite;

    private int lives;
    private GamePlayMaster gamePlay;
    private PlayerMaster playerMaster;
    private void Start()
    {
        SetInitialReferences();
        CreateLiveIcon(this.transform, lives);
        playerMaster.EventPlayerReload+= UpdateUI;
        playerMaster.EventPlayerDestroy += UpdateUI;
        playerMaster.EventPlayerAddLive += UpdateUI;
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        playerMaster = gamePlay.GetPlayerMaster(playerIndex);
        lives = (int)playerMaster.playerSettings.lives.Value;  
    }

    private void OnDisable()
    {
        playerMaster.EventPlayerDestroy -= UpdateUI;
        playerMaster.EventPlayerAddLive -= UpdateUI;
        playerMaster.EventPlayerReload -= UpdateUI;
    }

    private void UpdateUI()
    {
        lives = (int)playerMaster.playerSettings.lives.Value;
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
            GameObject icon = Instantiate(iconLives, parent);
            icon.GetComponent<Image>().sprite = playerMaster.playerSettings.playerSkin.hubSprite;
        }
    }
}
