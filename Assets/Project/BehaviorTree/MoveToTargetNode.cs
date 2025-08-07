using NodeCanvas.Framework;
using UnityEngine;

public class MoveToTargetNode : CharacterNodeBase
{
    public BBParameter<float> stopDistance;

    protected override void OnExecute()
    {
        var target = Player.transform;
        if (target == null)
        {
            EndAction(false);
            return;
        }

        Vector3 direction = target.position - Character.transform.position;
        float distance = direction.magnitude;

        if (distance <= stopDistance.value)
        {
            EndAction(true);
            return;
        }

        direction.Normalize();
        Character.MovementController.MoveTo(target.position, 5f);
        
    }

    protected override void OnStop()
    {
        Character.MovementController.StopMove();
    }
}