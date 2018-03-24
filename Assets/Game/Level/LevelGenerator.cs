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
    private float _startOffset;
    [SerializeField]
    private float _tileLength;

    // Use this for initialization
    void Start()
    {
        var levelLength = 10;

        for (int i = 0; i < levelLength; i++)
        {
            var tile = Instantiate(_parkTile, _levelRoot);

            var translation = -Vector3.right * ((i + 0.5f) * _tileLength + _startOffset);

            tile.transform.Translate(translation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
