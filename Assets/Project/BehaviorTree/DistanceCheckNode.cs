using NodeCanvas.Framework;
using UnityEngine;

public class DistanceCheckNode : CharacterConditionBase
{
    BBParameter<float> maxDistance;

    protected override bool OnCheck()
    {
        var target = Player.transform;

        if (target == null) return false;

        float distance = Vector3.Distance(Character.transform.position, target.position);
        return distance <= maxDistance.value;
    }
}