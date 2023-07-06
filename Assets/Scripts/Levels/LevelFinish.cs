using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    GameSettings gameSettings;
    GameManager gameManager;
    GamePlayMaster gamePlay;
    private void OnEnable()
    {
        gamePlay = GamePlayMaster.Instance;
        gameManager = GameManager.Instance;
        gameSettings = GameSettings.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(gameSettings.playerTag))
        {
            if (!gameManager.levelsFinish.Contains(gameManager.actualLevel))
                gameManager.levelsFinish.Add(gameManager.actualLevel);
            gamePlay.CallEventCompletePath();
            LogFinish();
        }
    }

    private void LogFinish()
    {
        if (gameManager.firebase.MyFirebaseApp != null && gameManager.firebase.dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Firebase.Analytics.Parameter[] FinishLevel = {
            new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, gamePlay.GetActualLevel().GetName),
            new Firebase.Analytics.Parameter("Milstone", gamePlay.GetActualPath())
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("FinishLevel", FinishLevel);
        }
        //Eventos do facebook
        if (FB.IsInitialized)
        {
            Dictionary<string, object> fbeventparamLevel = new Dictionary<string, object>();
            fbeventparamLevel[AppEventParameterName.Level] = gamePlay.GetActualLevel().GetName;
            fbeventparamLevel["Milstone"] = gamePlay.GetActualPath();
            FB.LogAppEvent(AppEventName.AchievedLevel, parameters: fbeventparamLevel);
        }
    }
}
