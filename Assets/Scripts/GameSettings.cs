using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Utils.Variables;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameManagers/GameSettings", order = 0)]
public class GameSettings : SingletonScriptableObject<GameSettings>
{

    public enum GameScenes { MenuInitial, HUB, Mission, EndGame }
    public enum GameModes { Classic = 0, Mission = 1 }

    [SerializeField]
    public GameScenes actualScene;
    [SerializeField]
    public GameScenes previousScene;
    [SerializeField]
    private GameModes gameMode;
    [SerializeField]
    private GameObject playerPrefab;
    [Header("Options")]
    [SerializeField]
    private IntReference maxContinue;
    [SerializeField]
    private IntVariable continues;

    [Header("Tag Names")]
    [SerializeField]
    public string playerTag = "Player"; // identifica o nome da tag para o player
    [SerializeField]
    public string wallTag = "Wall"; // identifica o nome da tag para as paredes
    [SerializeField]
    public string enemyTag = "Enemy"; // identifica o nome da tag para os inimigos
    [SerializeField]
    public string collectionTag = "Collection"; // identifica o nome da tag dos coletaveis
    [SerializeField]
    public string shootTag = "Shoot"; // identifica o nome da tag dos disparos

    public GameObject GamePlayer { get { return playerPrefab; } }
    public GameModes GameMode { get { return gameMode; } }

    private void OnEnable()
    {
        // Aqui carrega apenas na primeira vez que o jogo carrega ja que só a uma entrada.
        // Carregando a referencia da primeira scene
        ChangeGameScene(SceneManager.GetActiveScene().buildIndex);
        previousScene = actualScene;
        continues.SetValue(maxContinue.Value);
    }

    public void ChangeContinue(int num)
    {
        if (continues.Value + num > maxContinue)
            continues.SetValue(maxContinue);
        else if (continues.Value + num < 0)
            continues.ApplyChange(0);
        else
            continues.ApplyChange(num);
    }

    public bool CheckIfContinue
    {
        get
        {
            if (gameMode == GameModes.Classic)
            {
                if (maxContinue > 0 && continues.Value <= 0)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }
    }

    public void ChangeGameMode(GameModes gamemode)
    {
        ChangeGameMode((int)gamemode);
    }
    public void ChangeGameMode(int gamemode)
    {
        try
        {
            gameMode = (GameModes)gamemode;
        }
        catch (System.Exception)
        {
            gameMode = default(GameModes);
            Debug.LogError("Não existe esse modo de jogo na lista enum GameModes");
            throw;
        }

    }
    private void ChangeGameScene(int sceneID)
    {
        try
        {
            actualScene = (GameScenes)sceneID;
        }
        catch (System.Exception)
        {
            actualScene = default(GameScenes);
            Debug.LogError("Não existe esse scena na lista enum GameScenes");
            throw;
        }
    }

    public void Play(GameScenes scenename)
    {
        Play((int)scenename);
    }
    public void Play(int sceneID)
    {
        previousScene = actualScene;
        ChangeGameScene(sceneID);
        if (LoadSceneManager.Instance != null)
        {
            LoadSceneManager.Instance.LoadScene(sceneID);
        }
    }
}
