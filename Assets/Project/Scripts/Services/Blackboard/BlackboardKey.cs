using System.Collections.Generic;
using UnityEngine;

public class BlackboardKey<T>
{
    private static int nextId = 0;
    public readonly int Id;
    public readonly string Name;
    
    public BlackboardKey(string name)
    {
        Id = nextId++;
        Name = name;
    }
    
    public override string ToString() => Name;
}

public static class BBKeys
{
    // Player related
    public static readonly BlackboardKey<Vector3> PlayerPosition = new("PlayerPosition");
    public static readonly BlackboardKey<Transform> PlayerTransform = new("PlayerTransform");
    public static readonly BlackboardKey<Vector3> PlayerLastKnownPosition = new("PlayerLastKnownPosition");
    public static readonly BlackboardKey<bool> IsPlayerVisible = new("IsPlayerVisible");
    public static readonly BlackboardKey<float> DistanceToPlayer = new("DistanceToPlayer");
    
    // AI State
    public static readonly BlackboardKey<Vector3> CurrentDestination = new("CurrentDestination");
    public static readonly BlackboardKey<bool> IsMoving = new("IsMoving");
    public static readonly BlackboardKey<bool> HasReachedDestination = new("HasReachedDestination");
    
    // Combat
    public static readonly BlackboardKey<GameObject> CurrentTarget = new("CurrentTarget");
    public static readonly BlackboardKey<bool> IsInCombat = new("IsInCombat");
    public static readonly BlackboardKey<float> Health = new("Health");
    public static readonly BlackboardKey<float> MaxHealth = new("MaxHealth");
    
    // Environment
    public static readonly BlackboardKey<List<GameObject>> NearbyEnemies = new("NearbyEnemies");
    public static readonly BlackboardKey<List<Vector3>> PatrolPoints = new("PatrolPoints");
    public static readonly BlackboardKey<int> CurrentPatrolIndex = new("CurrentPatrolIndex");
}