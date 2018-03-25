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

    [SerializeField]
    private GameObject _loop;

    private GameObject _loopInstance;
    private AudioSource _loopSource;

    private float _targetFreq;
    private float _currentFreq;

    private AudioLowPassFilter _filter;

    private void Awake()
    {
        _loopInstance = Instantiate(_loop, transform);
        _loopSource = _loopInstance.GetComponent<AudioSource>();
        _filter = _loopInstance.GetComponent<AudioLowPassFilter>();
        _loopSource.Play();
    }

    void Start() {
        _targetFreq = _filter.cutoffFrequency;
    }

    void Update()
    {
        _targetFreq = (GameController.IsUsingPhone) ? filterFreq : 22000f;
        _currentFreq = Mathf.Lerp(_currentFreq, _targetFreq, 0.1f);
        _filter.cutoffFrequency = _currentFreq;
    }
}