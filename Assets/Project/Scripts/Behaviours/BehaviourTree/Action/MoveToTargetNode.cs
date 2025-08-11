using NodeCanvas.Framework;
using UnityEngine;

public class MoveToTargetAction : CharacterActionBase
{
    public BBParameter<float> stopDistance;

    private Transform target;
    
    protected override void OnExecute()
    { 
        target = Player != null ? Player.transform : null;

        if (Player == null)
        {
            EndAction(false);
        }
    }

    protected override void OnUpdate()
    {
        Vector3 direction = target.position - Character.transform.position;
        float distance = direction.magnitude;

        if (distance <= stopDistance.value)
        {
            EndAction(true);
            return;
        }
        
        direction.Normalize();
        Character.MovementController.MoveTo(target.position);
    }

    protected override void OnStop()
    {
        Character.MovementController.StopMove();
    }
}