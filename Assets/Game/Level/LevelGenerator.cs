using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _levelRoot;
    [SerializeField]
    private GameObject _parkTile;
    [SerializeField]
    private float _offsetRight;

    // Use this for initialization
    void Start()
    {
        var levelLength = 10;

        for (int i = 1; i <= levelLength; i++)
        {
            var tile = Instantiate(_parkTile, _levelRoot);

            var translation = -Vector3.right * (i + 1) * _offsetRight;

            tile.transform.Translate(translation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
