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
    public float Pitch { get; set; }
    public string[] Quotes { get; set; }
}

public class Dog : MonoBehaviour, IBouncer
{
    SFXManager _sfxManager;
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
        _sfxManager = this.Find<SFXManager>(GameTags.Audio);
        Bork();

        var player = this.Find<PlayerController>(GameTags.Player);
        if (player.Dog != null)
            throw new System.Exception();

        player.Dog = this;
        Leave.AddListener(() => player.Dog = null);

        StartCoroutine(WalkTimer());
    }

    public void Bork()
    {
        _sfxManager.PlaySound("Woof", Profile.Pitch);
    }

    IEnumerator WalkTimer()
    {
        var walkLength = Random.Range(_minWalkTime, _maxWalkTime);
        yield return new WaitForSeconds(walkLength);

        Bork();
        Leave.Invoke();
    }
}
