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

    private Camera _mainCam
    {
        get
        {
            return Camera.main.GetComponent<Camera>();
        }
    }

	void Update () {
        ray = _mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.SphereCast(ray, fuzziness, out hit))
        {
            hoverDog = hit.collider.GetComponentInParent<Dog>();
            if (hoverDog != null)
            {
                print(hoverDog.Name);
            }
        }
        else
        {
            hoverDog = null;
        }

        if (_followCursor)
        {
            _text.transform.position = Input.mousePosition + _offset;
        }
    }

    void OnGUI()
    {
        _text.text = (hoverDog == null) ? "" : hoverDog.Name;
    }
}
