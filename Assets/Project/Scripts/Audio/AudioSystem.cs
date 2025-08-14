using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance;

    [Header("Pooling")]
    [SerializeField] private AudioSource audioSourcePrefab;
    [SerializeField] private int poolSize = 20;

    [Header("Sound Library")]
    [SerializeField] private List<SoundData> soundLibrary;

    private Dictionary<string, SoundData> soundMap;
    private Queue<AudioSource> pool = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        soundMap = new Dictionary<string, SoundData>();
        foreach (var s in soundLibrary)
        {
            if (!string.IsNullOrEmpty(s.eventName))
                soundMap[s.eventName] = s;
        }

        for (int i = 0; i < poolSize; i++)
            CreateSource();
    }

    private void CreateSource()
    {
        var obj = Instantiate(audioSourcePrefab, transform);
        obj.playOnAwake = false;
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    private AudioSource GetSource()
    {
        if (pool.Count == 0)
            CreateSource();
        var src = pool.Dequeue();
        src.gameObject.SetActive(true);
        return src;
    }

    private void ReturnSource(AudioSource src)
    {
        src.Stop();
        src.clip = null;
        src.gameObject.SetActive(false);
        pool.Enqueue(src);
    }

    public void Play(string eventName, Vector3 position)
    {
        if (!soundMap.TryGetValue(eventName, out var def))
        {
            Debug.LogWarning($"Sound '{eventName}' not found!");
            return;
        }

        AudioSource src = GetSource();
        src.transform.position = position;
        src.outputAudioMixerGroup = def.mixerGroup;
        src.volume = def.volume;
        src.pitch = 1f + Random.Range(-def.pitchRandom, def.pitchRandom);
        src.spatialBlend = def.is3D ? 1f : 0f;
        src.maxDistance = def.maxDistance;
        src.clip = def.variations[Random.Range(0, def.variations.Length)];
        src.Play();
        StartCoroutine(ReturnAfter(src, src.clip.length));
    }

    public void Play2D(string eventName)
    {
        Play(eventName, Vector3.zero);
    }

    private System.Collections.IEnumerator ReturnAfter(AudioSource src, float time)
    {
        yield return new WaitForSeconds(time);
        ReturnSource(src);
    }
}
