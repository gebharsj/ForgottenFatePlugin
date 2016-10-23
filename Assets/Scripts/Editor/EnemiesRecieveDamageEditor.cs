using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemiesReceiveDamage))]
[CanEditMultipleObjects]
public class EnemiesRecieveDamageEditor : Editor
{
    SerializedObject serObj;
    SerializedProperty maxHp;
    SerializedProperty attackDamage;
    SerializedProperty defense;
    SerializedProperty criticalChance;
    SerializedProperty criticalMulitple;
    SerializedProperty expGiven;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        maxHp = serObj.FindProperty("maxHp");
        attackDamage = serObj.FindProperty("attackDamage");
        defense = serObj.FindProperty("defense");
        criticalChance = serObj.FindProperty("criticalChance");
        criticalMulitple = serObj.FindProperty("criticalDamage");
        expGiven = serObj.FindProperty("expGiven");

        //healthImage = serObj.FindProperty("healthBar");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();

        maxHp.intValue = EditorGUILayout.IntField(new GUIContent("Max Hp"), maxHp.intValue);

        attackDamage.intValue = EditorGUILayout.IntField(new GUIContent("Attack Damage"), attackDamage.intValue);

        defense.intValue = EditorGUILayout.IntField(new GUIContent("Defense"), defense.intValue);

        criticalChance.floatValue = EditorGUILayout.Slider(new GUIContent("Critical Chance", "The rate of which a critical hit occurs"), criticalChance.floatValue, .00f, .1f);

        criticalMulitple.floatValue = EditorGUILayout.Slider(new GUIContent("Critical Multiplier", "How many times greater a critical hit is"), criticalMulitple.floatValue, 1f, 5f);

        expGiven.intValue = EditorGUILayout.IntField(new GUIContent("Exp Given", "The amount of Exp this enemy gives"), expGiven.intValue);

        //EditorGUILayout.PropertyField(healthImage, new GUIContent("Health Image"));

        serObj.ApplyModifiedProperties();
    }
}
