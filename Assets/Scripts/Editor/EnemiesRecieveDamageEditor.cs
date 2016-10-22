using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemiesReceiveDamage))]
[CanEditMultipleObjects]
public class EnemiesRecieveDamageEditor : Editor
{
    SerializedObject serObj;
    SerializedProperty maxHp;
    SerializedProperty attackDamage;
    

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        maxHp = serObj.FindProperty("maxHp");
        attackDamage = serObj.FindProperty("attackDamage");
        //healthImage = serObj.FindProperty("healthBar");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();

        maxHp.intValue = EditorGUILayout.IntField(new GUIContent("Max Hp"), maxHp.intValue);

        attackDamage.intValue = EditorGUILayout.IntField(new GUIContent("Attack Damage"), attackDamage.intValue);

        //EditorGUILayout.PropertyField(healthImage, new GUIContent("Health Image"));

        serObj.ApplyModifiedProperties();
    }
}
