using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class DogFactory : MonoBehaviour
{
    [SerializeField]
    private int minSize = 10;
    [SerializeField]
    private int maxSize = 50;
    [SerializeField]
    private double sizeLambda = 25;

    private int _size;
    
    void Awake()
    {
    }

    void Update()
    {
        print(GetRandomSize());
    }

    private int GetRandomSize()
    {
        // first, do a rough poisson calculation
        double p = 1.0, L = Math.Exp(-sizeLambda);
        int k = 0;
        do
        {
            k++;
            p *= UnityEngine.Random.value;
        }
        while (p > L);
        k--;
        // now we've got our poisson, let's clamp it
        return Mathf.Clamp(k, minSize, maxSize);
    }
}
