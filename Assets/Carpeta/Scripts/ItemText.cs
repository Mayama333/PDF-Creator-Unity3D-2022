using HSVPicker;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemText : MonoBehaviour
{
    public TextMeshProUGUI text;

    // master
    public ControlPreparePdf controlScript;

    // Num
    public int NumPick;

    //Tamaño
    public int size = 80;

    // Color
    public string colorHx = "323232";

    public Color Color;
    public ColorPicker picker;
    public bool SetColorOnStart = false;

    // Alings
    public int lineUp = 0;
    public int lineBottom = 0;

    // Fonts
    public int numFont = 0;

    // Style
    public bool styleB;
    public bool styleI;
    public bool styleU;
    public bool styleS;
    [SerializeField] private Image[] botonesStyle;

    public TMP_FontAsset[] fontTexto;
    private List<string> m_DropOptions = new List<string>();
    [SerializeField] private TMP_Dropdown dropFont;
    [SerializeField] private bool[] style;

    [SerializeField] private Image[] botones;

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#323232", out Color);

        Color colorTest;
        if (ColorUtility.TryParseHtmlString("#" + colorHx, out colorTest))
        { text.color = colorTest; }
        
        text.fontSize = size;
        text.font = fontTexto[numFont];
        
        GetChangeAlingIn(lineUp.ToString());
        
        if (lineBottom == 0)
        {
            GetChangeAlingIn("3");
        }
        if (lineBottom == 1)
        {
            GetChangeAlingIn("4");
        }
        if (lineBottom == 2)
        {
            GetChangeAlingIn("5");
        }

        if (styleB == true)
        {
            GetChangeStyleIn("0");
        }
        if (styleI == true)
        {
            GetChangeStyleIn("1");
        }
        if (styleU == true)
        {
            GetChangeStyleIn("2");
        }
        if (styleS == true)
        {
            GetChangeStyleIn("3");
        }

        if (picker != null)
        {
            // Color
            picker.onValueChanged.AddListener(color =>
            {
                text.color = color; //text
                Color = color;
                colorHx = ColorUtility.ToHtmlStringRGB(Color);
            });

            text.color = picker.CurrentColor; //text
            if (SetColorOnStart)
            {
                picker.CurrentColor = Color;
            }

        }

        // Fonts
        dropFont.ClearOptions();
        for (int i = 0; i < fontTexto.Length; i++)
        {
            m_DropOptions.Add(fontTexto[i].name);
        }
        dropFont.AddOptions(m_DropOptions);
    }

    public void Back()
    {
        controlScript.backTo();
    }

    public void picked()
    {
        ColorUtility.TryParseHtmlString("#" + colorHx, out Color);

        controlScript.pickedCreate(NumPick);
        // Color
        picker.onValueChanged.AddListener(color =>
        {
            text.color = color; //text
            Color = color;
            colorHx = ColorUtility.ToHtmlStringRGB(Color);
        });

        text.color = picker.CurrentColor; //text
        if (SetColorOnStart)
        {
            picker.CurrentColor = Color;
        }
    }

   

    public void ChangeFontText()
    {
        text.font = fontTexto[dropFont.value];
        numFont = dropFont.value;
    }

    public void GetChangeStyleIn(string valueStyle)
    {
        Debug.Log(valueStyle);

        Color colo01;
        ColorUtility.TryParseHtmlString("#FFFFFF", out colo01);
        colo01.a = 0.42f; 

        Color colo02;
        ColorUtility.TryParseHtmlString("#FFFFFF", out colo02);
        colo02.a = 0.8f;

        for (int i = 0; i < 4; i++)
        {
            botonesStyle[i].GetComponent<Image>().color = colo01;
        }


        if (int.Parse(valueStyle) == 0 && style[0] == false)
        {
            style[0] = true;
            styleB = true;
            text.fontStyle ^= FontStyles.Bold;
        }
        else if (int.Parse(valueStyle) == 0 && style[0] == true)
        {
            style[0] = false;
            styleB = false;
            text.fontStyle -= FontStyles.Bold;
        }

        if (int.Parse(valueStyle) == 1 && style[1] == false)
        {
            style[1] = true;
            styleI = true;
            text.fontStyle ^= FontStyles.Italic;
        }
        else if (int.Parse(valueStyle) == 1 && style[1] == true)
        {
            style[1] = false;
            styleI = false;
            text.fontStyle -= FontStyles.Italic;
        }

        if (int.Parse(valueStyle) == 2 && style[2] == false)
        {
            style[2] = true;
            styleU = true;
            text.fontStyle ^= FontStyles.Underline;
        }
        else if (int.Parse(valueStyle) == 2 && style[2] == true)
        {
            style[2] = false;
            styleU = false;
            text.fontStyle -= FontStyles.Underline;
        }

        if (int.Parse(valueStyle) == 3 && style[3] == false)
        {
            style[3] = true;
            styleS = true;
            styleB = false;
            styleI = false;
            styleU = false;
            text.fontStyle = FontStyles.Strikethrough;
        }
        else if (int.Parse(valueStyle) == 3 && style[3] == true)
        {
            style[3] = false;
            styleS = false;
            text.fontStyle -= FontStyles.Strikethrough;
        }

        if (styleB == true)
        {
            botonesStyle[0].GetComponent<Image>().color = colo02;
        }
        if (styleI == true)
        {
            botonesStyle[1].GetComponent<Image>().color = colo02;
        }
        if (styleU == true)
        {
            botonesStyle[2].GetComponent<Image>().color = colo02;
        }
        if (styleS == true)
        {
            botonesStyle[3].GetComponent<Image>().color = colo02;
        }
    }

    public void GetChangeSizeIn(string valueSize)
    {
        if (int.Parse(valueSize) >= 0)
        {
            text.fontSize = int.Parse(valueSize);
            size = int.Parse(valueSize);
        }
    }

    public void GetChangeAlingIn(string valueInt)
    {
        Debug.Log(valueInt);

        if (int.Parse(valueInt) < 3)
        {
            for (int b = 0; b < 3; b++)
            {
                botones[b].color = new Color32(255, 255, 225, 110);
            }
            botones[int.Parse(valueInt)].color = new Color32(255, 255, 225, 225);
        }

        if (int.Parse(valueInt) > 2)
        {
            for (int b = 3; b < 6; b++)
            {
                botones[b].color = new Color32(255, 255, 225, 110);
            }
            botones[int.Parse(valueInt)].color = new Color32(255, 255, 225, 225);
        }

        Debug.Log(text.alignment);

        if (int.Parse(valueInt) == 0 && (text.alignment == TextAlignmentOptions.Top || text.alignment == TextAlignmentOptions.TopLeft || text.alignment == TextAlignmentOptions.TopRight))
        {
            text.alignment = TextAlignmentOptions.TopLeft;
            lineUp = 0;
        }
        if (int.Parse(valueInt) == 0 && (text.alignment == TextAlignmentOptions.Center || text.alignment == TextAlignmentOptions.Left || text.alignment == TextAlignmentOptions.Right))
        {
            text.alignment = TextAlignmentOptions.Left;
            lineUp = 0;
        }
        if (int.Parse(valueInt) == 0 && (text.alignment == TextAlignmentOptions.Bottom || text.alignment == TextAlignmentOptions.BottomLeft || text.alignment == TextAlignmentOptions.BottomRight))
        {
            text.alignment = TextAlignmentOptions.BottomLeft;
            lineUp = 0;
        }

        if (int.Parse(valueInt) == 1 && (text.alignment == TextAlignmentOptions.Top || text.alignment == TextAlignmentOptions.TopLeft || text.alignment == TextAlignmentOptions.TopRight))
        {
            text.alignment = TextAlignmentOptions.Top;
            lineUp = 1;
        }
        if (int.Parse(valueInt) == 1 && (text.alignment == TextAlignmentOptions.Center || text.alignment == TextAlignmentOptions.Left || text.alignment == TextAlignmentOptions.Right))
        {
            text.alignment = TextAlignmentOptions.Center;
            lineUp = 1;
        }
        if (int.Parse(valueInt) == 1 && (text.alignment == TextAlignmentOptions.Bottom || text.alignment == TextAlignmentOptions.BottomLeft || text.alignment == TextAlignmentOptions.BottomRight))
        {
            text.alignment = TextAlignmentOptions.Bottom;
            lineUp = 1;
        }

        if (int.Parse(valueInt) == 2 && (text.alignment == TextAlignmentOptions.Top || text.alignment == TextAlignmentOptions.TopLeft || text.alignment == TextAlignmentOptions.TopRight))
        {
            text.alignment = TextAlignmentOptions.TopRight;
            lineUp = 2;
        }
        if (int.Parse(valueInt) == 2 && (text.alignment == TextAlignmentOptions.Center || text.alignment == TextAlignmentOptions.Left || text.alignment == TextAlignmentOptions.Right))
        {
            text.alignment = TextAlignmentOptions.Right;
            lineUp = 2;
        }
        if (int.Parse(valueInt) == 2 && (text.alignment == TextAlignmentOptions.Bottom || text.alignment == TextAlignmentOptions.BottomLeft || text.alignment == TextAlignmentOptions.BottomRight))
        {
            text.alignment = TextAlignmentOptions.BottomRight;
            lineUp = 2;
        }


        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.Top))
        {
            text.alignment = TextAlignmentOptions.Top;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.TopLeft))
        {
            text.alignment = TextAlignmentOptions.TopLeft;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.TopRight))
        {
            text.alignment = TextAlignmentOptions.TopRight;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.Center))
        {
            text.alignment = TextAlignmentOptions.Top;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.Left))
        {
            text.alignment = TextAlignmentOptions.TopLeft;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.Right))
        {
            text.alignment = TextAlignmentOptions.TopRight;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.Bottom))
        {
            text.alignment = TextAlignmentOptions.Top;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.BottomLeft))
        {
            text.alignment = TextAlignmentOptions.TopLeft;
            lineBottom = 0;
        }
        if (int.Parse(valueInt) == 3 && (text.alignment == TextAlignmentOptions.BottomRight))
        {
            text.alignment = TextAlignmentOptions.TopRight;
            lineBottom = 0;
        }

        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.Top))
        {
            text.alignment = TextAlignmentOptions.Center;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.TopLeft))
        {
            text.alignment = TextAlignmentOptions.Left;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.TopRight))
        {
            text.alignment = TextAlignmentOptions.Right;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.Center))
        {
            text.alignment = TextAlignmentOptions.Center;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.Left))
        {
            text.alignment = TextAlignmentOptions.Left;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.Right))
        {
            text.alignment = TextAlignmentOptions.Right;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.Bottom))
        {
            text.alignment = TextAlignmentOptions.Center;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.BottomLeft))
        {
            text.alignment = TextAlignmentOptions.Left;
            lineBottom = 1;
        }
        if (int.Parse(valueInt) == 4 && (text.alignment == TextAlignmentOptions.BottomRight))
        {
            text.alignment = TextAlignmentOptions.Right;
            lineBottom = 1;
        }

        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.Top))
        {
            text.alignment = TextAlignmentOptions.Bottom;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.TopLeft))
        {
            text.alignment = TextAlignmentOptions.BottomLeft;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.TopRight))
        {
            text.alignment = TextAlignmentOptions.BottomRight;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.Center))
        {
            text.alignment = TextAlignmentOptions.Bottom;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.Left))
        {
            text.alignment = TextAlignmentOptions.BottomLeft;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.Right))
        {
            text.alignment = TextAlignmentOptions.BottomRight;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.Bottom))
        {
            text.alignment = TextAlignmentOptions.Bottom;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.BottomLeft))
        {
            text.alignment = TextAlignmentOptions.BottomLeft;
            lineBottom = 2;
        }
        if (int.Parse(valueInt) == 5 && (text.alignment == TextAlignmentOptions.BottomRight))
        {
            text.alignment = TextAlignmentOptions.BottomRight;
            lineBottom = 2;
        }
        Debug.Log(text.alignment);
}
}
