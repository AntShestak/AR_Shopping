using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetManager : MonoBehaviour
{
    //public Vector2 LineStartOffset { get; private set; }
    public Vector2 LineEndOffset { get; private set; }

    public Vector2 InfoBoxPivot { get; private set; }

    private void Start()
    {
        //LineStartOffset = Vector2.zero;
        //LineEndOffset = Vector2.zero;
        //InfoBoxPivot = Vector2.zero;
    }
    

    public void SetOffsetFromTouchPosition(Vector2 touchPixelPosition)
    {
        ScreenConsole.Instance.Log($"Touch pixel is {touchPixelPosition}");

        //LineStartOffset = Vector2.zero;
        LineEndOffset = Vector2.zero;
        InfoBoxPivot = Vector2.up * 0.8f;

        Resolution res = Screen.currentResolution;


        if (touchPixelPosition.x <= res.width / 2)
        {
            //we are on the left side - move to the RIGHT
            LineEndOffset += Vector2.right;
            
        }
        else
        {
            LineEndOffset += Vector2.left;
            InfoBoxPivot += Vector2.right;
        }

        if (touchPixelPosition.y <= res.height / 2)
        {
            //we are on the bottom half - move towards TOP
            LineEndOffset += Vector2.up * 0.6f;
        }
        else
        {
            LineEndOffset += Vector2.down * 0.6f;
            //InfoBoxOffset += Vector2.down * 5f;
        }

        ScreenConsole.Instance.Log($"Line offset: {LineEndOffset} Pivot: {InfoBoxPivot}");

    }
}
