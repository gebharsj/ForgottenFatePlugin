using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor {

    SerializedObject serObj;
    SerializedProperty speed;
    SerializedProperty sprint;
    SerializedProperty maxStamina;
    SerializedProperty staminaRecoveryRate;
    SerializedProperty staminaDelay;

    void OnEnable()
    {
        serObj = new SerializedObject(target);
        speed = serObj.FindProperty("speed");
        sprint = serObj.FindProperty("sprint");
        maxStamina = serObj.FindProperty("maxStamina");
        staminaRecoveryRate = serObj.FindProperty("staminaRecoveryRate");
        staminaDelay = serObj.FindProperty("staminRechargeDelay");
    }

    public override void OnInspectorGUI()
    {
        serObj.Update();

        speed.floatValue = EditorGUILayout.Slider(new GUIContent("Movement Speed", "The movement speed of the player"), speed.floatValue, 10f, 100f);

        sprint.floatValue = EditorGUILayout.Slider(new GUIContent("Sprint Multiplier", "How much faster sprinting is"), sprint.floatValue, 1f, 10f);

        staminaRecoveryRate.floatValue = EditorGUILayout.Slider(new GUIContent("Stamina Recharge Rate", "How fast stamina recharges"), staminaRecoveryRate.floatValue, .05f, 1f);

        staminaDelay.intValue = EditorGUILayout.IntSlider(new GUIContent("Stamin Recharge Delay", "How long before the stamina recharge starts"), staminaDelay.intValue, 0, 10);

        maxStamina.intValue = EditorGUILayout.IntField(new GUIContent("Max Stamina", "How much stamina a player gets. Enables sprint."), maxStamina.intValue);

        serObj.ApplyModifiedProperties();
    }
}
