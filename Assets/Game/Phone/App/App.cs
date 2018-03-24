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

    private GameObject _currentGraphics;
    private DogProfile _currentProfile;

    private DogFactory _factory;

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

    void Start()
    {
        _factory = this.FindInChild<DogFactory>(GameTags.Factories);
        LoadProfile(_factory.GetNewDogProfile());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameController.IsUsingPhone)
            {
                _factory.SpawnDog(_currentProfile);
            }
            else
            {
                _currentProfile = _factory.GetNewDogProfile();
                LoadProfile(_currentProfile);
            }
        }
    }
}
