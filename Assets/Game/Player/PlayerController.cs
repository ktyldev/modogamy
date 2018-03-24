using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, IBouncer
{
    [SerializeField]
    private float _walkSpeed;
    
    private bool HasInput { get { return Input.GetAxis(GameTags.Horizontal) != 0; } }
    public bool IsBouncing { get { return HasInput; } }
    
    void Update()
    {
        if (GameController.IsUsingPhone)
            return;
        
        Walk();
    }

    private void Walk()
    {
        var movementAxis = Input.GetAxis(GameTags.Horizontal);
        var movement = new Vector3
        {
            x = movementAxis * _walkSpeed
        };

        transform.Translate(movement * Time.deltaTime);

        if (movementAxis == 0f)
            return;

        // flip the player in their movement direction
        transform.localScale = new Vector3((movementAxis < 0f) ? -1 : 1, 1, 1);
    }
}
