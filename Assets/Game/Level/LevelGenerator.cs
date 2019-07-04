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
    private GameObject _park;
    [SerializeField]
    private GameObject _parkCityTransition;
    [SerializeField]
    private GameObject _end;
    [SerializeField]
    private float _tileLength;
    [SerializeField]
    private int _levelLength;
    
    void Start()
    {
        float start = -_levelLength * _tileLength;

        for (int i = 0; i < _levelLength; i++)
        {
            var tile = Instantiate(_park, _levelRoot);
            var translation = Vector3.right * ((i + 0.5f) * _tileLength + start);
            tile.transform.Translate(translation);
        }
    }
}
