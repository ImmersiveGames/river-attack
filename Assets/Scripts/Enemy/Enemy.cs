using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;
using MyUtils.NewLocalization;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Enemy", menuName = "Agents/Enemy", order = 1)]
[System.Serializable]
public class Enemy : ScriptableObject
{

    [Header("Default Settings")]
    new public string name;
    public LocalizationString translateName;
    public string fbScore;
    public IntReference enemyScore;
    public Sprite spriteIcon;
    public AudioMixerGroup enemyAudioMixerGroup;
    public DifficultyList enemysDifficulty;
    public bool canFlip;
    public bool canRespawn;
    public bool canDestruct;
    public bool isCheckInPoint;

    public string GetName
    {
        get
        {
            if (translateName != null)
            {
                LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
                return translate.Translate(translateName, LocalizationTranslate.StringFormat.AllFirstLetterUp);
            }
            return this.name;
        }
    }
}
