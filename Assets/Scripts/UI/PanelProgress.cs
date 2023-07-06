using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyUtils.Variables;

public class PanelProgress : MonoBehaviour
{

    [SerializeField]
    private GamePlaySettings playSettings;

    [Header("Text Fields")]
    [SerializeField]
    private Text textDistance;
    [SerializeField]
    private Text textLives;
    [SerializeField]
    private Text textFuel;
    [SerializeField]
    private Text textScore;
    [SerializeField]
    private Text textTime;
    private GameManagerSaves gameSaves;
    private float time;
    System.TimeSpan t;

    private void OnEnable()
    {
        gameSaves = GameManagerSaves.Instance;
        gameSaves.LoadGamePlay(playSettings);
        textDistance.text = playSettings.pathDistance.ToString("000000");
        textLives.text = Mathf.Abs(playSettings.livesSpents).ToString("000000");
        textFuel.text = playSettings.fuelSpents.ToString("000000");
        textScore.text = playSettings.totalScore.ToString("000000000");
        t = System.TimeSpan.FromSeconds(playSettings.totalTime + time);
        textTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
    }

    private void LateUpdate()
    {
        time = Time.unscaledTime;
        t = System.TimeSpan.FromSeconds(playSettings.totalTime + time);
        textTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
    }

    private void OnDisable()
    {
        //GameManagerSaves.Instance.LoadGamePlay(playSettings);
        playSettings.totalTime += Time.unscaledTime;
        gameSaves.SaveGamePlay(playSettings);
    }

    private void OnDestroy()
    {
        playSettings.totalTime += time;
    }
}
