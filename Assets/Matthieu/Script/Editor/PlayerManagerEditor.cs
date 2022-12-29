using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    // Health Life controls
    SerializedProperty _maxHealth;
    SerializedProperty _currentHealth;
    SerializedProperty _maxLifeCount;
    SerializedProperty _currentLifeCount;

    // Timer controls
    SerializedProperty _respawnTime;
    SerializedProperty _recoverTime;
    SerializedProperty _comboTime;
    SerializedProperty _attackTime;

    // Hurt manager
    SerializedProperty _invicibleDuration;
    SerializedProperty _knockbackForce;
    SerializedProperty _knockbackEasing;
    SerializedProperty _knockbackDuration;

    // Score
    SerializedProperty _score;


    private void OnEnable()
    {
        _maxHealth = serializedObject.FindProperty("_maxHealth");
        _currentHealth = serializedObject.FindProperty("_currentHealth");
        _maxLifeCount = serializedObject.FindProperty("_maxLifeCount");
        _currentLifeCount = serializedObject.FindProperty("_currentLifeCount");
        _respawnTime = serializedObject.FindProperty("_respawnTime");
        _recoverTime = serializedObject.FindProperty("_recoverTime");
        _comboTime = serializedObject.FindProperty("_comboTime");
        _attackTime = serializedObject.FindProperty("_attackTime");
        _invicibleDuration = serializedObject.FindProperty("_invicibleDuration");
        _knockbackForce = serializedObject.FindProperty("_knockbackForce");
        _knockbackEasing = serializedObject.FindProperty("_knockbackEasing");
        _knockbackDuration = serializedObject.FindProperty("_knockbackDuration");
        _score = serializedObject.FindProperty("_score");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(_maxHealth);
        _maxHealth.intValue = Mathf.Clamp(_maxHealth.intValue, 0, 10);
        EditorGUILayout.PropertyField(_currentHealth);
        _currentHealth.intValue = Mathf.Clamp(_currentHealth.intValue, 0, _maxHealth.intValue);
        EditorGUILayout.PropertyField(_maxLifeCount);
        _maxLifeCount.intValue = Mathf.Clamp(_maxLifeCount.intValue, 0, 5);
        EditorGUILayout.PropertyField(_currentLifeCount);
        _currentLifeCount.intValue = Mathf.Clamp(_currentLifeCount.intValue, 0, _maxLifeCount.intValue);

        EditorGUILayout.PropertyField(_respawnTime);
        EditorGUILayout.PropertyField(_recoverTime);
        EditorGUILayout.PropertyField(_comboTime);
        EditorGUILayout.PropertyField(_attackTime);
        EditorGUILayout.PropertyField(_invicibleDuration);
        EditorGUILayout.PropertyField(_knockbackForce);
        EditorGUILayout.PropertyField(_knockbackEasing);
        EditorGUILayout.PropertyField(_knockbackDuration);
        EditorGUILayout.PropertyField(_score);

        serializedObject.ApplyModifiedProperties();
    }
}
