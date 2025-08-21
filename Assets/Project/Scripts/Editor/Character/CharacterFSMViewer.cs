using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

public class CharacterFSMViewer : EditorWindow
{
    private Vector2 scrollPos;

    [MenuItem("Window/Character FSM Viewer")]
    public static void ShowWindow()
    {
        GetWindow<CharacterFSMViewer>("FSM Viewer");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Character FSM Viewer", EditorStyles.boldLabel);

        GameObject selectedObject = Selection.activeGameObject;
        if (selectedObject == null)
        {
            EditorGUILayout.HelpBox("Select a character in the scene.", MessageType.Info);
            return;
        }

        Character character = selectedObject.GetComponent<Character>();
        if (character == null)
        {
            EditorGUILayout.HelpBox("Selected object has no Character component.", MessageType.Warning);
            return;
        }

        if (character.StateMachine == null)
        {
            EditorGUILayout.HelpBox("The selected character has no initialized StateMachine.", MessageType.Warning);
            return;
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        var fsm = character.StateMachine;

        EditorGUILayout.LabelField("Selected Character:", character.name);
        EditorGUILayout.LabelField("Current State:", fsm.CurrentState?.GetType().Name ?? "None");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("All States:", EditorStyles.boldLabel);

        foreach (var statePair in GetStates(fsm))
        {
            string stateName = statePair.Key.Name;
            bool isActive = statePair.Value == fsm.CurrentState;
            EditorGUILayout.LabelField(stateName, isActive ? EditorStyles.helpBox : EditorStyles.label);
        }

        EditorGUILayout.EndScrollView();

        Repaint();
    }

    private Dictionary<Type, CharacterState> GetStates(CharacterStateMachine fsm)
    {
        var statesField = typeof(CharacterStateMachine).GetField("_states", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return statesField.GetValue(fsm) as Dictionary<Type, CharacterState>;
    }
}
