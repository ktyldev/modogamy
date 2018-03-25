using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneButton : MonoBehaviour {
    float _scale = 1f;
	
	void Update () {
        _scale = Mathf.Lerp(_scale, 1f, 0.1f);
        transform.localScale = Vector3.one * _scale;
	}

    public void Pulse ()
    {
        _scale *= 1.2f;
    }
}
