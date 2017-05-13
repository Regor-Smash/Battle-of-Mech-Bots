using UnityEngine;

public class WubColorChange : MonoBehaviour
{
    public ColorData colorMang;

    bool isChanging;
    float t = 0;

    Color startColor;
    Color newColor;
    Color oldColor;
    Color currentColor;

    public Material glowMat;
    public Light glowLight;

    public float interval;

    void Start()
    {
        startColor = glowMat.color;
        oldColor = colorMang.wubGlowColors[0];
        RandomColor();
        isChanging = true;
    }

    void FixedUpdate()
    {
        if (isChanging)
        {
            currentColor = Color.Lerp(oldColor, newColor, t);
            t = t + interval;
            glowMat.color = currentColor;
            glowLight.color = currentColor;
            if (t >= 1)
            {
                isChanging = false;
            }
        }
        else if (!isChanging)
        {
            oldColor = newColor;
            RandomColor();
            t = 0;
            isChanging = true;
        }
    }

    void RandomColor()
    {
        newColor = colorMang.wubGlowColors[(int)Random.Range(0, colorMang.wubGlowColors.Length)];
        while (newColor == oldColor)
        {
            newColor = colorMang.wubGlowColors[(int)Random.Range(0, colorMang.wubGlowColors.Length)];
        }
    }

    private void OnDestroy()
    {
        glowMat.color = startColor;
    } 
}
