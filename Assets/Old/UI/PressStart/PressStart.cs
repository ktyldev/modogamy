using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensions;

public class PressStart : MonoBehaviour {
    [SerializeField]
    private GameObject _start;
    private Text _startText;

    Coroutine _lastPulse = null;
    private GameController _controller;

    private void Start()
    {
        _startText = _start.GetComponent<Text>();
        _lastPulse = StartCoroutine(DoPulse());

        _controller = this.Find<GameController>(GameTags.GameController);
        _controller.StartGame.AddListener(FadeOut);
    }

    private IEnumerator DoPulse()
    {
        for (float e = 0f; e < Mathf.PI; e += Time.deltaTime * 1.5f)
        {
            _startText.color = new Color(1f, 1f, 1f, Mathf.Sin(e));
            yield return new WaitForEndOfFrame();
        }

        _startText.color = new Color(1f, 1f, 1f, 0f);
        yield return new WaitForSeconds(.5f);
        _lastPulse = StartCoroutine(DoPulse());
    }

    private void FadeOut()
    {
        StopCoroutine(_lastPulse);
        _startText.CrossFadeAlpha(0f, 0.5f, true);
    }
}
