using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HUBSettings", menuName = "GameManagers/HUBSettings", order = 1)]
public class HUBSettings : SingletonScriptableObject<HUBSettings>
{
    [SerializeField]
    public List<Levels> levelHubComplete;
    [SerializeField]
    public List<Levels> levelToUnlock;
}
