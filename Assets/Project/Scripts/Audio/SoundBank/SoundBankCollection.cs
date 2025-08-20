using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundBankCollection", menuName = "Audio/Sound Bank Collection")]
public class SoundBankCollection : ScriptableObject
{
    public List<SoundBank> banks;

    private Dictionary<string, SoundData> globalMap;

    public void Initialize()
    {
        globalMap = new Dictionary<string, SoundData>();
        foreach (var bank in banks)
        {
            bank.Initialize();
            foreach (var s in bank.sounds)
            {
                if (!string.IsNullOrEmpty(s.eventName))
                    globalMap[s.eventName] = s;
            }
        }
    }

    public bool TryGetSound(string eventName, out SoundData data)
    {
        if (globalMap == null) Initialize();
        return globalMap.TryGetValue(eventName, out data);
    }
}