using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DogAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _animationFrames;
    [SerializeField]
    private float _frameLength;

    private GameObject[] _frames;

    // Use this for initialization
    void Start()
    {
        _frames = _animationFrames
            .Select(go => Instantiate(go, transform))
            .ToArray();

        foreach (var frame in _frames)
        {
            frame.SetActive(false);
        }

        StartCoroutine(Animate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Animate()
    {
        int i = 0;
        while (true)
        {
            int last = i == 0 ? _frames.Length - 1 : i - 1;
            _frames[last].SetActive(false);
            _frames[i].SetActive(true);

            yield return new WaitForSeconds(_frameLength);

            i++;
            if (i == _animationFrames.Length)
            {
                i = 0;
            }
        }
    }
}
