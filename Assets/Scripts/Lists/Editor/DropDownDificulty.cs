using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleMoveByApproach), true)]
public class DropDownDificulty : Editor
{

    private DifficultyList enemyDifficultyList;
    private SerializedProperty dificultType;
    private List<string> dificults;
    private int indexDificult;

    public void OnEnable()
    {
        SetInitialReferences();
    }
    public void SetInitialReferences()
    {
        ObstacleMoveByApproach script = (ObstacleMoveByApproach)target;
        enemyDifficultyList = script.enDifList;
        //enemyDifficultyList = Selection.activeGameObject.GetComponent<EnemyDifficulty>().GetDifficultList();
        SetDropDownDificult();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        GetDropDownDificult();
        serializedObject.ApplyModifiedProperties();
    }

    private void SetDropDownDificult()
    {
        if(enemyDifficultyList != null && enemyDifficultyList.difficulties.Count > 0)
        {
            dificults = enemyDifficultyList.ListDificultyByName();
            indexDificult = 0;
            dificultType = serializedObject.FindProperty("dificultType");
            indexDificult = dificults.IndexOf(dificultType.stringValue);
        } 
    }

    private void GetDropDownDificult()
    {
        if (enemyDifficultyList != null && dificults.Count > 0)
        {
            indexDificult = EditorGUILayout.Popup("Enemy Dificult", indexDificult, dificults.ToArray(), EditorStyles.popup);
            if (indexDificult < 0)
                indexDificult = 0;
            dificultType.stringValue = dificults[indexDificult];
        }
    }
}
