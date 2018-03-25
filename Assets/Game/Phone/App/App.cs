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
    private GameObject _matchButton;
    [SerializeField]
    private GameObject _rejectButton;
    [SerializeField]
    private GameObject _warning;

    [SerializeField]
    private Camera _cam;
    private GameObject _currentGraphics;
    private DogProfile _currentProfile;

    private DogFactory _factory;
    private SFXManager _sfxManager;

    private PlayerController _player;
    private bool _dogCanSpawn { get { return _player.Dog == null; } }

    void LoadProfile(DogProfile profile)
    {
        if (_currentGraphics != null)
            Destroy(_currentGraphics);

        _currentGraphics = Instantiate(_factory.DogGraphics[profile.Index], transform);
        _currentGraphics.transform.position += _dogOffset;
        _currentGraphics.transform.eulerAngles = _dogAngles + new Vector3 { y = UnityEngine.Random.Range(-90f, 90f) };
        _currentGraphics.transform.localScale = Vector3.one * .1f;
        //_cam.transform.LookAt(_currentGraphics.transform);

        _nameText.text = profile.Name;
        _likesText.text = "likes " + profile.Like;
    }

    void LoadNewProfile()
    {
        GetComponentInChildren<Camera>().backgroundColor = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), 0.8f, 0.5f);
        _currentProfile = _factory.GetNewDogProfile();
        LoadProfile(_currentProfile);
    }

    void Start()
    {
        _player = this.Find<PlayerController>(GameTags.Player);
        _factory = this.FindInChild<DogFactory>(GameTags.Factories);
        _sfxManager = this.Find<SFXManager>(GameTags.Audio);
        LoadNewProfile();
    }

    void DoAccept()
    {
        _matchButton.GetComponent<PhoneButton>().Pulse();
        if (_dogCanSpawn)
        {
            _sfxManager.PlaySound("Success");
            StartCoroutine(SpawnDog());
        }
        else
        {
            _sfxManager.PlaySound("Failure");
            LoadNewProfile();
        }
    }

    void DoDecline()
    {
        _rejectButton.GetComponent<PhoneButton>().Pulse();
        _sfxManager.PlaySound("Failure");
        LoadNewProfile();
    }

    void Update()
    {
        if (!GameController.IsUsingPhone)
            return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            DoDecline();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            DoAccept();
        }
    }

    void OnGUI()
    {
        _warning.gameObject.SetActive(!_dogCanSpawn);
    }

    IEnumerator SpawnDog()
    {
        yield return new WaitForSecondsRealtime(.1f);
        _factory.SpawnDog(_currentProfile);
        GameController.IsUsingPhone = false;
        yield return new WaitForSecondsRealtime(.1f);
        LoadNewProfile();
    }
}
