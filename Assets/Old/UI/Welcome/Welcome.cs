using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

public class Welcome : MonoBehaviour {
    [SerializeField]
    private GameObject _welcome;
    private Image _welcomeImage;

    private float _currentBrightness = 1f;

    private GameController _controller;

    private void Start()
    {
        _welcomeImage = _welcome.GetComponent<Image>();

        _controller = this.Find<GameController>(GameTags.GameController);
        _controller.StartGame.AddListener(() => StartCoroutine(FadeOut()));
    }

    IEnumerator FadeOut()
    {
        while (_currentBrightness > 0.001f)
        {
            _currentBrightness = Mathf.Lerp(_currentBrightness, 0f, 0.1f);
            _welcomeImage.color = new Color(1f, 1f, 1f, _currentBrightness);
            yield return new WaitForEndOfFrame();
        }
        Destroy(_welcome.gameObject);
    }
}
