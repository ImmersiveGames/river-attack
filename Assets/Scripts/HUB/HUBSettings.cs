using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HUBSettings", menuName = "GameManagers/HUBSettings", order = 1)]
public class HUBSettings : SingletonScriptableObject<HUBSettings>
{

    public enum IconStatAnimation { Normal, Locked, UnLocked, Finish, Complete }

    [SerializeField]
    private ListLevels hubFinishLevels;
    [SerializeField]
    private ListLevels realFinishLevels;
    [SerializeField]
    public float timeToMoveCam;

    public ListLevels HUBFinishLevels { get { return hubFinishLevels; } }
    public ListLevels RealFinishLevels { get { return realFinishLevels; } }

    public bool BeatNewLevel
    {
        get
        {
            if (realFinishLevels != null && hubFinishLevels.Count < realFinishLevels.Count)
                return true;
            return false;
        }
    }

    public Levels GetLevelBeat()
    {
        return realFinishLevels.LastLevel();
    }

    public void AddBeatLevel(Levels level)
    {
        hubFinishLevels.Add(level);
    }
}
