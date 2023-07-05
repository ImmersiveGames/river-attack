using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBMaster : Singleton<HUBMaster>
{
    public Dictionary<Levels, Transform> iconsPosition { get; private set; }
    public Dictionary<Levels, HUBIconMaster> iconMasterList { get; private set; }

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventOffFocusElement;

    public delegate void HUBIconsEventHandler(Levels level, Vector3 position);
    public event HUBIconsEventHandler EventOnFocusElement;
    public event HUBIconsEventHandler EventAllOffFocusElement;

    public delegate void HUBEventHandler(Levels level);
    public event HUBEventHandler EventBeatLevel;
    public event HUBEventHandler EventUnlockedMission;

    public void AddIconPosition(Levels level, Transform pos)
    {
        if (iconsPosition != null && !iconsPosition.ContainsKey(level))
            iconsPosition.Add(level, pos);
        else
        {
            iconsPosition = new Dictionary<Levels, Transform>();
            iconsPosition.Add(level, pos);
        }
    }
    public void AddIconMaster(Levels level, HUBIconMaster icon)
    {
        if (iconMasterList != null && !iconMasterList.ContainsKey(level))
            iconMasterList.Add(level, icon);
        else
        {
            iconMasterList = new Dictionary<Levels, HUBIconMaster>();
            iconMasterList.Add(level, icon);
        }
    }

    public Transform GetIconTransform(Levels level)
    {
        return iconsPosition[level];
    }

    public HUBIconMaster GetIconMaster(Levels level)
    {
        return iconMasterList[level];
    }

    public List<Levels> GetPreviousLevelOf(Levels level)
    {
        List<Levels> listPreviousLevles = new List<Levels>();
        foreach (KeyValuePair<Levels, HUBIconMaster> item in iconMasterList)
        {
            if (!iconMasterList[item.Key].MyLevel.PreviousLevel.Contains(level)) continue;
            int length = iconMasterList[item.Key].MyLevel.PreviousLevel.Count;
            for (int i = 0; i < length; i++)
            {
                listPreviousLevles.Add(item.Key);
            }
        }
        return listPreviousLevles;
    }

    public void CallEventAllOffFocusElement(Levels level, Vector3 pos)
    {
        if (EventAllOffFocusElement != null)
        {
            EventAllOffFocusElement(level, pos);
        }
    }
    public void CallEventOffFocusElement()
    {
        if (EventOffFocusElement != null)
        {
            EventOffFocusElement();
        }
    }

    public void CallEventOnFocusElement(Levels level, Vector3 pos)
    {
        if (EventOnFocusElement != null)
        {
            EventOnFocusElement(level, pos);
        }
    }
    public void CallEventUnlockedMission(Levels level)
    {
        if (EventUnlockedMission != null)
        {
            EventUnlockedMission(level);
        }
    }
    public void CallEventBeatLevel(Levels level)
    {
        if (EventBeatLevel != null)
        {
            EventBeatLevel(level);
        }
    }

    protected override void OnDestroy() { }
}
