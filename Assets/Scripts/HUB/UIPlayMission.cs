using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayMission : MonoBehaviour
{
    [SerializeField]
    private float speedShowButton;
    [SerializeField]
    private float speedHideButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Text missionName;

    private HUBMaster hubMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        hubMaster.EventOnSelectMission += ShowButton;
        hubMaster.EventOnUnselectMission += HideButton;
    }

    private void SetInitialReferences()
    {
        hubMaster = HUBMaster.Instance;
    }

    private void OnDisable()
    {
        hubMaster.EventOnSelectMission -= ShowButton;
        hubMaster.EventOnUnselectMission -= HideButton;
    }

    public void ShowButton(Levels level)
    {
        if(level != null)
        {
            missionName.text = level.GetName;
            playButton.interactable = !level.CheckIfLocked(GameManager.Instance.levelsFinish);
            playButton.onClick.RemoveAllListeners();
            StopCoroutine("MovePanel");
            Vector2 posy = gameObject.GetComponent<RectTransform>().anchoredPosition;
            playButton.onClick.AddListener(
                () => { Play(level);}
                );
            StartCoroutine(MovePanel(gameObject.GetComponent<RectTransform>(),Vector2.zero, speedShowButton));
        }        
    }
    public void HideButton()
    {
        StopCoroutine("MovePanel");
        float hight = gameObject.GetComponent<RectTransform>().rect.height;
        StartCoroutine(MovePanel(gameObject.GetComponent<RectTransform>(),new Vector2(0, -hight), speedHideButton));
    }

    public IEnumerator MovePanel(RectTransform panel, Vector2 pointTo, float time)
    {
        while (panel.anchoredPosition != pointTo)
        {
            panel.anchoredPosition = Vector3.MoveTowards(panel.anchoredPosition, pointTo, time * Time.deltaTime);
            yield return null;
        }
    }

    public void Play(Levels level)
    {
        GameSettings.Instance.ChangeGameMode("Mission");
        GameSettings.Instance.ChangeGameScene(GameSettings.Instance.GetGameScenes("PlayGame"));
    }
}
