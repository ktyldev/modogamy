using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class DogFactory : MonoBehaviour {
    public static int minSize = 10;
    public static int maxSize = 50;
    public static double lambda = 25;
    int size;

    int getSize()
    {
        // first, do a rough poisson calculation
        double p = 1.0, L = Math.Exp(-lambda);
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

    void Awake ()
    {
    }

    void Update ()
    {
        size = getSize();
        print(size);
    }
}
