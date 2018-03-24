using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dog : MonoBehaviour
{
    [SerializeField]
    private GameObject _graphics;
    [SerializeField]
    private int _minWalkTime;
    [SerializeField]
    private int _maxWalkTime;

    public UnityEvent Leave { get; set; }

    public float Size { get; set; }
    public string Name { get; set; }
    public string Like { get; set; }

    private int _lifetime;

    private void Awake()
    {
        Leave = new UnityEvent();
    }

    void Start()
    {
        _graphics.transform.localScale *= Size;
        StartCoroutine(WalkTimer());
    }

    void Update()
    {
        
    }

    IEnumerator WalkTimer()
    {
        var walkLength = Random.Range(_minWalkTime, _maxWalkTime);
        yield return new WaitForSeconds(walkLength);

        Leave.Invoke();
    }
}
