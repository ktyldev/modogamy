using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField]
    private GameObject _graphics;
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _maxTargetOffset;
    
    public float Size { get; set; }
    public string Name { get; set; }
    public string Like { get; set; }

    private Transform _target;
    private Vector3 _targetPosition;

    void Start()
    {
        _graphics.transform.localScale *= Size;
        _target = this.Find(GameTags.Player).transform;

        StartCoroutine(SetMoveTarget());
    }

    void Update()
    {
        transform.LookAt(_targetPosition);
        transform.Translate(Vector3.forward * _walkSpeed * Time.deltaTime);
    }

    private IEnumerator SetMoveTarget()
    {
        while (true)
        {
            if (Vector3.Distance(_targetPosition, transform.position) < 0.5f)
            {
                var offset = (Random.value * 2 - 1) * _maxTargetOffset;
                _targetPosition = _target.position + new Vector3 { x = offset };
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
