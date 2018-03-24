using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct DogProfile
{
    public string Name { get; set; }
    public float Size { get; set; }
    public string Like { get; set; }
    public int Index { get; set; }
}

public class Dog : MonoBehaviour, IBouncer
{
    public GameObject graphics;

    [SerializeField]
    private int _minWalkTime;
    [SerializeField]
    private int _maxWalkTime;
    
    public UnityEvent Leave { get; private set; }

    public DogProfile Profile { get; set; }

    public bool IsBouncing { get { return true; } }

    private void Awake()
    {
        Leave = new UnityEvent();
    }

    void Start()
    {
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
