using UnityEngine;
using UnityEngine.Advertisements;

public class GamePlayGameOver : MonoBehaviour {

    [SerializeField]
    private GameObject panelGameOver;
    
    private GamePlayMaster gamePlay;
    private LoadSceneManager loadScene;
    

    private void Awake()
    {
        if(panelGameOver)
        panelGameOver.SetActive(false);
    }
    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventGameOver += GameOver;
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        loadScene = LoadSceneManager.Instance;
    }

    private void GameOver()
    {
        if (panelGameOver)
            panelGameOver.SetActive(true);
        else
            LoadScene();
    }

    public void ExitButton()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        if (loadScene != null && GameSettings.Instance.GameMode == GameSettings.GameModes.Mission)
            loadScene.LoadScene((int)GameSettings.GameScenes.HUB);
        else
            loadScene.LoadScene((int)GameSettings.GameScenes.MenuInitial);
    }

    private void OnDisable()
    {
        gamePlay.EventGameOver -= GameOver;
    }
}
