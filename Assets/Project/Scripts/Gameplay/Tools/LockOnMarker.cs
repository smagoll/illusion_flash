using UnityEngine;
using UnityEngine.UI;

public class LockOnMarker : MonoBehaviour
{
    public static LockOnMarker Instance; // Singleton для быстрого доступа
    
    [SerializeField] private Image markerImage;
    private Transform target;

    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        markerImage.enabled = false; // Скрыт пока нет цели
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // Переводим мировую позицию в экранные координаты
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

            // Проверка — если цель за камерой, скрываем маркер
            if (screenPos.z > 0)
            {
                markerImage.transform.position = screenPos;
                markerImage.enabled = true;
            }
            else
            {
                markerImage.enabled = false;
            }
        }
        else
        {
            markerImage.enabled = false;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ClearTarget()
    {
        target = null;
    }
}