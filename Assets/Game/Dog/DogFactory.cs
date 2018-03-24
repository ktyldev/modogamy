using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class DogFactory : MonoBehaviour
{
    [SerializeField]
    private GameObject _dog;
    [SerializeField]
    private int _minSize = 10;
    [SerializeField]
    private int _maxSize = 50;
    [SerializeField]
    private double _sizeLambda = 25;
    [SerializeField]
    private float _dogScale = 0.05f;

    private string[] _nameList;
    private int _nameCount;

    private string[] _likeList;
    private int _likeCount;

    void Awake()
    {
        _nameList = File.ReadAllLines("Assets/Resources/names.txt");
        _nameCount = _nameList.Length;

        _likeList = File.ReadAllLines("Assets/Resources/likes.txt");
        _likeCount = _likeList.Length;
    }

    void Start()
    {
        StartCoroutine(GenerateDogs());
    }

    // Temporary until we can generate dogs properly
    private IEnumerator GenerateDogs()
    {
        var delay = 2;
        var maxOffset = 10f;

        while (true)
        {
            var dog = GetNewDog();
            var offset = new Vector3
            {
                x = (UnityEngine.Random.value * 2 - 1) * maxOffset
            };

            dog.transform.Translate(offset);
            yield return new WaitForSeconds(delay);
        }
    }

    void Update()
    {

    }

    public Dog GetNewDog()
    {
        var dog = Instantiate(_dog).GetComponent<Dog>();
        dog.Size = GetRandomSize();
        dog.Name = GetRandomName();
        dog.Like = GetRandomLike();

        print(dog.Name + " likes " + dog.Like);

        return dog;

    }

    private float GetRandomSize()
    {
        // first, do a rough poisson calculation
        double p = 1.0, L = Math.Exp(-_sizeLambda);
        int k = 0;
        do
        {
            k++;
            p *= UnityEngine.Random.value;
        }
        while (p > L);
        k--;
        // now we've got our poisson, let's clamp it
        return Mathf.Clamp((float)k, _minSize, _maxSize) * _dogScale;
    }

    private string GetRandomName()
    {
        string name = _nameList[UnityEngine.Random.Range(0, _nameCount)];
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower().Trim());
    }

    private string GetRandomLike()
    {
        string like = _likeList[UnityEngine.Random.Range(0, _likeCount)];
        return like.ToLower().Trim();
    }
}
