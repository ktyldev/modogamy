using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed;

    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        var movement = new Vector3
        {
            x = Input.GetAxis(GameTags.Horizontal) * _walkSpeed
        }; 

        transform.Translate(movement * Time.deltaTime);
    }
}
