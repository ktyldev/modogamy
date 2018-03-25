using Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private float _despawnDistance;

    [SerializeField]
    [Range(0f, 1f)]
    private float _lookLockTime;
    private float _lookTimer = 0;

    private bool LookIsLocked { get { return _lookTimer > 0f; } }
    
    // Where the dog wants to run
    private Vector3 _targetMomentum;
    private Vector3 _momentum;

    private Transform _target;
    private Vector3 _targetPosition;

    private bool _isLeaving;
    private Dog _dog;
    private DogQuote _quotes;

    // Use this for initialization
    void Start()
    {
        _dog = GetComponent<Dog>();
        _target = this.Find(GameTags.Player).transform;
        _quotes = this.FindInChild<DogQuote>(GameTags.UI);

        StartCoroutine(UpdateTarget());

        _dog.Leave.AddListener(() => _isLeaving = true);
    }

    // Update is called once per frame
    void Update()
    {
        _momentum = Vector3.Lerp(_momentum, _targetMomentum, _acceleration);

        SetLookTarget(_targetPosition);
        transform.Translate(_momentum * Time.deltaTime, Space.World);
        
        // Nice hack
        if (transform.position.z < -1)
        {
            SetNewTargetPosition();
        }

        var bounds = GameController.ParkBounds;
        if (_target.transform.position.x < bounds.min.x || _target.transform.position.x > bounds.max.x)
        {
            SetNewTargetPosition();
        }


        if (Vector3.Distance(_target.position, transform.position) > _despawnDistance)
        {
            _dog.Leave.Invoke();
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

    private IEnumerator UpdateTarget()
    {
        SetNewTargetPosition();

        float targetSpeed = _maxSpeed;
        Vector3 targetDir = Vector3.zero;

        while (!_isLeaving)
        {
            var arrivedAtTarget = Vector3.Distance(
                _target.position,
                transform.position) < _minDistanceFromPlayer;

            if (arrivedAtTarget)
            {
                _quotes.ShowQuote(_dog);
            }

            arrivedAtTarget = arrivedAtTarget || Vector3.Distance(
                _target.position,
                _targetPosition) < _minDistanceFromTarget;

            var tooFarAway = Vector3.Distance(
                _target.position,
                transform.position) > _maxDistanceFromPlayer;

            if (arrivedAtTarget || tooFarAway)
            {
                SetNewTargetPosition();
            }
            
            yield return new WaitForEndOfFrame();
        }
    }

    private Vector3 GetMoveDir()
    {
        return _targetPosition - transform.position;
    }

    private void SetNewTargetPosition()
    {
        var bounds = GameController.ParkBounds;
        var z = 0f;
        var dirToParkCentre = Vector3.zero;

        var positions = new[] { _target.transform.position, transform.position };
        if (positions.All(p => p.x > bounds.min.x && p.x < bounds.max.x))
        {
            z = Random.Range(bounds.min.z, bounds.max.z);
        } 
        else
        {
            dirToParkCentre = (bounds.center - transform.position).normalized;
        }

        var skewAmount = 0.5f;
        float skew = 1;
        if (dirToParkCentre.x > 0)
        {
            skew = skewAmount;
        }
        else if (dirToParkCentre.x < 0)
        {
            skew = 2 - skewAmount;
        }
        
        // skew targets towards center of park
        var x = (Random.value * 2 - skew) * _maxTargetOffset;
        _targetPosition = new Vector3 { x = _target.position.x + x, z = z };

        var targetSpeed = Random.Range(_minSpeed, _maxSpeed);
        var targetDir = (_targetPosition - transform.position).normalized;
        _targetMomentum = targetDir * targetSpeed;
    }
}
