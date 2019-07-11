using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorManager : MonoBehaviour {
    [SerializeField]
    Texture2D _cursor;

    private CursorMode _cursorMode = CursorMode.Auto;

    void OnEnable()
    {
        Cursor.SetCursor(_cursor, Vector2.zero, _cursorMode);
    }

    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, _cursorMode);
    }
}
