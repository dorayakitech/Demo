using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[ShowOdinSerializedPropertiesInInspector]
public class AudioManager : Singleton<AudioManager>
{
    public enum SoundType
    {
        PlayerWalk,
        PlayerRun,
        DoorOperation
    }

    [SerializeField, Required] private Dictionary<SoundType, SOSound> _soundDict = new();
    [SerializeField, ReadOnly] private Dictionary<SoundType, AudioSource> _sourceDict = new();
    [SerializeField, ReadOnly] private Dictionary<SoundType, float> _lastPlayTimeDict = new();

    private void Awake()
    {
        foreach (var kv in _soundDict)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = kv.Value.Clip;
            source.volume = kv.Value.Volume;
            source.loop = kv.Value.Loop;
            source.playOnAwake = false;

            _sourceDict.Add(kv.Key, source);
        }
    }

    public void Play(SoundType soundType)
    {
        if (!_sourceDict.TryGetValue(soundType, out var source) || !_soundDict.TryGetValue(soundType, out var sound))
        {
            Debug.LogError($"{soundType} not found");
            return;
        }

        if (!_lastPlayTimeDict.TryGetValue(sound.SoundType, out var lastPlayTime))
        {
            // play the first time
            source.Play();
            _lastPlayTimeDict[sound.SoundType] = Time.time;
            return;
        }

        // handle delay
        if (!(Time.time - lastPlayTime >= sound.DelayTime)) return;
        source.Play();
        _lastPlayTimeDict[sound.SoundType] = Time.time;
    }
}