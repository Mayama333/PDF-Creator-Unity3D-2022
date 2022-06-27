using HSVPicker;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundColor : MonoBehaviour
{
    public Image background;
    // Color
    public Color Color = Color.red;
    public ColorPicker picker;

    public bool SetColorOnStart = false;

    // Start is called before the first frame update
    void Start()
    {
        // Color
        picker.onValueChanged.AddListener(color =>
        {
            background.color = color; //text
                Color = color;
        });

        background.color = picker.CurrentColor; //text
        if (SetColorOnStart)
        {
            picker.CurrentColor = Color;
        }
    }
}
