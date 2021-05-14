using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D streamCursor;

    void Start()
    {
        Cursor.SetCursor(streamCursor, Vector2.zero, CursorMode.Auto);
    }

}
