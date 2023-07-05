using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Enemy", menuName = "Agents/Enemy", order = 1)]
public class Enemy : ScriptableObject {

    [Header("Default Settings")]
    new public string name;
    public IntReference enemyScore;
    public Sprite spriteIcon;
    //public bool canSpriteFlip;
    public AudioMixerGroup enemyAudioMixerGroup;
    public DifficultyList enemysDifficulty;
    public bool canFlip;
    public bool endLevel;
    public bool canRespawn;
    public bool canDestruct;
    public bool ignoreWall;
    public bool ignoreEnemys;
    public bool isCheckInPoint;
}
