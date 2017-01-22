using UnityEngine;
using UnityEditor;
using System.Collections;

public enum TypeOfSpell
{
    None,
    Projectile,
    Heal,
    Burst,
};

[CustomEditor(typeof(SpellCreator))]
public class SpellCreatorEditor : Editor {

    public TypeOfSpell spell;
    SerializedObject serObj;
    SerializedProperty sfx;
    SerializedProperty uiImage;
    SerializedProperty key;
    SerializedProperty unlocked;
    SerializedProperty cooldown;
    SerializedProperty damage;
    SerializedProperty obj;
    SerializedProperty healing;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        sfx = serObj.FindProperty("sfx");
        uiImage = serObj.FindProperty("UiImage");
        key = serObj.FindProperty("key");
        unlocked = serObj.FindProperty("unlocked");
        cooldown = serObj.FindProperty("cooldown");
        damage = serObj.FindProperty("damage");
        obj = serObj.FindProperty("obj");
        healing = serObj.FindProperty("healing");
    }

    public override void OnInspectorGUI()
    {
        spell = (TypeOfSpell)EditorGUILayout.EnumPopup("Type of Spell", spell);

        if (spell != 0)
        {
            EditorGUILayout.PropertyField(sfx, new GUIContent("Sound Effect"));
            EditorGUILayout.PropertyField(uiImage, new GUIContent("UI Image"));
            EditorGUILayout.PropertyField(key, new GUIContent("Activation Button"));
            unlocked.boolValue = EditorGUILayout.Toggle(new GUIContent("Unlocked", "If the spell is unlocked at the beguinning or not"), unlocked.boolValue);
            cooldown.floatValue = EditorGUILayout.FloatField(new GUIContent("Cooldown", "How often you can use that spell"), cooldown.floatValue);

            switch (spell)
            {
                case TypeOfSpell.Projectile:
                    damage.floatValue = EditorGUILayout.FloatField(new GUIContent("Damage", "The Amount of Damage each part does"), damage.floatValue);
                    EditorGUILayout.PropertyField(obj, new GUIContent("Projectile"));
                    break;
                case TypeOfSpell.Heal:
                    healing.intValue = EditorGUILayout.IntField(new GUIContent("Healing", "The Amount of Health You Restore"), healing.intValue);
                    break;
            }

            SpellCreator script = (SpellCreator)target;

            if (GUILayout.Button(new GUIContent("Build Spell")))
            {
                switch (spell)
                {
                    case TypeOfSpell.Projectile:
                        script.CreateProjectileSpell();
                        break;
                    case TypeOfSpell.Heal:
                        script.CreateHealingSpell();
                        break;
                }
                        
            }

            if (GUILayout.Button(new GUIContent("Clear Spells")))
            {
                switch (spell)
                {
                    case TypeOfSpell.Projectile:
                        script.ClearProjectileSpell();
                        break;
                    case TypeOfSpell.Heal:
                        script.ClearHealingSpell();
                        break;
                }

            }
        }

        serObj.ApplyModifiedProperties();
    }
}

