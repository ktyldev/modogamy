using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject _trackedObject;
    [SerializeField]
    [Range(0, 1)]
    private float _sensitivity;
    [SerializeField]
    private float _lookOffsetV;

    private Vector3 _offset;
    private PlayerController _player;
    
    void Start()
    {
        _offset = transform.position - _trackedObject.transform.position;
        _player = _trackedObject.GetComponent<PlayerController>();
        if (_player == null)
            throw new System.Exception();
    }
    
    void LateUpdate()
    {
        var dog = _player.Dog;
        var targetPosition = Vector3.zero;
        if (dog != null)
        {
            // Get the position halfway between the two
            targetPosition = Vector3.Lerp(_trackedObject.transform.position, dog.transform.position, .5f);
        }
        else
        {
            targetPosition = _trackedObject.transform.position;
        }
        
        transform.position = Vector3.Lerp(transform.transform.position, targetPosition + _offset, _sensitivity);
        transform.LookAt(targetPosition + Vector3.up * _lookOffsetV);
    }
}
