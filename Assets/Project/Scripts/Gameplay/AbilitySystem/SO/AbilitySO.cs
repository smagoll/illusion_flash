using UnityEngine;

public abstract class AbilitySO : ScriptableObject
{
    [SerializeField] private string id;

    public string Id => id;
    
    public abstract Ability Create();
}