using System.Collections.Generic;
using BayatGames.SaveGameFree;
using MyUtils.NewLocalization;
using UnityEngine;

public class GameManagerSaves : Singleton<GameManagerSaves>
{
    LocalizationSettings language;
    [SerializeField]
    private bool resetSaves;
    private GameSettings gameSettings;

    private void OnEnable()
    {
        language = LocalizationSettings.Instance;
        language.EventTranslate += SaveLanguages;
        if (resetSaves) SaveGameClear();
    }

    private void Start()
    {
        gameSettings = GameSettings.Instance;
    }

    public void SaveGamePlay(GamePlaySettings gameplay)
    {
        SaveGame.Save("pathDistance", gameplay.pathDistance);
        SaveGame.Save("livesSpents", gameplay.livesSpents);
        SaveGame.Save("fuelSpents", gameplay.fuelSpents);
        SaveGame.Save("bombSpents", gameplay.bombSpents);
        SaveGame.Save("totalScore", gameplay.totalScore);
        SaveGame.Save("totalTime", gameplay.totalTime);

        int length = gameplay.HitEnemys.Count;
        Dictionary<int, int> hitEnemys = new Dictionary<int, int>();
        for (int i = 0; i < length; i++)
        {
            hitEnemys.Add(gameplay.HitEnemys[i].enemy.GetInstanceID(), gameplay.HitEnemys[i].quantity);
        }
        SaveGame.Save("HitEnemys_01", hitEnemys);

    }

    public void LoadGamePlay(GamePlaySettings gameplay)
    {
        if (SaveGame.Exists("pathDistance"))
            gameplay.pathDistance = SaveGame.Load("pathDistance", gameplay.pathDistance);
        if (SaveGame.Exists("livesSpents"))
            gameplay.livesSpents = SaveGame.Load("livesSpents", gameplay.livesSpents);
        if (SaveGame.Exists("fuelSpents"))
            gameplay.fuelSpents = SaveGame.Load("fuelSpents", gameplay.fuelSpents);
        if (SaveGame.Exists("bombSpents"))
            gameplay.bombSpents = SaveGame.Load("bombSpents", gameplay.bombSpents);
        if (SaveGame.Exists("totalScore"))
            gameplay.totalScore = SaveGame.Load("totalScore", gameplay.totalScore);
        if (SaveGame.Exists("totalTime"))
            gameplay.totalTime = SaveGame.Load("totalTime", gameplay.totalTime);
        if (SaveGame.Exists("HitEnemys"))
            SaveGame.Delete("HitEnemys");
        if (SaveGame.Exists("HitEnemys_01"))
        {
            Dictionary<int, int> hitEnemys = new Dictionary<int, int>();
            hitEnemys = SaveGame.Load("HitEnemys_01", hitEnemys);
            gameplay.HitEnemys = new List<EnemysResults>();
            foreach (KeyValuePair<int, int> keyValuePair in hitEnemys)
            {
                int length = gameSettings.allEnemys.Count;
                for (int i = 0; i < length; i++)
                {
                    if (gameSettings.allEnemys.Value[i].GetInstanceID() == keyValuePair.Key)
                    {
                        gameplay.HitEnemys.Add(new EnemysResults(gameSettings.allEnemys.Value[i], keyValuePair.Value));
                        return;
                    }
                }
            }
        }

    }

    public void SaveLanguages(Languages languages)
    {
        SaveGame.Save<int>("gameLanguage", languages.GetInstanceID());
    }

    public Languages LoadLanguages()
    {
        if (SaveGame.Exists("gameLanguage"))
        {
            int l = SaveGame.Load<int>("gameLanguage");
            foreach (Languages lang in LocalizationSettings.Instance.localizations)
            {
                if (lang.GetInstanceID() == l) return lang;
            }
            return LocalizationSettings.Instance.defaultLanguage;
        }
        else
        {
            return LocalizationSettings.Instance.defaultLanguage;
        }
    }

    public void SavePlayer(Player player)
    {
        SaveGame.Save("playerLives", player.lives);
        SaveGame.Save("playerWallet", player.wealth);
        SaveGame.Save("playerBombs", player.bombs);
        SaveGame.Save("playerListShop", MyUtils.Tools.ListToScriptableList<ShopProduct>(player.listProducts));
    }

    public void LoadPlayer(ref Player player)
    {
        if (SaveGame.Exists("playerLives"))
            player.lives.SetValue(SaveGame.Load("playerLives", player.lives));
        if (SaveGame.Exists("playerWallet"))
            player.wealth.SetValue(SaveGame.Load("playerWallet", player.wealth));
        if (SaveGame.Exists("playerBombs"))
            player.bombs.SetValue(SaveGame.Load("playerBombs", player.bombs));
        if (SaveGame.Exists("playerListShop"))
        {
            List<int> idlist = new List<int>();
            idlist.Add(GameSettings.Instance.defaultSkin.GetInstanceID());
            idlist = SaveGame.Load("playerListShop", idlist);
            player.listProducts = MyUtils.Tools.ScriptableListToList<ShopProduct>(idlist, GameSettings.Instance.listaShop.Value);
        }
    }
    public void SaveLevelComplete(string savename, List<Levels> levelComplete)
    {
        //Debug.Log("Salvou os Niveis");
        SaveGame.Save(savename, MyUtils.Tools.ListToScriptableList<Levels>(levelComplete));
    }
    public List<Levels> LoadLevelComplete(string savename)
    {
        if (SaveGame.Exists(savename))
        {
            List<int> idlist = new List<int>();
            idlist = SaveGame.Load(savename, idlist);
            return MyUtils.Tools.ScriptableListToList<Levels>(idlist, GameSettings.Instance.allLevels.Value);
        }
        else
            return new List<Levels>();
    }

    public void SaveGameClear()
    {
        SaveGame.Clear();
    }

    private void OnDisable()
    {
        language.EventTranslate -= SaveLanguages;
    }
    protected override void OnDestroy() { }
}
