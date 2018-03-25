using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

public class App : MonoBehaviour {
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _likesText;

    [SerializeField]
    private Vector3 _dogOffset;
    [SerializeField]
    private Vector3 _dogAngles;

    [SerializeField]
    private Camera _cam;
    private GameObject _currentGraphics;
    private DogProfile _currentProfile;

    private DogFactory _factory;
    private SFXManager _sfxManager;

    private bool _hasPressed = false;

    [SerializeField]
    private int _maxDogs = 1;
    private int _numDogs = 0;

    void LoadProfile(DogProfile profile)
    {
        if (_currentGraphics != null)
            Destroy(_currentGraphics);

        _currentGraphics = Instantiate(_factory.DogGraphics[profile.Index], transform);
        _currentGraphics.transform.position += _dogOffset;
        _currentGraphics.transform.eulerAngles = _dogAngles;
        _currentGraphics.transform.localScale = Vector3.one * .1f;

        _nameText.text = profile.Name;
        _likesText.text = "likes " + profile.Like;
    }

    void LoadNewProfile(bool silent = false)
    {
        if (!silent)
            _sfxManager.PlayPitchedSound(GameTags.Woof);
        GetComponentInChildren<Camera>().backgroundColor = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), 0.8f, 0.5f);
        _currentProfile = _factory.GetNewDogProfile();
        LoadProfile(_currentProfile);
    }

    void Start()
    {
        _factory = this.FindInChild<DogFactory>(GameTags.Factories);
        _sfxManager = this.Find<SFXManager>(GameTags.Audio);
        LoadNewProfile(true);
    }

    void DoAccept()
    {
        if (_numDogs < _maxDogs)
        {
            var dog = _factory.SpawnDog(_currentProfile);
            dog.Leave.AddListener(() => _numDogs--);
            _numDogs++;
            GameController.IsUsingPhone = false;
        }
        LoadNewProfile();
    }

    void DoDecline()
    {
        LoadNewProfile();
    }

    void Update()
    {
        if (!GameController.IsUsingPhone)
            return;

        float _axis = Input.GetAxis("Horizontal");

        if (_axis == 0f)
        {
            _hasPressed = false;
        }
        else if (!_hasPressed)
        {
            _hasPressed = true;
            if (_axis > 0f)
            {
                DoAccept();
            }
            else
            {
                DoDecline();
            }
        }
    }
}
