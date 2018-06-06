// Draws a texture in the left corner of the screen.
// The texture is drawn in a window 60x60 pixels.
// The source texture is given an aspect ratio of 10x1
// and scaled to fit in the 60x60 rectangle.  Because
// the aspect ratio is preserved, the texture will fit
// inside a 60x10 pixel area of the screen rectangle.

using UnityEngine;
using System.Collections;

public class drawrendertexture : MonoBehaviour
{
    public Texture aTexture;

    void OnGUI()
    {
        if (!aTexture)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }

        GUI.DrawTexture(new Rect(10, 10, 60, 60), aTexture, ScaleMode.ScaleToFit, true, 10.0F);
    }
}