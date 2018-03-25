using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _templates;
    [SerializeField]
    private int _treeCount;
    [SerializeField]
    private float _treeSeparation;
    [SerializeField]
    private int _failedAttempts;

    private List<Vector3> _treeLocations;

    private void Awake()
    {
        _treeLocations = new List<Vector3>();
    }

    void Start()
    {
        SpawnTrees();
    }
    
    private void SpawnTrees()
    {
        var bounds = GameController.ParkBounds;
        var failures = 0;
        while (failures < _failedAttempts && _treeLocations.Count() < _treeCount)
        {
            // get a location for the tree
            var x = Random.Range(bounds.min.x, bounds.max.x);
            var z = Random.Range(bounds.min.z, bounds.max.z);

            var targetLocation = new Vector3
            {
                x = x,
                z = z
            };

            // check if it's too close to another tree
            if (_treeLocations.Any(p => Vector3.Distance(p, targetLocation) < _treeSeparation))
            {
                failures++;
                continue;
            }

            _treeLocations.Add(targetLocation);
            var treeTemplate = _templates[Random.Range(0, _templates.Length)];

            // get a random rotation
            var rot = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            Instantiate(treeTemplate, targetLocation, rot);
        }
    }
}
