using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : AudioManager {
    
    protected override string _VolumeKey
    {
        get
        {
            return GameTags.MusicVolume;
        }
    }

    [SerializeField]
    private float filterFreq = 400f;
    private float maxFreq = 22000f;

    [SerializeField]
    private GameObject _loop;

    private GameObject _loopInstance;
    private AudioSource _loopSource;

    private float _currentRatio;

    private AudioLowPassFilter _filter;

    private void Awake()
    {
        _loopInstance = Instantiate(_loop, transform);
        _loopSource = _loopInstance.GetComponent<AudioSource>();
        _filter = _loopInstance.GetComponent<AudioLowPassFilter>();
        _loopSource.Play();
    }

    float ratioToFrequency(float ratio)
    {
        float n = 12800f; //spooky number
        float m = (n * n) / (n + filterFreq);
        float f = m * (Mathf.Exp(ratio) - 1);
        return Mathf.Clamp(f + filterFreq, filterFreq, maxFreq);
    }

    void Start() {
        _currentRatio = 1;
    }

    void Update()
    {
        _currentRatio = Mathf.Lerp(_currentRatio, (GameController.IsUsingPhone) ? 0f : 1f, 0.3f);
        _filter.cutoffFrequency = ratioToFrequency(_currentRatio);
    }
}