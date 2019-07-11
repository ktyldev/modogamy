using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField]
    private GameObject _bounceController;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _amplitude;

    private IBouncer _bouncer;
    private bool _isBouncing;
    
    // Use this for initialization
    void Start()
    {
        _bouncer = _bounceController.GetComponent<IBouncer>();
        if (_bouncer == null)
            throw new System.Exception();
    }

    private void Update()
    {
        if (_bouncer.IsBouncing && !_isBouncing)
        {
            StartBounce();
        }
        else if (!_bouncer.IsBouncing && _isBouncing)
        {
            StopBounce();
        }
    }

    private void StartBounce()
    {
        _isBouncing = true;
        StartCoroutine(DoBounce());
    }

    private void StopBounce()
    {
        _isBouncing = false;
    }
    
    private IEnumerator DoBounce()
    {
        while (_bouncer.IsBouncing)
        {
            for (float e = 0f; e < Mathf.PI; e += Time.deltaTime * _speed)
            {
                transform.position = new Vector3
                {
                    x = transform.position.x,
                    y = Mathf.Pow(Mathf.Sin(e), 2) * _amplitude,
                    z = transform.position.z
                };

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
