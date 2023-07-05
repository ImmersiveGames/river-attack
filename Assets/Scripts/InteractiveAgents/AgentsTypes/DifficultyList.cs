using Utils.Variables;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleDifficultyList", menuName = "Agents/Dificult List", order = 201)]
public class DifficultyList : ScriptableObject
{
    public List<EnemySetDifficulty> difficulties;

    public List<string> ListDificultyByName()
    {
        IEnumerable<string> NewList = new List<string>();
        NewList = difficulties.Select(x => x.name).Distinct();
        return NewList.ToList();
    }
    public EnemySetDifficulty GetDifficult(int score)
    {
        return difficulties.Find(x => x.scoreToChange.Value >= (score));
    }
}

[System.Serializable]
public struct EnemySetDifficulty
{
    public string name;
    public FloatReference scoreMod;
    public IntReference scoreToChange;
    [Range(0, 10)]
    public float multplySpeedy;
    [Range(0, 10)]
    public float multplyPlayerDistance;
}
