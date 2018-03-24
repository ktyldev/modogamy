using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField]
    private GameObject _graphics;

    public float Size { get; set; }

    void Start()
    {
        _graphics.transform.localScale *= Size;
    }

    void Update()
    {

    }
}
