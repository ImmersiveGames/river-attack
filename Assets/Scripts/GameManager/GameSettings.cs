using MyUtils.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameManagers/GameSettings", order = 1)]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [Header("Layer Settings")]
    public string playerLayer, wallLayer, enemyLayer, collectionLayer;
    [Header("Tag Settings")]
    [SerializeField]
    public string playerTag, wallTag, enemyTag, collectionTag, shootTag;
    [Header("Game Modes")]
    [SerializeField]
    private List<GameModes> gameModes;
    [Header("Game Scenes")]
    [SerializeField]
    private List<GameScenes> gameScenes;
    [SerializeField]
    public ListLevels allLevels;
    [SerializeField]
    public EnemyList allEnemys;
    [SerializeField]
    public GameScenes actualScene { get; private set; }
    [SerializeField]
    public GameScenes previousScene { get; private set; }
    [SerializeField]
    public GameModes gameMode { get; private set; }

    [SerializeField]
    public int numContinue;
    [Header("Game Shop")]
    [SerializeField]
    public ShopProductSkin defaultSkin;
    [SerializeField]
    public ListShopProduct listaShop;

    private void Awake()
    {
        actualScene = gameScenes[0];
        gameMode = gameModes[0];
    }

    public GameModes GetGameModes(string modeName)
    {
        return gameModes.Find(x => x.modeName == modeName);
    }
    public GameModes GetGameModes(int modeId)
    {
        return gameModes.Find(x => x.modeId == modeId);
    }

    public GameScenes GetGameScenes(string sceneName)
    {
        return gameScenes.Find(x => x.sceneName == sceneName);
    }
    public GameScenes GetGameScenes(int modeId)
    {
        return gameScenes.Find(x => x.sceneId == modeId);
    }
    public void ChangeGameScene(GameScenes scene)
    {
        previousScene = actualScene;
        actualScene = scene;
        Play(actualScene);
    }
    public void ChangeGameScene(int sceneID)
    {
        ChangeGameScene(GetGameScenes(sceneID));
    }
    public void ChangeGameScene(string sceneName)
    {
        ChangeGameScene(GetGameScenes(sceneName));
    }

    private void Play(GameScenes scene)
    {
        if (LoadSceneManager.Instance != null)
            LoadSceneManager.Instance.LoadScene(scene.sceneId);
    }

    public void ChangeGameMode(int modeId)
    {
        gameMode = gameModes.Find(x => x.modeId == modeId);
    }
    public void ChangeGameMode(string modeNome)
    {
        gameMode = gameModes.Find(x => x.modeName == modeNome);
    }
}

[System.Serializable]
public class GameModes
{
    public string modeName;
    public int modeId;
}
[System.Serializable]
public class GameScenes
{
    public string sceneName;
    public int sceneId;
}
