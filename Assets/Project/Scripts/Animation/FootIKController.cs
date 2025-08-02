using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FootIKController : MonoBehaviour
{
    [SerializeField] private Transform leftFootTarget;
    [SerializeField] private Transform leftFootBone;
    [SerializeField] private Transform rightFootTarget;
    [SerializeField] private Transform rightFootBone;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float footOffset = 0f;

    private void LateUpdate()
    {
        UpdateFootTarget(leftFootTarget, leftFootBone);
        UpdateFootTarget(rightFootTarget, rightFootBone);
    }

    private void UpdateFootTarget(Transform footBone, Transform footTarget)
    {
        // Берём мировую позицию кости стопы
        Vector3 rayOrigin = footBone.position + Vector3.up * 0.3f;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10, groundMask))
        {
            // Рассчитываем мировую позицию
            Vector3 worldPos = hit.point + Vector3.up * footOffset;

            // Переводим мировую позицию в локальные координаты родителя IK-цели
            Vector3 localPos = footTarget.parent.InverseTransformPoint(worldPos);

            // Ставим цель
            footTarget.localPosition = Vector3.Lerp(footTarget.localPosition, localPos, Time.deltaTime * 10);

            // Рассчитываем поворот стопы по нормали и переводим его в локальные координаты
            Quaternion worldRot = Quaternion.FromToRotation(Vector3.up, hit.normal) * transform.rotation;
            Quaternion localRot = Quaternion.Inverse(footTarget.parent.rotation) * worldRot;

            footTarget.localRotation = Quaternion.Lerp(footTarget.localRotation, localRot, Time.deltaTime * 10);
        }
        else
        {
            // Если нет земли под ногой - возвращаем стопу в исходную анимационную точку
            Vector3 localPos = footTarget.parent.InverseTransformPoint(footBone.position);
            footTarget.localPosition = Vector3.Lerp(footTarget.localPosition, localPos, Time.deltaTime * 10);
        }
    }
}