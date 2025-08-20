using UnityEngine;

public class VFXBase : MonoBehaviour
{
    private VFXData data;
    
    public void Init(VFXData vfxData)
    {
        data = vfxData;
        PlaySound();
    }
    
    private void PlaySound()
    {
        if (data != null)
            AudioSystem.Instance.Play(data.sound.eventName, transform.position);
    }
}