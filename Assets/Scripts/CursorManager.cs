using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorNormal;
    public Vector2 cursorNormalHotspot;
    public Texture2D cursorInteract;
    public Vector2 cursorInteractHotspot;

    public static CursorManager instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Cursor.SetCursor(cursorNormal, cursorNormalHotspot, CursorMode.Auto);
    }

    public void SetCursorNormal()
    {
        Cursor.SetCursor(cursorNormal, cursorNormalHotspot, CursorMode.Auto);
    }

    public void SetCursorInteract()
    {
        Cursor.SetCursor(cursorInteract, cursorInteractHotspot, CursorMode.Auto);
    }
}
