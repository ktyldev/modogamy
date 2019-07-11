using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class PostFXHandler : MonoBehaviour {

    private GameController _controller;

    [SerializeField]
    [Range(.1f, 32f)]
    private float _normalAperture;
    [SerializeField]
    [Range(.1f, 32f)]
    private float _phoneAperture;
    private float _delta = 0.015f;

    private float _targetAperture;

    void Start () {
        _controller = this.Find<GameController>(GameTags.GameController);
        _controller.StartGame.AddListener(() => StartCoroutine(SpeedUp()));
	}

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(1f);
        _delta = 0.2f;
    }
}
