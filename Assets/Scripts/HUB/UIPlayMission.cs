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

    private HUBMaster hubMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        hubMaster.EventOnFocusElement += ShowButton;
        hubMaster.EventOffFocusElement += HideButton;
    }

    private void SetInitialReferences()
    {
        hubMaster = HUBMaster.Instance;
    }

    public void ShowButton(Levels level, Vector3 pos)
    {
        playButton.interactable = !level.CheckIfLocked(GameManager.Instance.levelsFinish.Value);
        StopCoroutine("MovePanel");
        Vector2 posy = gameObject.GetComponent<RectTransform>().anchoredPosition;
        StartCoroutine(MovePanel(Vector2.zero, speedShowButton));
    }
    public void HideButton()
    {
        StopCoroutine("MovePanel");
        float hight = gameObject.GetComponent<RectTransform>().rect.height;
        StartCoroutine(MovePanel(new Vector2(0, -hight), speedHideButton));
    }

    public IEnumerator MovePanel(Vector2 pointTo, float time)
    {
        while (GetComponent<RectTransform>().anchoredPosition != pointTo)
        {
            GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, pointTo, time * Time.deltaTime);
            yield return null;
        }
    }

    public void Play()
    {
        GameSettings.Instance.Play((int)GameSettings.GameScenes.Mission);
    }

    private void OnDisable()
    {
        hubMaster.EventOnFocusElement -= ShowButton;
        hubMaster.EventOffFocusElement -= HideButton;
    }
}
