using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour {
    [SerializeField]
    private GameObject _welcome;
    private Image _welcomeImage;
    [SerializeField]
    private float timeTillFade;

    private float _currentBrightness = 1f;
    private Color _currentColor;

    private void Start()
    {
        _welcomeImage = _welcome.GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(timeTillFade);
        while (_currentBrightness > 0.001f)
        {
            _currentBrightness = Mathf.Lerp(_currentBrightness, 0f, 0.1f);
            _welcomeImage.color = new Color(1f, 1f, 1f, _currentBrightness);
            yield return new WaitForEndOfFrame();
        }
        Destroy(_welcome.gameObject);
    }
}
