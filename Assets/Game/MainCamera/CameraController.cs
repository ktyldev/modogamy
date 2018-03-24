using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _trackedObject;
    [SerializeField]
    [Range(0, 1)]
    private float _sensitivity;
    [SerializeField]
    private float _lookOffsetV;

    private Vector3 _offset;

    // Use this for initialization
    void Start()
    {
        _offset = transform.position - _trackedObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var targetPosition = _trackedObject.transform.position + _offset;
        transform.position = Vector3.Lerp(transform.transform.position, targetPosition, _sensitivity);
        transform.LookAt(_trackedObject.transform.position + Vector3.up * _lookOffsetV);
    }
}
