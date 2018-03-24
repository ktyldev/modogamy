using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dog))]
public class DogMovement : MonoBehaviour
{
    [SerializeField]
    private float _minSpeed;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _maxTargetOffset;
    [SerializeField]
    [Range(0, 1)]
    private float _acceleration;
    [SerializeField]
    private float _minDistanceFromTarget;
    [SerializeField]
    private float _minDistanceFromPlayer;
    [SerializeField]
    private float _maxDistanceFromPlayer;

    [SerializeField]
    [Range(0f, 1f)]
    private float _lookLockTime;
    private float _lookTimer = 0;

    private bool LookIsLocked { get { return _lookTimer > 0f;  } }

    // Where the dog wants to run
    private Vector3 _targetMomentum;
    private Vector3 _momentum;

    private Transform _target;
    private Vector3 _targetPosition;

    private bool _isLeaving;

    // Use this for initialization
    void Start()
    {
        _target = this.Find(GameTags.Player).transform;

        StartCoroutine(SetMoveTarget());

        GetComponent<Dog>().Leave.AddListener(() => _isLeaving = true);
    }

    // Update is called once per frame
    void Update()
    {
        _momentum = Vector3.Lerp(_momentum, _targetMomentum, _acceleration);

        SetLookTarget(_targetPosition);
        transform.Translate(_momentum * Time.deltaTime, Space.World);

        if (Vector3.Distance(_target.position, transform.position) > 20)
        {
            Destroy(gameObject);
        }
    }

    private void SetLookTarget(Vector3 target)
    {
        if (!LookIsLocked)
        {
            transform.LookAt(target);
            _lookTimer = _lookLockTime;
        }

        _lookTimer = Mathf.Clamp(_lookTimer - Time.deltaTime, 0f, _lookLockTime);
    }

    private IEnumerator SetMoveTarget()
    {
        SetNewTargetPosition();

        float targetSpeed = _maxSpeed;

        while (!_isLeaving)
        {
            var arrivedAtTarget = Vector3.Distance(
                _target.position, 
                transform.position) < _minDistanceFromPlayer;
            arrivedAtTarget = arrivedAtTarget || Vector3.Distance(
                _target.position,
                _targetPosition) < _minDistanceFromTarget;

            var tooFarAway = Vector3.Distance(
                _target.position, 
                transform.position) > _maxDistanceFromPlayer;

            if (arrivedAtTarget || tooFarAway)
            {
                SetNewTargetPosition();
                targetSpeed = Random.Range(_minSpeed, _maxSpeed) * GetMoveDir();
            }

            // If we are to the right of the target, move left
            _targetMomentum = new Vector3 { x = targetSpeed };

            yield return new WaitForEndOfFrame();
        }
    }

    private float GetMoveDir()
    {
        return (_targetPosition - transform.position).x < 0 ? -1 : 1;
    }

    private void SetNewTargetPosition()
    {
        var offset = (Random.value * 2 - 1) * _maxTargetOffset;
        _targetPosition = _target.position + new Vector3 { x = offset };
    }
}
