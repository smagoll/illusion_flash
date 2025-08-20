using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBank", menuName = "Audio/Sound Bank")]
public class SoundBank : ScriptableObject
{
    [Header("Все звуки в игре")]
    public List<SoundData> sounds;

    private Dictionary<string, SoundData> soundMap;

    public void Initialize()
    {
        soundMap = new Dictionary<string, SoundData>();
        foreach (var s in sounds)
        {
            if (!string.IsNullOrEmpty(s.eventName))
                soundMap[s.eventName] = s;
        }
    }

    public bool TryGetSound(string eventName, out SoundData data)
    {
        if (soundMap == null) Initialize();
        return soundMap.TryGetValue(eventName, out data);
    }
}