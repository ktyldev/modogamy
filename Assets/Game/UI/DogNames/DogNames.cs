using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogNames : MonoBehaviour {
    Ray ray;
    RaycastHit hit;

    Dog hoverDog;

    [SerializeField]
    bool _followCursor;

    [SerializeField]
    Text _text;

    [SerializeField]
    Vector3 _offset;

    [SerializeField]
    float fuzziness;

    [SerializeField]
    [Range(0, 2f)]
    float _hangTime;
    float _hangingTime;

    private bool IsHanging
    {
        get
        {
            return _hangingTime > 0f;
        }
    }

    private Camera _mainCam
    {
        get
        {
            return Camera.main.GetComponent<Camera>();
        }
    }

	void Update () {
        if (GameController.IsUsingPhone)
        {
            hoverDog = null;
            return;
        }

        ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, fuzziness, out hit))
        {
            var hitDog = hit.collider.GetComponentInParent<Dog>();
            if (hitDog != null)
            {
                hoverDog = hitDog;
                _hangingTime = _hangTime;
            }
            else
            {
                if (!IsHanging)
                {
                    hoverDog = null;
                }
            }
        }
        else
        {
            if (!IsHanging)
            {
                hoverDog = null;
            }
        }

        _hangingTime = Mathf.Clamp(_hangingTime - Time.deltaTime, 0f, _hangTime);

        if (_followCursor)
        {
            _text.transform.position = Input.mousePosition + _offset;
        }
    }

    void OnGUI()
    {
        _text.text = (hoverDog == null) ? "" : hoverDog.Profile.Name;
    }
}
