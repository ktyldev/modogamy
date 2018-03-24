using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform _levelRoot;
    [SerializeField]
    private GameObject _cityParkTransition;
    [SerializeField]
    private GameObject _parkTile;
    [SerializeField]
    private float _startOffset;
    [SerializeField]
    private float _tileLength;
    [SerializeField]
    private int _levelLength;

    private int _tileIndex;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < _levelLength; i++)
        {
            var tile = Instantiate(i == 0 ? _cityParkTransition : _parkTile, _levelRoot);

            var translation = -Vector3.right * ((i + 0.5f) * _tileLength + _startOffset);

            tile.transform.Translate(translation);
        }
    }
}
