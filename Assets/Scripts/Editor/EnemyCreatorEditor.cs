using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EnemyCreator))]
public class EnemyCreatorEditor : Editor {

    SerializedObject serObj;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyCreator theScript = (EnemyCreator)target;
        if (GUILayout.Button(new GUIContent("Build Enemy")))
        {
            theScript.BuildEnemy();
        }
    }
}
