using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _amplitude;

    private bool _isBouncing;
    
    // Use this for initialization
    void Start()
    {
        var bouncer = GetComponentInParent<IBouncer>();
        if (bouncer == null)
            throw new System.Exception();

        bouncer.StartBouncing.AddListener(StartBounce);
        bouncer.StopBouncing.AddListener(StopBounce);
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
        while (_isBouncing)
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
