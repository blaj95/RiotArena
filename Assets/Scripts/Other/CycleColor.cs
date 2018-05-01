using UnityEngine;
using System.Collections;

public class ColorLerp : MonoBehaviour
{
    public Color[] colors;
    public float duration = 3.0f;

    private int index = 0;
    private float timer = 0.0f;
    private Color currentColor;
    private Color startColor;
    Renderer myRend;

    Light Light;

    void Start()
    {
        myRend = gameObject.GetComponent<Renderer>();
        gameObject.GetComponent<Light>();
        myRend.material.color = (colors[0]);
    }

    void Update()
    {
        currentColor = Color.Lerp(startColor, colors[index], timer);

        timer += Time.deltaTime / duration;
        if (timer > 1.0f)
        {
            timer -= 1.0f;
            index++;
            if (index >= colors.Length)
                index = 0;
            startColor = currentColor;
        }
    }
}