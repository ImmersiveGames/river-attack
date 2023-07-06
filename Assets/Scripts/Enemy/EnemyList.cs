using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyList", menuName = "Variables/Lists/EnemyLists", order = 4)]
public class EnemyList : ScriptableObject
{

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public List<Enemy> Value = new List<Enemy>();

    public void SetValue(List<Enemy> value)
    {
        Value = value;
    }

    public void Add(Enemy levels)
    {
        if (!Value.Contains(levels))
            Value.Add(levels);
    }

    public void Remove(Enemy levels)
    {
        if (Value.Contains(levels))
            Value.Remove(levels);
    }

    public int Count
    {
        get { return Value.Count; }
    }
    public bool Contains(Enemy levels)
    {
        return Value.Contains(levels);
    }
}
