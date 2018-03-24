using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSlide : MonoBehaviour
{
    [SerializeField]
    private float _slideOutOffset;
    [SerializeField]
    [Range(0, 1)]
    private float _slideSpeed;

    private float _slideInY;
    private float _slideOutY;
    private float _targetY;

    // Use this for initialization
    void Start()
    {
        // Grab the initial position for slid in position
        _slideInY = transform.position.y;
        _slideOutY = _slideInY + _slideOutOffset;

        // Hidden off screen to start with
        _targetY = _slideOutY;
        transform.position = new Vector3
        {
            x = transform.position.x,
            y = _slideOutY,
            z = transform.position.z
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_targetY == _slideInY)
            {
                SlideOut();
            }
            else if (_targetY == _slideOutY)
            {
                SlideIn();
            }
        }
        
        var targetPositon = new Vector3 {
            x = transform.position.x,
            y = _targetY,
            z = transform.position.z
        };

        transform.position = Vector3.Lerp(transform.position, targetPositon, _slideSpeed);
    }

    public void SlideIn()
    {
        GameController.IsUsingPhone = true;
        _targetY = _slideInY;
    }

    public void SlideOut()
    {
        GameController.IsUsingPhone = false;
        _targetY = _slideOutY;
    }
}
