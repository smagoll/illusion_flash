using UnityEngine;

[System.Serializable]
public class IKFootConfig
{
    [Header("Raycast Settings")]
    public LayerMask groundMask;
    public float raycastStartOffset;
    public float raycastLength;

    [Header("Foot Offsets")]
    public float footHeightOffset;
    public float maxIKDistance;

    [Header("Smoothing")]
    public float positionLerpSpeed;
    public float rotationLerpSpeed;
    
    public static IKFootConfig Default => new IKFootConfig
    {
        groundMask = LayerMask.GetMask("Default"),
        raycastStartOffset = 0.3f,
        raycastLength = 0.6f,
        footHeightOffset = 0.05f,
        maxIKDistance = 0.4f,
        positionLerpSpeed = 8f,
        rotationLerpSpeed = 8f
    };
}