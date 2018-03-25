using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class GameController : MonoBehaviour {
    [SerializeField]
    private bool _usingPhone = false;
    [SerializeField]
    private Bounds _levelBounds;
    [SerializeField]
    private Bounds _parkBounds;

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

    public static Bounds ParkBounds
    {
        get { return Instance._parkBounds; }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsUsingPhone = !IsUsingPhone;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
