using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ExpSystemPlayer))]
public class ExpEditor : Editor
{
    SerializedObject serObj;
    SerializedProperty healthGained;
    SerializedProperty staminaGained;
    SerializedProperty attackGained;
    SerializedProperty defenseGained;
    SerializedProperty healthGainedMin;
    SerializedProperty healthGainedMax;
    SerializedProperty staminaGainedMin;
    SerializedProperty staminaGainedMax;
    SerializedProperty attackGainedMin;
    SerializedProperty attackGainedMax;
    SerializedProperty defenseGainedMin;
    SerializedProperty defenseGainedMax;
    SerializedProperty random;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        random = serObj.FindProperty("random");
        healthGained = serObj.FindProperty("healthGained");
        staminaGained = serObj.FindProperty("staminaGained");
        attackGained = serObj.FindProperty("attackGained");
        defenseGained = serObj.FindProperty("defenseGained");
        healthGainedMin = serObj.FindProperty("healthGainedMin");
        healthGainedMax = serObj.FindProperty("healthGainedMax");
        staminaGainedMin = serObj.FindProperty("staminaGainedMin");
        staminaGainedMax = serObj.FindProperty("staminaGainedMax");
        attackGainedMin = serObj.FindProperty("attackGainedMin");
        attackGainedMax = serObj.FindProperty("attackGainedMax");
        defenseGainedMin = serObj.FindProperty("defenseGainedMin");
        defenseGainedMax = serObj.FindProperty("defenseGainedMax");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();

        random.boolValue = EditorGUILayout.Toggle(new GUIContent("Random/Fixed", "If clicked, the level-up will random between two values. If not, the level-up will be fixed"), random.boolValue);

        if (random.boolValue)
        {
            EditorGUILayout.BeginHorizontal();

            healthGainedMin.intValue = EditorGUILayout.IntField(new GUIContent("Health Min Gained", "Mininium amount of health gained on level-up"), healthGainedMin.intValue);
            healthGainedMax.intValue = EditorGUILayout.IntField(new GUIContent("Health Max Gained", "Maxinium amount of health gained on level-up"), healthGainedMax.intValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            staminaGainedMin.intValue = EditorGUILayout.IntField(new GUIContent("Stamina Min Gained", "Mininium amount of stamina gained on level-up"), staminaGainedMin.intValue);
            staminaGainedMax.intValue = EditorGUILayout.IntField(new GUIContent("Stamina Max Gained", "Maxinium amount of stamina gained on level-up"), staminaGainedMax.intValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            attackGainedMin.intValue = EditorGUILayout.IntField(new GUIContent("Attack Min Gained", "Mininium amount of attack gained on level-up"), attackGainedMin.intValue);
            attackGainedMax.intValue = EditorGUILayout.IntField(new GUIContent("Attack Max Gained", "Maxinium amount of attack gained on level-up"), attackGainedMax.intValue);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            defenseGainedMin.intValue = EditorGUILayout.IntField(new GUIContent("Defense Min Gained", "Mininium amount of defense gained on level-up"), defenseGainedMin.intValue);
            defenseGainedMax.intValue = EditorGUILayout.IntField(new GUIContent("Defense Max Gained", "Maxinium amount of defense gained on level-up"), defenseGainedMax.intValue);

            EditorGUILayout.EndHorizontal();

        }
        else
        {
            healthGained.intValue = EditorGUILayout.IntField(new GUIContent("Health Gained", "Amount of health gained on level-up"), healthGained.intValue);
            staminaGained.intValue = EditorGUILayout.IntField(new GUIContent("Stamina Gained", "Amount of stamina gained on level-up"), staminaGained.intValue);
            attackGained.intValue = EditorGUILayout.IntField(new GUIContent("Attack Gained", "Amount of base attack gained on level-up"), attackGained.intValue);
            defenseGained.intValue = EditorGUILayout.IntField(new GUIContent("Defense Gained", "Amount of defense gained on level-up"), defenseGained.intValue);
        }

        serObj.ApplyModifiedProperties();
    }
}
