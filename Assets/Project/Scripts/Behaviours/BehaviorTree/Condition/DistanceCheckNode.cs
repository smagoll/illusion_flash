using UnityEngine;

[CreateAssetMenu(fileName = "New Distance Check", menuName = "BehaviourTree/Condition/Distance Check")]
public class DistanceCheckNode : ConditionNode
{
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private bool checkPlayer = true;
    
    private Transform target;
    private Transform owner;
    
    public override bool CheckCondition(Character character)
    {
        if (!owner) owner = character.transform;
        
        target = null;
        
        if (checkPlayer)
        {
            var player = character.Blackboard.GlobalBlackboard.GetValue(BBKeys.PlayerCharacter);
            target = player.gameObject.transform;
        }
        
        if (target == null) return false;
        
        float distance = Vector3.Distance(character.transform.position, target.position);
        
        return distance <= maxDistance;
    }
    
    public override void OnDrawGizmos()
    {
        if (owner != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(owner.transform.position, maxDistance);
        }
    }
}