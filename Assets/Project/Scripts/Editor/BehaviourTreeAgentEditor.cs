using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BehaviourTreeAgent))]
public class BehaviourTreeAgentEditor : Editor
{
    private BehaviourTreeAgent agent;
    
    private void OnEnable()
    {
        agent = (BehaviourTreeAgent)target;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        if (agent.BehaviourTree != null)
        {
            EditorGUILayout.LabelField("Current Tree Info:", EditorStyles.boldLabel);
            EditorGUILayout.LabelField($"Tree: {agent.BehaviourTree.name}");
            
            if (agent.BehaviourTree.RootNode != null)
            {
                EditorGUILayout.LabelField($"Root: {agent.BehaviourTree.RootNode.NodeName}");
                EditorGUILayout.LabelField($"Type: {agent.BehaviourTree.RootNode.GetType().Name}");
            }
            else
            {
                EditorGUILayout.HelpBox("Root node is not set!", MessageType.Warning);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No behaviour tree assigned!", MessageType.Warning);
        }
        
        EditorGUILayout.Space();
        
        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Runtime Controls:", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Force Tick"))
            {
                if (agent.BehaviourTree != null)
                {
                    var character = agent.GetComponent<Character>();
                    if (character != null)
                    {
                        var result = agent.BehaviourTree.Tick(character);
                        Debug.Log($"Tick result: {result}");
                    }
                }
            }
        }
    }
}