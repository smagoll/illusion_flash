using UnityEngine;

public class VFXBase : MonoBehaviour
{
    private AudioClip audioClip;
    
    public void Init(AudioClip audioClip)
    {
        this.audioClip = audioClip;
        PlaySound();
    }
    
    private void PlaySound()
    {
        if (audioClip != null)
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
    }
}