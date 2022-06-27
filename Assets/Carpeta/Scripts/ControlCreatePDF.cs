using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
//using Sfs2X.Entities.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using UnityEngine.UI;

public class ControlCreatePDF : MonoBehaviour
{
    [SerializeField] private SaverPreparet saveScript;

    [SerializeField] private GameObject[] activate;

    [SerializeField] private GameObject prefabLeaf;
    [SerializeField] private List<GameObject> prefabLeafs = new List<GameObject>();

    [SerializeField] private List<GameObject> prefabsP = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabsAct = new List<GameObject>();

    [SerializeField] private Transform conteiner;
    [SerializeField] private Transform conteinerLeaf;

    [SerializeField] private int numContainer;

    [SerializeField] private TextMeshProUGUI TextPag;
    [SerializeField] private int numPag = 1;
    [SerializeField] private int pagAct = 1;


    [SerializeField] private List<string> paths = new List<string>();
    [SerializeField] private List<GameObject> imgs = new List<GameObject>();

    public TMP_FontAsset[] fontTexto;

    GameObject[] jajdsaa;
    /// <summary>
    /// //
    /// </summary>

    string pathExtra;

    // Screnn
    // 4k = 3840 x 2160   1080p = 1920 x 1080
    public int captureWidth = 2480;
    public int captureHeight = 3508;

    public int numImagen;

    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    [SerializeField] private int counter = 0; // image #

    public Camera carmaes;

    public void AddPrefab()
    {
        activate[0].SetActive(false);
        activate[1].SetActive(true);

        Debug.Log("1");
        string path;
        path = EditorUtility.OpenFolderPanel("Select Directory", PlayerPrefs.GetString("pathSave"), "");
        if (!Directory.Exists(path))
        {
            activate[0].SetActive(true);
            activate[1].SetActive(false);
            return;
        }
        pathExtra = path;
        PlayerPrefs.SetString("pathSave", pathExtra);

        GameObject instanLeaf01 = Instantiate(prefabLeaf, conteinerLeaf);
        prefabLeafs.Add(instanLeaf01);

        Color colorAyudante01;
        ColorUtility.TryParseHtmlString(("#" + saveScript.colorLeft[0]), out colorAyudante01);
        instanLeaf01.GetComponent<UnityEngine.UI.Image>().color = colorAyudante01;

        for (int i = 0; i < saveScript.sizeLeft.Count; i++)
        {
            numContainer += saveScript.sizeLeft[i];

            if (numContainer >= 3330)
            {
                numContainer = 0;

                GameObject instanLeaf = Instantiate(prefabLeaf, conteinerLeaf);
                prefabLeafs.Add(instanLeaf);

                Color colorAyudante;
                ColorUtility.TryParseHtmlString(("#" + saveScript.colorLeft[i]), out colorAyudante);
                instanLeaf.GetComponent<UnityEngine.UI.Image>().color = colorAyudante;

                numPag++;
                pagAct++;
            }

            GameObject instan = Instantiate(prefabsP[saveScript.prefabTipe[i]], prefabLeafs[prefabLeafs.Count - 1].transform.GetChild(0), prefabLeafs[prefabLeafs.Count - 1].transform.parent.GetChild(0));
            prefabsAct.Add(instan);

            // prefabTipe = 0 desc title || prefabTipe = 1 desc || prefabTipe = 2 desc title imge || prefabTipe = 0 desc imge 
            if (saveScript.prefabTipe[i] == 0 || saveScript.prefabTipe[i] == 4 || saveScript.prefabTipe[i] == 8)
            {
                // Title
                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = saveScript.textTitles[i];
                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textTitlesSize[i];

                Color colorAyudanteTextTitle;
                ColorUtility.TryParseHtmlString(("#" + saveScript.textTitlesColorHx[i]), out colorAyudanteTextTitle);
                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextTitle;

                // Align Text
                { 
                if (saveScript.textTitleslineUp[i] == 0)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                }
                if (saveScript.textTitleslineUp[i] == 1)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                }
                if (saveScript.textTitleslineUp[i] == 2)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                }

                if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                }
                if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                }
                if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                }

                if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                }
                if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                }
                if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                }

                if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                }
                if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                }
                if (saveScript.textTitleslineBottom[i] == 3 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                }
            }

                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textTitlesNumFont[i]];

                if (saveScript.textTitlesStyleB[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                }
                if (saveScript.textTitlesStyleI[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                }
                if (saveScript.textTitlesStyleU[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                }
                if (saveScript.textTitlesStyleS[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                }

                // Descripcion
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = saveScript.textDescrips[i];
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                Color colorAyudanteTextDescrip;
                ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                // Align Text
                {
                    if (saveScript.textDescripslineUp[i] == 0)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineUp[i] == 1)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineUp[i] == 2)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                    }

                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                    }
                    if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                    }
                }

                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                if (saveScript.textDescripsStyleB[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                }
                if (saveScript.textDescripsStyleI[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                }
                if (saveScript.textDescripsStyleU[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                }
                if (saveScript.textDescripsStyleS[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                }
            }
            
            if (saveScript.prefabTipe[i] == 1 || saveScript.prefabTipe[i] == 5 || saveScript.prefabTipe[i] == 9)
            {
                // Descripcion
                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = saveScript.textDescrips[i];
                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                Color colorAyudanteTextDescrip;
                ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                // Align Text
                {
                    if (saveScript.textDescripslineUp[i] == 0)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineUp[i] == 1)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineUp[i] == 2)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                    }

                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                    }
                    if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                    }
                }

                instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                if (saveScript.textDescripsStyleB[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                }
                if (saveScript.textDescripsStyleI[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                }
                if (saveScript.textDescripsStyleU[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                }
                if (saveScript.textDescripsStyleS[i] == true)
                {
                    instan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                }
            }
            
            if (saveScript.prefabTipe[i] == 2 || saveScript.prefabTipe[i] == 6 || saveScript.prefabTipe[i] == 10)
            {
                // Img
                paths.Add(saveScript.pathImgs[i]);
                imgs.Add(instan);
                //StartCoroutine(LoadImage(saveScript.pathImgs[i], instan));

                /* WWW www = new WWW(saveScript.pathImgs[i]);
                 while (!www.isDone)
                 instan.transform.GetChild(1).GetComponent<RawImage>().texture = www.texture;*/

                /*
                using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(saveScript.pathImgs[i]))
                {
                        var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                        instan.transform.GetChild(1).GetComponent<RawImage>().texture = uwrTexture;
                }*/

                // Title
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = saveScript.textTitles[i];
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textTitlesSize[i];

                Color colorAyudanteTextTitle;
                ColorUtility.TryParseHtmlString(("#" + saveScript.textTitlesColorHx[i]), out colorAyudanteTextTitle);
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextTitle;

                // Align Text
                {
                    if (saveScript.textTitleslineUp[i] == 0)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textTitleslineUp[i] == 1)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textTitleslineUp[i] == 2)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                    }
                    if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                    }
                    if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                    }

                    if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                    }
                    if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                    }
                    if (saveScript.textTitleslineBottom[i] == 3 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                    }
                }

                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textTitlesNumFont[i]];

                if (saveScript.textTitlesStyleB[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                }
                if (saveScript.textTitlesStyleI[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                }
                if (saveScript.textTitlesStyleU[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                }
                if (saveScript.textTitlesStyleS[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                }

                // Descripcion
                instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = saveScript.textDescrips[i];
                instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                Color colorAyudanteTextDescrip;
                ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                // Align Text
                {
                    if (saveScript.textDescripslineUp[i] == 0)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineUp[i] == 1)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineUp[i] == 2)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                    }

                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                    }
                    if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                    }
                }

                instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                if (saveScript.textDescripsStyleB[i] == true)
                {
                    instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                }
                if (saveScript.textDescripsStyleI[i] == true)
                {
                    instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                }
                if (saveScript.textDescripsStyleU[i] == true)
                {
                    instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                }
                if (saveScript.textDescripsStyleS[i] == true)
                {
                    instan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                }
            }
            
            if (saveScript.prefabTipe[i] == 3 || saveScript.prefabTipe[i] == 7 || saveScript.prefabTipe[i] == 11)
            {

                // Img
                //StartCoroutine(LoadImage(saveScript.pathImgs[i], instan));
                paths.Add(saveScript.pathImgs[i]);
                imgs.Add(instan);

                /* using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(saveScript.pathImgs[i]))
                 {
                         var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                         instan.transform.GetChild(1).GetComponent<RawImage>().texture = uwrTexture;

                 }*/


                // Descripcion
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = saveScript.textDescrips[i];
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                Color colorAyudanteTextDescrip;
                ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                // Align Text
                {
                    if (saveScript.textDescripslineUp[i] == 0)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineUp[i] == 1)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineUp[i] == 2)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                    }
                    if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                    }

                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                    }
                    if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                    }

                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                    }
                    if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                    }
                    if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                    {
                        instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                    }
                }

                instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                if (saveScript.textDescripsStyleB[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                }
                if (saveScript.textDescripsStyleI[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                }
                if (saveScript.textDescripsStyleU[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                }
                if (saveScript.textDescripsStyleS[i] == true)
                {
                    instan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                }
            }

        }

        //Invoke("createPDFsPng", 2f);

        if (File.Exists(pathExtra + "/File.pdf"))
            File.Delete(pathExtra + "/File.pdf");
        StartCoroutine(LoadImage("2"));
    }

    IEnumerator LoadImage(string pathpp)
    {
        for (int i = 0; i < paths.Count; i++)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(paths[i]))
            {
                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                    imgs[i].transform.GetChild(1).GetComponent<RawImage>().texture = uwrTexture;
                }
            }
        }

        StartCoroutine(GenerateFile(activate));
    }


    IEnumerator GenerateFile(GameObject[] acti)
    {
        Debug.Log("2");
        activate[0].SetActive(false);
        activate[1].SetActive(true);

        using (var fileStream = new FileStream(pathExtra + "/File.pdf", FileMode.OpenOrCreate, FileAccess.Write))
        {
            var document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            var writer = PdfWriter.GetInstance(document, fileStream);
            document.Open();

            for (int i = 0; i < prefabLeafs.Count; i++)
            {
                Debug.Log("3");
                for (int k = 0; k < prefabLeafs.Count; k++)
                {
                    prefabLeafs[k].SetActive(false);
                }
                prefabLeafs[i].SetActive(true);

                yield return new WaitForSeconds(1f);

                Debug.Log("4");

                byte[] pngArray;

                // create screenshot objects if needed
                {
                    if (renderTexture == null)
                    {
                        // creates off-screen render texture that can rendered into
                        rect = new Rect(0, 0, captureWidth, captureHeight);
                        renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
                        screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
                    }

                    // get main camera and manually render scene into rt
                    Camera camera = carmaes; // NOTE: added because there was no reference to camera in original script; must add this script to Camera
                    camera.targetTexture = renderTexture;
                    camera.Render();

                    // read pixels will read from the currently active render texture so make our offscreen 
                    // render texture active and then read the pixels
                    RenderTexture.active = renderTexture;
                    screenShot.ReadPixels(rect, 0, 0);

                    // reset active camera texture and render texture
                    camera.targetTexture = null;
                    RenderTexture.active = null;

                    // pull in our file header/data bytes for the specified image format (has to be done from main thread)
                    byte[] fileData = null;

                    // create a file header for ppm formatted file
                    fileData = screenShot.EncodeToJPG();

                    pngArray = fileData;
                }

                /// Optional
                // create new thread to save the image to file (only operation that can be done in background)
                if (!Directory.Exists(pathExtra + "/" + "Jpg_images" + "/" + numImagen + ".JPG"))
                {
                    Directory.CreateDirectory(pathExtra + "/" + "Jpg_images" + "/");
                }

                numImagen = 0;
                for (int p = 0; p < 10000; p++)
                {
                    while (File.Exists(pathExtra + "/" + "Jpg_images" + "/" + numImagen + ".JPG"))
                    {
                        numImagen += 1;
                    }
                }

                // create file and write optional header with image bytes
                byte[] fileData02 = screenShot.EncodeToJPG();
                var f = File.Create(pathExtra + "/" + "Jpg_images" + "/" + numImagen + ".JPG");
                f.Write(fileData02, 0, fileData02.Length);
                f.Close();
                //Debug.Log(string.Format(filename, fileData.Length));
                ///

                document.NewPage();

                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                MemoryStream ms = new MemoryStream(pngArray);

                Document doc = new Document(PageSize.A4); // Crear un documento A4

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms); // Crear una instancia de imagen

                //Limita la imagen para que no exceda el rango A4
                if ((image.Height > PageSize.A4.Height) || (image.Width > PageSize.A4.Width))
                {
                    image.ScaleToFit((PageSize.A4.Width), (PageSize.A4.Height));
                }

                // Alineación central
                image.Alignment = Element.ALIGN_MIDDLE;

                document.Add(image);
            }

            document.Close();
            writer.Close();
        }
        Debug.Log("3");
        PrintFiles();
     }

    void PrintFiles()
    {
        Debug.Log(pathExtra + "/File.pdf");
        if ((pathExtra + "/File.pdf") == null)
            return;

        if (File.Exists(pathExtra + "/File.pdf"))
        {
            Debug.Log("file found");
        }
        else
        {
            Debug.Log("file not found");
            return;
        }
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
        process.StartInfo.UseShellExecute = true;
        process.StartInfo.FileName = pathExtra + "/File.pdf";
        //process.StartInfo.Verb = "print";

        process.Start();
        //process.WaitForExit();


        activate[0].SetActive(true);
        activate[1].SetActive(false);
    }


    public void NextPaga()
    {
        pagAct++;

        if (pagAct > numPag)
        {
            pagAct = 1;
        }

        for (int i = 0; i < numPag; i++)
        {
            prefabLeafs[i].SetActive(false);
        }
        prefabLeafs[pagAct - 1].SetActive(true);
        
        if (pagAct > numPag)
        {
            pagAct = 1;

            for (int i = 0; i < numPag; i++)
            {
                prefabLeafs[i].SetActive(false);
            }
            prefabLeafs[0].SetActive(true);
        }

        TextPag.text = "" + pagAct;
    }
    public void PreviousPage()
    {
        pagAct--;

        if (pagAct < 1)
        {
            pagAct = numPag;
        }

        for (int i = 0; i < numPag; i++)
        {
            prefabLeafs[i].SetActive(false);
        }
        prefabLeafs[pagAct - 1].SetActive(true);

        if (pagAct < 1)
        {
            pagAct = numPag;

            for (int i = 0; i < numPag; i++)
            {
                prefabLeafs[i].SetActive(false);
            }
            prefabLeafs[numPag - 1].SetActive(true);
        }

        TextPag.text = "" + pagAct;
    }

}
