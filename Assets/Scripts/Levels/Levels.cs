using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.NewLocalization;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Agents/Levels", order = 102)]
public class Levels : ScriptableObject
{
    [SerializeField]
    public string levelName;
    [SerializeField]
    public LocalizationString translateName;
    [SerializeField]
    [Multiline]
    public string levelDedescription;
    [SerializeField]
    public LocalizationString translateDedescription;
    [SerializeField]
    public Sprite levelIcon;
    [SerializeField]
    public bool beatGame;
    [SerializeField]
    private float levelOffset;
    [SerializeField]
    private GameObject pathStart;
    [SerializeField]
    private GameObject pathEnd;
    [SerializeField]
    private int maxLevelLoad;
    [SerializeField]
    private List<LevelsSetup> levelSet;
    [SerializeField]
    private List<Levels> previousLevel;

    public GameObject StartPathLevel { get { return pathStart; } }
    public GameObject EndPathLevel { get { return pathEnd; } }
    public List<Levels> PreviousLevel { get { return previousLevel; } }

    public string GetName
    {
        get
        {
            if (translateName != null)
            {
                LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
                return translate.Translate(translateName, LocalizationTranslate.StringFormat.AllFirstLetterUp);
            }
            return this.levelName;
        }
    }

    public string GetDescription
    {
        get
        {
            if (translateDedescription != null)
            {
                LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
                return translate.Translate(translateDedescription, LocalizationTranslate.StringFormat.AllFirstLetterUp);
            }
            return this.levelDedescription;
        }
    }

    public void CreateLevel()
    {
        if (levelSet.Count > 0)
        {
            Vector3 nextbound = new Vector3(0, 0, levelOffset);
            GameObject patch = null;
            GameObject enemys = null;
            int patchs = levelSet.Count;
            patch = SetStartLevel(patch);
            nextbound = Vector3.zero;
            for (int i = 0; i < patchs; i++)
            {
                Bounds bound = new Bounds(Vector3.zero, Vector3.zero);
                bound = Utils.Tools.GetChildRenderBounds(levelSet[i].levelPaths, GameSettings.Instance.wallTag);
                levelSet[i].levelPaths.SetActive(false);
                patch = Instantiate<GameObject>(levelSet[i].levelPaths);
                patch.transform.position = nextbound;
                if (i == patchs - 1 && patch.GetComponentInChildren<EnemyMaster>())
                {
                    EnemyMaster[] enemyMaster = patch.GetComponentsInChildren<EnemyMaster>();
                    for (int ix = 0; ix < enemyMaster.Length; ix++)
                    {
                        if (enemyMaster[ix].enemy.endLevel)
                        {
                            enemyMaster[ix].goalLevel = true;
                            continue;
                        }
                    }
                }
                enemys = SetEnemys(nextbound, enemys, i);
                if (maxLevelLoad > i)
                    patch.SetActive(true);

                nextbound += new Vector3(0, 0, bound.extents.z * 2);
            }
            patch = SetEndLevel(nextbound, patch);
        }

    }

    private GameObject SetEnemys(Vector3 nextbound, GameObject enemys, int i)
    {
        if (levelSet[i].enemysSets != null)
        {
            levelSet[i].enemysSets.SetActive(false);
            enemys = Instantiate<GameObject>(levelSet[i].enemysSets);
            enemys.transform.position = nextbound;
            if (maxLevelLoad > i)
                enemys.SetActive(true);
        }

        return enemys;
    }

    private GameObject SetEndLevel(Vector3 nextbound, GameObject patch)
    {
        if (EndPathLevel != null)
        {
            patch = Instantiate<GameObject>(EndPathLevel);
            patch.transform.position = new Vector3(0, 0, nextbound.z);
        }

        return patch;
    }

    private GameObject SetStartLevel(GameObject patch)
    {
        if (StartPathLevel != null)
        {
            patch = Instantiate<GameObject>(pathStart);
            patch.transform.position = new Vector3(0, 0, levelOffset);
        }

        return patch;
    }

    public bool CheckIfCompleate(List<Levels> finishList)
    {
        return (finishList.Contains(this)) ? true : false;
    }

    public bool CheckIfLastFinish(List<Levels> finishList)
    {
        return (finishList[finishList.Count - 1] == this) ? true : false;
    }

    public bool CheckIfUnloked(Levels previous)
    {
        return (previousLevel.Contains(previous)) ? true : false;
    }

    public bool CheckIfLocked(List<Levels> finishList)
    {
        if (previousLevel.Count <= 0) return false;
        for (int i = 0; i < previousLevel.Count; i++)
        {
            if (finishList.Contains(previousLevel[i])) return false;
        }
        return true;
    }
}

[System.Serializable]
public struct LevelsSetup
{
    public GameObject levelPaths;
    public GameObject enemysSets;
} 