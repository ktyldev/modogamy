using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class GameController : MonoBehaviour {
    [SerializeField]
    private bool _usingPhone = false;
    private static GameController Instance { get; set; }

    void Awake()
    {
        if (Instance != null)
        {
            throw new System.Exception();
        }

        Instance = this;
    }

    public static bool IsUsingPhone
    {
        get { return Instance._usingPhone; }
        set { Instance._usingPhone = value; }
    }
}
