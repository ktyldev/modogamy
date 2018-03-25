using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class GameController : MonoBehaviour {
    [SerializeField]
    private bool _usingPhone = false;
    [SerializeField]
    private Bounds _levelBounds;

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
    
    public static Bounds LevelBounds
    {
        get { return Instance._levelBounds; }
    }

    void Update()
    {
        if (!_usingPhone && Input.GetKeyDown(KeyCode.Space))
        {
            IsUsingPhone = true;
        }
    }
}
