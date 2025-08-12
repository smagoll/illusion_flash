using System;
using Input;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

public class CameraService : MonoBehaviour, ICameraService
{
    [SerializeField] private CinemachineCamera _freeLook;
    [SerializeField] private CinemachineCamera lockOnCam;
    
    [Header("Lock-On Settings")]
    [SerializeField] private float lockOnSmoothTime = 0.2f;
    [SerializeField] private float lockOnDistance = 5f;
    [SerializeField] private float lockOnHeight = 1.5f;
    [SerializeField] private float minHeight = 0.5f;
    [SerializeField] private LayerMask groundLayer = 1;
    
    private CinemachineCamera currentCam;
    private Transform player;
    private Transform currentTarget;
    private Vector3 lockOnVelocity;
    
    public Quaternion Rotation => currentCam.State.RawOrientation;
    public Vector3 Forward => currentCam.State.RawOrientation * Vector3.forward;

    private void Update()
    {
        if (IsLockOnActive())
        {
            UpdateLockOnCamera();
        }
    }

    public void SetTrackingTarget(Transform target)
    {
        currentCam = _freeLook;
        _freeLook.Target.TrackingTarget = target;
        player = target;
        lockOnCam.gameObject.SetActive(false);
    }

    public void LockOn(Transform target)
    {
        currentTarget = target;
        currentCam = lockOnCam;
        
        lockOnCam.Follow = null;
        lockOnCam.LookAt = target;
        
        _freeLook.gameObject.SetActive(false);
        lockOnCam.gameObject.SetActive(true);
    }

    public void Unlock()
    {
        currentTarget = null;
        currentCam = _freeLook;
        
        lockOnCam.gameObject.SetActive(false);
        _freeLook.gameObject.SetActive(true);
    }
    
    private bool IsLockOnActive()
    {
        return currentTarget != null && currentCam == lockOnCam && player != null;
    }
    
    private void UpdateLockOnCamera()
    {
        Vector3 targetPosition = CalculateCameraPosition();
        
        lockOnCam.transform.position = Vector3.SmoothDamp(
            lockOnCam.transform.position, 
            targetPosition, 
            ref lockOnVelocity, 
            lockOnSmoothTime
        );
    }
    
    private Vector3 CalculateCameraPosition()
    {
        // Направление от игрока к цели
        Vector3 toTarget = (currentTarget.position - player.position).normalized;
        
        // Позиция позади игрока
        Vector3 position = player.position - toTarget * lockOnDistance;
        position.y = player.position.y + lockOnHeight;
        
        // Простая проверка земли только по Y
        position.y = GetSafeHeight(position);
        
        return position;
    }
    
    private float GetSafeHeight(Vector3 position)
    {
        Vector3 rayStart = new Vector3(position.x, position.y + 2f, position.z);
        
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 5f, groundLayer))
        {
            float groundHeight = hit.point.y + minHeight;
            float playerMinHeight = player.position.y + minHeight;
            
            return Mathf.Max(position.y, groundHeight, playerMinHeight);
        }
        
        return Mathf.Max(position.y, player.position.y + minHeight);
    }
}