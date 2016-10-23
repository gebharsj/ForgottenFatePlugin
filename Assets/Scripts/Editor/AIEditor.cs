using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AiIntermediate))]
[CanEditMultipleObjects]

public class AIEditor : Editor
{
    SerializedObject serObj;
    SerializedProperty attackDistance;
    SerializedProperty movementSpeed;
    SerializedProperty attackTime;
    SerializedProperty fleeChance;
    SerializedProperty fleePercent;
    SerializedProperty targetObject;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        attackDistance = serObj.FindProperty("attackDistance");
        movementSpeed = serObj.FindProperty("movementSpeed");
        attackTime = serObj.FindProperty("attackTimer");
        fleeChance = serObj.FindProperty("fleePercent");
        fleePercent = serObj.FindProperty("fleeHealthPercent");
        targetObject = serObj.FindProperty("targetObject");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();

        attackDistance.floatValue = EditorGUILayout.FloatField(new GUIContent("Attack Distance", "Distance the enemy will start chasing from"), attackDistance.floatValue);

        movementSpeed.floatValue = EditorGUILayout.FloatField(new GUIContent("Movement Speed", "The speed the enemy moves at"), movementSpeed.floatValue);

        attackTime.floatValue = EditorGUILayout.Slider(new GUIContent("Attack Rate", "How often per second an ememy attacks"), attackTime.floatValue, .02f, 5f);

        fleeChance.intValue = EditorGUILayout.IntSlider(new GUIContent("Flee Rate", "The chance that an enemy flees"), fleeChance.intValue, 0, 100);

        fleePercent.intValue = EditorGUILayout.IntSlider(new GUIContent("Flee Percent", "The percent of health remaining that an enemy attempts to flee"), fleePercent.intValue, 0, 60);

        EditorGUILayout.PropertyField(targetObject, new GUIContent("Target Object"));

        serObj.ApplyModifiedProperties();
    }
}

