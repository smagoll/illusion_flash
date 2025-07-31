using UnityEngine;

[CreateAssetMenu(fileName = "New Distance Check", menuName = "BehaviourTree/Condition/Distance Check")]
public class DistanceCheckNode : ConditionNode
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private bool checkPlayer = true;
    [SerializeField] private string targetTag = "Player";
    
    private Transform target;
    
    public override bool CheckCondition(Character character)
    {
        target = null;
        
        if (checkPlayer)
        {
            var player = GameObject.FindWithTag(targetTag);
            target = player?.transform;
        }
        
        if (target == null) return false;
        
        float distance = Vector3.Distance(character.transform.position, target.position);
        return distance <= maxDistance;
    }
    
    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.transform.position, maxDistance);
    }
}