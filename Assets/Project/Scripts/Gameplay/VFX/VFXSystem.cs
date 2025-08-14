using System;
using UnityEngine;

public class VFXSystem : MonoBehaviour
{
    public static VFXSystem Instance;
    
    public VFXLibrary library;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void SpawnImpact(VFXData vfxData, Vector3 position, Vector3 normal)
    {
        var data = vfxData;
        if (data == null) return;

        var vfxInstance = Instantiate(data.vfxPrefab, position, Quaternion.LookRotation(normal));

        if (vfxInstance != null)
            vfxInstance.Init(data);
    }
}
