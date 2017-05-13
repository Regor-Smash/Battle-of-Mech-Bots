using UnityEngine;

public class AnimateTexture : MonoBehaviour
{
    public int columns;
    public int rows;

    public float framesPerSecond = 30.0f;

    void Update()
    {
        // Calculate index
        int index = (int)(Time.time * framesPerSecond);
        // repeat when exhausting all frames
        index = index % (columns * rows);

        // Size of every tile
        Vector2 size = new Vector2(1.0f / columns, 1.0f / rows);

        // split into horizontal and vertical index
        float uIndex = index % columns;
        float vIndex = index / columns;

        // build offset
        // v coordinate is the bottom of the image in opengl so we need to invert.
        Vector2 offset = new Vector2(uIndex * size.x, 1.0f - size.y - vIndex * size.y);

        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
        GetComponent<Renderer>().material.SetTextureScale("_MainTex", size);
    }
}
