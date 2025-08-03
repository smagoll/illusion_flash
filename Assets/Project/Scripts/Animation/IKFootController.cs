using UnityEngine;

public class IKFootController : IAnimatorIKController
{
    private Animator animator;
    private IKFootConfig config;

    private Vector3 leftFootPos, rightFootPos;
    private Quaternion leftFootRot, rightFootRot;

    private static readonly int IKLeftFootWeight = Animator.StringToHash("IKLeftFootWeight");
    private static readonly int IKRightFootWeight = Animator.StringToHash("IKRightFootWeight");

    public IKFootController(Animator animator, IKFootConfig config)
    {
        this.animator = animator;
        this.config = config;

        leftFootPos = animator.GetBoneTransform(HumanBodyBones.LeftFoot).position;
        rightFootPos = animator.GetBoneTransform(HumanBodyBones.RightFoot).position;

        leftFootRot = Quaternion.identity;
        rightFootRot = Quaternion.identity;
    }

    public void OnAnimatorIK(int layerIndex)
    {
        float leftWeight = animator.GetFloat(IKLeftFootWeight);
        float rightWeight = animator.GetFloat(IKRightFootWeight);

        HandleFootIK(AvatarIKGoal.LeftFoot, HumanBodyBones.LeftFoot, ref leftFootPos, ref leftFootRot, leftWeight);
        HandleFootIK(AvatarIKGoal.RightFoot, HumanBodyBones.RightFoot, ref rightFootPos, ref rightFootRot, rightWeight);
    }

    private void HandleFootIK(AvatarIKGoal foot, HumanBodyBones bone, ref Vector3 pos, ref Quaternion rot, float weight)
    {
        Transform footBone = animator.GetBoneTransform(bone);
        Vector3 rayStart = footBone.position + Vector3.up * config.raycastStartOffset;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, config.raycastLength, config.groundMask))
        {
            float dist = rayStart.y - hit.point.y;
            float finalWeight = (dist < config.maxIKDistance) ? weight : 0f;

            Vector3 targetPos = hit.point + Vector3.up * config.footHeightOffset;
            Quaternion targetRot = Quaternion.LookRotation(animator.transform.forward, hit.normal);

            pos = Vector3.Lerp(pos, targetPos, Time.deltaTime * config.positionLerpSpeed);
            rot = Quaternion.Slerp(rot, targetRot, Time.deltaTime * config.rotationLerpSpeed);

            animator.SetIKPositionWeight(foot, finalWeight);
            animator.SetIKRotationWeight(foot, finalWeight);
            animator.SetIKPosition(foot, pos);
            animator.SetIKRotation(foot, rot);
        }
        else
        {
            animator.SetIKPositionWeight(foot, 0f);
            animator.SetIKRotationWeight(foot, 0f);
        }
    }

    public void OnDrawGizmos()
    {
        if (animator == null) return;

        DrawFootRay(HumanBodyBones.LeftFoot, Color.green);
        DrawFootRay(HumanBodyBones.RightFoot, Color.cyan);
    }

    private void DrawFootRay(HumanBodyBones bone, Color color)
    {
        Transform t = animator.GetBoneTransform(bone);
        if (t == null) return;

        Vector3 rayStart = t.position + Vector3.up * config.raycastStartOffset;
        Vector3 rayEnd = rayStart + Vector3.down * config.raycastLength;

        Gizmos.color = color;
        Gizmos.DrawLine(rayStart, rayEnd);
        Gizmos.DrawSphere(rayEnd, 0.02f);
    }
}
