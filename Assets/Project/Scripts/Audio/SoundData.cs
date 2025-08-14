using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundData", menuName = "Audio/Sound Data")]
public class SoundData : ScriptableObject
{
    public string eventName;            // ID события, например "Sword_Hit"
    public AudioClip[] variations;      // Варианты звука
    public AudioMixerGroup mixerGroup;  // Mixer-группа
    public bool is3D = true;            // 3D или 2D
    public float volume = 1f;
    public float pitchRandom = 0.05f;   // Рандомизация питча
    public float maxDistance = 50f;     // Дальность
}