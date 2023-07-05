using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListLevels", menuName = "Variables/Lists/ListLevels", order = 3)]
public class ListLevels : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public List<Levels> Value = new List<Levels>();

    public void SetValue(List<Levels> value)
    {
        Value = value;
    }

    public void Add(Levels levels)
    {
        if(!Value.Contains(levels))
            Value.Add(levels);
    }

    public void Remove(Levels levels)
    {
        if (Value.Contains(levels))
            Value.Remove(levels);
    }

    public int Count
    {
        get { return Value.Count; }
    }
    public bool Contains(Levels levels)
    {
        return Value.Contains(levels);
    }

    public Levels LastLevel()
    {
        if (Value.Count > 0)
        {
            return Value[Value.Count - 1];
        }
        return null;
    }
}
