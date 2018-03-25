using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _treeTemplates;
    [SerializeField]
    private int _treeCount;
    [SerializeField]
    private float _treeSeparation;
    [SerializeField]
    private GameObject[] _flowerTemplates;
    [SerializeField]
    private int _flowerCount;
    [SerializeField]
    private float _flowerSeparation;
    [SerializeField]
    private float _flowerScale;
    [SerializeField]
    private GameObject[] _grassTemplates;
    [SerializeField]
    private int _grassCount;
    [SerializeField]
    private float _grassSeparation;
    [SerializeField]
    private float _grassScale;
    [SerializeField]
    private int _failedAttempts;

    private List<Vector3> _treeLocations = new List<Vector3>();
    private List<Vector3> _flowerLocations = new List<Vector3>();
    private List<Vector3> _grassLocations = new List<Vector3>();

    void Start()
    {
        SpawnTrees();
        SpawnFlowers();
        SpawnGrass();
    }
    
    private void SpawnTrees()
    {
        var failures = 0;
        while (failures < _failedAttempts && _treeLocations.Count() < _treeCount)
        {
            // get a location for the tree
            var targetLocation = GetLocationInPark();
            // check if it's too close to another tree
            if (_treeLocations.Any(p => Vector3.Distance(p, targetLocation) < _treeSeparation))
            {
                failures++;
                continue;
            }

            _treeLocations.Add(targetLocation);
            var treeTemplate = _treeTemplates[Random.Range(0, _treeTemplates.Length)];

            // get a random rotation
            //var rot = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            var rot = Quaternion.AngleAxis(Random.Range(0, 4) * 90, Vector3.up);
            Instantiate(treeTemplate, targetLocation, rot);
        }
    }

    private void SpawnFlowers()
    {
        var failures = 0;
        while (failures < _failedAttempts && _flowerLocations.Count() < _flowerCount)
        {
            var targetLocation = GetLocationInPark();
            if (_flowerLocations.Any(p => Vector3.Distance(p, targetLocation) < _flowerSeparation))
            {
                failures++;
                continue;
            }

            _flowerLocations.Add(targetLocation);
            var template = _flowerTemplates[Random.Range(0, _flowerTemplates.Length)];

            // get a random rotation
            var rot = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            var flower = Instantiate(template, targetLocation, rot);
            flower.transform.localScale = Vector3.one * _flowerScale;
        }
    }

    private void SpawnGrass()
    {
        var failures = 0;
        while (failures < _failedAttempts && _grassLocations.Count() < _grassCount)
        {
            var targetLocation = GetLocationInPark();
            if (_grassLocations.Any(p => Vector3.Distance(p, targetLocation) < _grassSeparation))
            {
                failures++;
                continue;
            }

            _grassLocations.Add(targetLocation);
            var template = _grassTemplates[Random.Range(0, +_grassTemplates.Length)];

            // get a random rotation
            var rot = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            var grass = Instantiate(template, targetLocation, rot);
            grass.transform.localScale = Vector3.one * _grassScale;
        }
    }

    private Vector3 GetLocationInPark()
    {
        var bounds = GameController.ParkBounds;
        return new Vector3
        {
            x = Random.Range(bounds.min.x, bounds.max.x),
            z = Random.Range(bounds.min.z, bounds.max.z)
        };
    }
}
