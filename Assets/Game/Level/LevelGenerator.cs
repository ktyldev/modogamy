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
    private float _startOffset;
    [SerializeField]
    private float _tileLength;
    [SerializeField]
    private int _levelLength;
    
    void Start()
    {
        for (int i = 0; i < _levelLength; i++)
        {
            GameObject template = null;
            if (i == 0)
            {
                template = _cityParkTransition;
            }
            else if (i == _levelLength - 2)
            {
                template = _parkCityTransition;
            }
            else if (i == _levelLength - 1)
            {
                template = _end;
            }
            else
            {
                template = _park;
            }
            
            var tile = Instantiate(template, _levelRoot);
            var translation = -Vector3.right * ((i + 0.5f) * _tileLength + _startOffset);
            tile.transform.Translate(translation);
        }
    }
}
