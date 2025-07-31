// Editor для BehaviourTree (поместить в папку Editor)
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(BehaviourTree))]
public class BehaviourTreeEditor : Editor
{
    private BehaviourTree behaviourTree;
    private Vector2 scrollPosition;
    
    private void OnEnable()
    {
        behaviourTree = (BehaviourTree)target;
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.LabelField("Behaviour Tree", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        
        // Root Node
        EditorGUILayout.LabelField("Root Node:", EditorStyles.label);
        BTNode newRoot = (BTNode)EditorGUILayout.ObjectField(behaviourTree.RootNode, typeof(BTNode), false);
        if (newRoot != behaviourTree.RootNode)
        {
            Undo.RecordObject(behaviourTree, "Change Root Node");
            behaviourTree.SetRootNode(newRoot);
            EditorUtility.SetDirty(behaviourTree);
        }
        
        EditorGUILayout.Space();
        
        // Отображение структуры дерева
        if (behaviourTree.RootNode != null)
        {
            EditorGUILayout.LabelField("Tree Structure:", EditorStyles.boldLabel);
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(200));
            
            DrawNodeHierarchy(behaviourTree.RootNode, 0);
            
            EditorGUILayout.EndScrollView();
        }
        
        EditorGUILayout.Space();
        
        // Кнопки для создания новых нод
        EditorGUILayout.LabelField("Quick Create:", EditorStyles.boldLabel);
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Sequence"))
        {
            CreateNodeAsset<SequenceNode>("New Sequence");
        }
        if (GUILayout.Button("Create Selector"))
        {
            CreateNodeAsset<SelectorNode>("New Selector");
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Distance Check"))
        {
            CreateNodeAsset<DistanceCheckNode>("New Distance Check");
        }
        if (GUILayout.Button("Create Move Action"))
        {
            CreateNodeAsset<MoveToTargetNode>("New Move Action");
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
    
    private void DrawNodeHierarchy(BTNode node, int depth)
    {
        if (node == null) return;
        
        string indent = new string(' ', depth * 4);
        string nodeInfo = $"{indent}• {node.NodeName} ({node.GetType().Name})";
        
        EditorGUILayout.LabelField(nodeInfo);
        
        if (node is CompositeNode composite)
        {
            foreach (var child in composite.Children)
            {
                DrawNodeHierarchy(child, depth + 1);
            }
        }
        else if (node is DecoratorNode decorator && decorator.Child != null)
        {
            DrawNodeHierarchy(decorator.Child, depth + 1);
        }
    }
    
    private void CreateNodeAsset<T>(string defaultName) where T : BTNode
    {
        T asset = ScriptableObject.CreateInstance<T>();
        
        string path = EditorUtility.SaveFilePanelInProject(
            "Save BT Node", 
            defaultName, 
            "asset", 
            "Please enter a file name to save the node to");
        
        if (path.Length != 0)
        {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}

// Editor для CompositeNode для управления дочерними элементами
[CustomEditor(typeof(CompositeNode), true)]
public class CompositeNodeEditor : Editor
{
    private CompositeNode compositeNode;
    
    private void OnEnable()
    {
        compositeNode = (CompositeNode)target;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Children Management", EditorStyles.boldLabel);
        
        // Отображение дочерних элементов
        for (int i = 0; i < compositeNode.Children.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            
            BTNode child = (BTNode)EditorGUILayout.ObjectField(
                $"Child {i}", 
                compositeNode.Children[i], 
                typeof(BTNode), 
                false);
            
            if (child != compositeNode.Children[i])
            {
                Undo.RecordObject(compositeNode, "Change Child Node");
                compositeNode.Children[i] = child;
                EditorUtility.SetDirty(compositeNode);
            }
            
            if (GUILayout.Button("Remove", GUILayout.Width(60)))
            {
                Undo.RecordObject(compositeNode, "Remove Child Node");
                compositeNode.RemoveChild(compositeNode.Children[i]);
                EditorUtility.SetDirty(compositeNode);
                break;
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space();
        
        // Кнопка для добавления нового дочернего элемента
        if (GUILayout.Button("Add Child"))
        {
            Undo.RecordObject(compositeNode, "Add Child Node");
            compositeNode.AddChild(null);
            EditorUtility.SetDirty(compositeNode);
        }
        
        if (GUILayout.Button("Clear All Children"))
        {
            if (EditorUtility.DisplayDialog("Clear Children", 
                "Are you sure you want to remove all child nodes?", 
                "Yes", "Cancel"))
            {
                Undo.RecordObject(compositeNode, "Clear All Children");
                compositeNode.ClearChildren();
                EditorUtility.SetDirty(compositeNode);
            }
        }
    }
}

// Editor для DecoratorNode
[CustomEditor(typeof(DecoratorNode), true)]
public class DecoratorNodeEditor : Editor
{
    private DecoratorNode decoratorNode;
    
    private void OnEnable()
    {
        decoratorNode = (DecoratorNode)target;
    }
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Child Management", EditorStyles.boldLabel);
        
        BTNode newChild = (BTNode)EditorGUILayout.ObjectField("Child", decoratorNode.Child, typeof(BTNode), false);
        if (newChild != decoratorNode.Child)
        {
            Undo.RecordObject(decoratorNode, "Change Child Node");
            decoratorNode.SetChild(newChild);
            EditorUtility.SetDirty(decoratorNode);
        }
    }
}