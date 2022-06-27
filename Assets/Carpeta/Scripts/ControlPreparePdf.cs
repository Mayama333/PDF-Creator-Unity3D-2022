using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HSVPicker;

using UnityEngine.Networking;
public class ControlPreparePdf : MonoBehaviour
{
    
    [SerializeField] private SaverPreparet saveScript;

    [SerializeField] private GameObject prefabLeaf;
    [SerializeField] private List<GameObject> prefabLeafs = new List<GameObject>();
    [SerializeField] private List<GameObject> pickColorLeafs = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabsP = new List<GameObject>();
    [SerializeField] private List<GameObject> prefabsAct = new List<GameObject>();
    [SerializeField] private Transform conteiner;
    [SerializeField] private Transform conteinerLeaf;

    [SerializeField] private GameObject prefapickColor;
    [SerializeField] private Transform ConteinerpickColor;

    [SerializeField] private int numContainer;

    [SerializeField] private TextMeshProUGUI TextPag;
    [SerializeField] private int numPag = 1;
    [SerializeField] private int pagAct = 1;

    // Variables
    public TMP_FontAsset[] fontTexto;

    //  public List<ItemImage> ItemImagess = new List<ItemImage>();

    public void backTo()
    {
        for (int i = ConteinerpickColor.childCount - 1; i >= 1; i--)
        {
            Destroy(ConteinerpickColor.GetChild(i).gameObject);
        }
    }

    public void pickedCreate(int num)
    {
        Debug.Log("pickecCreate");

        for (int i = ConteinerpickColor.childCount - 1; i >= 1; i--)
        {
            Destroy(ConteinerpickColor.GetChild(i).gameObject);
        }
        GameObject instanPick = Instantiate(prefapickColor, ConteinerpickColor);
        //instanPick.transform.position = new Vector3(0, 0, 0);
        //TextoTitulo
        for (int i = 0; i < prefabsAct.Count; i++)
        {
            for (int k = 0; k < prefabsAct[i].transform.childCount; k++)
            {
                if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null)
                {
                    prefabsAct[i].transform.GetChild(k).GetChild(3).gameObject.SetActive(false);
                }

                if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Text Title" && prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().NumPick == num)
                {
                    instanPick.GetComponent<ColorPicker>()._color = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().Color;
                    prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().picker = instanPick.GetComponent<ColorPicker>();
                }

                if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Text Description" && prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().NumPick == num)
                {
                    instanPick.GetComponent<ColorPicker>()._color = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().Color;
                    prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().picker = instanPick.GetComponent<ColorPicker>();

                }
            }
        }
    }


        public void colorpickLeaf()
        {
            for (int i = 0; i < pickColorLeafs.Count; i++)
            {
                pickColorLeafs[i].SetActive(false);
            }

            pickColorLeafs[pagAct - 1].SetActive(true);
        }
        public void colorpickLeafDesactive()
        {
            for (int i = 0; i < pickColorLeafs.Count; i++)
            {
                pickColorLeafs[i].SetActive(false);
            }
        }

        public void Update()
        {
            StartCoroutine("SyncFata");
        }

        IEnumerator SyncFata()
        {
            //Hojac
            for (int i = 0; i < prefabLeafs.Count; i++)
            {
                saveScript.colorLeft[i] = ColorUtility.ToHtmlStringRGB(prefabLeafs[i].GetComponent<Image>().color);
            }

            //Imagenes
            for (int i = 0; i < prefabsAct.Count; i++)
            {
                for (int k = 0; k < prefabsAct[i].transform.childCount; k++)
                {
                    if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemImage>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Image")
                    {
                        saveScript.pathImgs[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemImage>().pathImg;
                    }
                }
            }

            //TextoTitulo
                for (int i = 0; i < prefabsAct.Count; i++)
                {
                    for (int k = 0; k < prefabsAct[i].transform.childCount; k++)
                    {
                        if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Text Title")
                        {
                            saveScript.textTitles[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().text.text;
                            saveScript.textTitlesColorHx[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().colorHx;
                            saveScript.textTitlesSize[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().size;
                            saveScript.textTitleslineUp[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().lineUp;
                            saveScript.textTitleslineBottom[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().lineBottom;
                            saveScript.textTitlesNumFont[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().numFont;
                            saveScript.textTitlesStyleB[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleB;
                            saveScript.textTitlesStyleI[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleI;
                            saveScript.textTitlesStyleU[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleU;
                            saveScript.textTitlesStyleS[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleS;
                        }
                    }
                }

            //TextoDescrip

                for (int i = 0; i < prefabsAct.Count; i++)
                {
                    for (int k = 0; k < prefabsAct[i].transform.childCount; k++)
                    {
                        if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Text Description")
                        {
                            saveScript.textDescrips[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().text.text;
                            saveScript.textDescripsColorHx[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().colorHx;
                            saveScript.textDescripsSize[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().size;
                            saveScript.textDescripslineUp[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().lineUp;
                            saveScript.textDescripslineBottom[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().lineBottom;
                            saveScript.textDescripsNumFont[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().numFont;
                            saveScript.textDescripsStyleB[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleB;
                            saveScript.textDescripsStyleI[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleI;
                            saveScript.textDescripsStyleU[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleU;
                            saveScript.textDescripsStyleS[i] = prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().styleS;
                        }
                    }
                }

            yield return new WaitForSeconds(0.5f);
        }
    

    public void AddPrefab(int addNum)
    {

        saveScript.prefabTipe.Add(addNum);

        if (addNum == 0 || addNum == 1 || addNum == 2 || addNum == 3)
        {
            saveScript.sizeLeft.Add(370);
            numContainer += 370;
        }
        if (addNum == 4 || addNum == 5 || addNum == 6 || addNum == 7)
        {
            saveScript.sizeLeft.Add(690);
            numContainer += 690;
        }
        if (addNum == 8 || addNum == 9 || addNum == 10 || addNum == 11)
        {
            saveScript.sizeLeft.Add(1150);
            numContainer += 1150;
        }

        if (numContainer >= 3330)
        {
            numContainer = 0;

            GameObject instanLeaf = Instantiate(prefabLeaf, conteinerLeaf);
            prefabLeafs.Add(instanLeaf);
            pickColorLeafs.Add(instanLeaf.transform.GetChild(0).gameObject);
            numPag++;
            pagAct++;
            TextPag.text = "" + pagAct + "/" + numPag;
        }

        GameObject instan = Instantiate(prefabsP[addNum], prefabLeafs[prefabLeafs.Count - 1].transform.GetChild(1), prefabLeafs[prefabLeafs.Count - 1].transform.parent.GetChild(1));
        prefabsAct.Add(instan);
        int pickColorNum = 0;

            //TextoTitulo
            for (int i = 0; i < prefabsAct.Count; i++)
            {
                for (int k = 0; k < prefabsAct[i].transform.childCount; k++)
                {
                    if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Text Title")
                    {
                        prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().NumPick = pickColorNum;
                        prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().controlScript = GetComponent<ControlPreparePdf>();

                        pickColorNum++;
                    }

                    if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null && prefabsAct[i].transform.GetChild(k).name == "Panel Text Description")
                    {
                        prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().NumPick = pickColorNum;
                        prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>().controlScript = GetComponent<ControlPreparePdf>();
                        pickColorNum++;
                    }
                }
            }  


        //Hoja
        saveScript.colorLeft.Add(null);
        //Imagenes
        saveScript.pathImgs.Add(null);
        //TextoTitulo
        saveScript.textTitles.Add(null);
        saveScript.textTitlesColorHx.Add("323232");
        saveScript.textTitlesSize.Add(80);
        saveScript.textTitleslineUp.Add(0);
        saveScript.textTitleslineBottom.Add(0);
        saveScript.textTitlesNumFont.Add(0);
        saveScript.textTitlesStyleB.Add(false);
        saveScript.textTitlesStyleI.Add(false);
        saveScript.textTitlesStyleU.Add(false);
        saveScript.textTitlesStyleS.Add(false);

        //TextoDescrip
        saveScript.textDescrips.Add(null);
        saveScript.textDescripsColorHx.Add("323232");
        saveScript.textDescripsSize.Add(80);
        saveScript.textDescripslineUp.Add(0);
        saveScript.textDescripslineBottom.Add(0);
        saveScript.textDescripsNumFont.Add(0);
        saveScript.textDescripsStyleB.Add(false);
        saveScript.textDescripsStyleI.Add(false);
        saveScript.textDescripsStyleU.Add(false);
        saveScript.textDescripsStyleS.Add(false);


        for (int i = 0; i < numPag; i++)
        {
            prefabLeafs[i].SetActive(false);
        }
        prefabLeafs[pagAct - 1].SetActive(true);

        TextPag.text = "" + pagAct + "/" + numPag;
    }


    public void LoadSave()
    {
        saveScript.Cargar();

       // Invoke("LoadFullSave", 0.3f);
    }

    public void LoadFullSave()
    {
        for (int i = prefabLeafs.Count - 1; i >= 1; i--)
        {
            Destroy(prefabLeafs[i].gameObject);
            prefabLeafs.RemoveAt(i);
        }

        for (int i = prefabsAct.Count - 1; i >= 0; i--)
        {
            Destroy(prefabsAct[i].gameObject);
        }

        prefabsAct.Clear();

        numContainer = 0;
        numPag = 1;
        pagAct = 1;
        TextPag.text = "1/1";

        Color colorAyudante01;
        ColorUtility.TryParseHtmlString(("#" + saveScript.colorLeft[0]), out colorAyudante01);
        prefabLeafs[0].GetComponent<UnityEngine.UI.Image>().color = colorAyudante01;

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

                //Hoja
                //saveScript.colorLeft.Add(null);

                numPag++;
                pagAct++;
                TextPag.text = "" + pagAct + "/" + numPag;
            }

            if (i < saveScript.prefabTipe.Count)
            {
                GameObject instan = Instantiate(prefabsP[saveScript.prefabTipe[i]], prefabLeafs[prefabLeafs.Count - 1].transform.GetChild(1), prefabLeafs[prefabLeafs.Count - 1].transform.parent.GetChild(1));
                prefabsAct.Add(instan);

                Debug.Log("hice una instancia"); // Confirmacion

                // TT
                {
                    
                    // prefabTipe = 0 desc title || prefabTipe = 1 desc || prefabTipe = 2 desc title imge || prefabTipe = 0 desc imge 
                    if (saveScript.prefabTipe[i] == 0 || saveScript.prefabTipe[i] == 4 || saveScript.prefabTipe[i] == 8)
                    {
                        // Title
                        instan.transform.GetChild(1).GetComponent<ItemText>().text.text = saveScript.textTitles[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().colorHx = saveScript.textTitlesColorHx[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().size = saveScript.textTitlesSize[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().lineUp = saveScript.textTitleslineUp[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().lineBottom = saveScript.textTitleslineBottom[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().numFont = saveScript.textTitlesNumFont[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleB = saveScript.textTitlesStyleB[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleI = saveScript.textTitlesStyleI[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleU = saveScript.textTitlesStyleU[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleS = saveScript.textTitlesStyleS[i];

                        instan.transform.GetChild(1).GetChild(1).GetComponent<TMP_InputField>().text = saveScript.textTitles[i];
                        instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textTitlesSize[i];

                        Color colorAyudanteTextTitle;
                        ColorUtility.TryParseHtmlString(("#" + saveScript.textTitlesColorHx[i]), out colorAyudanteTextTitle);
                        instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextTitle;

                        // Align Text
                        {
                            if (saveScript.textTitleslineUp[i] == 0)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textTitleslineUp[i] == 1)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textTitleslineUp[i] == 2)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                            }
                            if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                            }
                            if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                            }

                            if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                            }
                            if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                            }
                            if (saveScript.textTitleslineBottom[i] == 3 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                            }
                        }

                        instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textTitlesNumFont[i]];

                        if (saveScript.textTitlesStyleB[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                        }
                        if (saveScript.textTitlesStyleI[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                        }
                        if (saveScript.textTitlesStyleU[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                        }
                        if (saveScript.textTitlesStyleS[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        }

                        // Descripcion
                        instan.transform.GetChild(2).GetComponent<ItemText>().text.text = saveScript.textDescrips[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().colorHx = saveScript.textDescripsColorHx[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().size = saveScript.textDescripsSize[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().lineUp = saveScript.textDescripslineUp[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().lineBottom = saveScript.textDescripslineBottom[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().numFont = saveScript.textDescripsNumFont[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleB = saveScript.textDescripsStyleB[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleI = saveScript.textDescripsStyleI[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleU = saveScript.textDescripsStyleU[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleS = saveScript.textDescripsStyleS[i];

                        instan.transform.GetChild(2).GetChild(1).GetComponent<TMP_InputField>().text = saveScript.textDescrips[i];

                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                        Color colorAyudanteTextDescrip;
                        ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                        // Align Text
                        {
                            if (saveScript.textDescripslineUp[i] == 0)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineUp[i] == 1)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineUp[i] == 2)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                            }

                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                            }
                            if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                            }
                        }

                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                        if (saveScript.textDescripsStyleB[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                        }
                        if (saveScript.textDescripsStyleI[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                        }
                        if (saveScript.textDescripsStyleU[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                        }
                        if (saveScript.textDescripsStyleS[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        }
                    }

                    if (saveScript.prefabTipe[i] == 1 || saveScript.prefabTipe[i] == 5 || saveScript.prefabTipe[i] == 9)
                    {
                        // Descripcion
                        instan.transform.GetChild(1).GetComponent<ItemText>().text.text = saveScript.textDescrips[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().colorHx = saveScript.textDescripsColorHx[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().size = saveScript.textDescripsSize[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().lineUp = saveScript.textDescripslineUp[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().lineBottom = saveScript.textDescripslineBottom[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().numFont = saveScript.textDescripsNumFont[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleB = saveScript.textDescripsStyleB[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleI = saveScript.textDescripsStyleI[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleU = saveScript.textDescripsStyleU[i];
                        instan.transform.GetChild(1).GetComponent<ItemText>().styleS = saveScript.textDescripsStyleS[i];

                        instan.transform.GetChild(1).GetChild(1).GetComponent<TMP_InputField>().text = saveScript.textDescrips[i];

                        instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                        Color colorAyudanteTextDescrip;
                        ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                        instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                        // Align Text
                        {
                            if (saveScript.textDescripslineUp[i] == 0)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineUp[i] == 1)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineUp[i] == 2)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                            }

                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                            }
                            if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                            }
                        }

                        instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                        if (saveScript.textDescripsStyleB[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                        }
                        if (saveScript.textDescripsStyleI[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                        }
                        if (saveScript.textDescripsStyleU[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                        }
                        if (saveScript.textDescripsStyleS[i] == true)
                        {
                            instan.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        }
                    }

                    if (saveScript.prefabTipe[i] == 2 || saveScript.prefabTipe[i] == 6 || saveScript.prefabTipe[i] == 10)
                    {
                        // Img
                        prefabsAct[i].transform.GetChild(1).GetComponent<ItemImage>().pathImg = saveScript.pathImgs[i];
                        StartCoroutine(LoadImage(saveScript.pathImgs[i], instan.transform.GetChild(1).GetChild(0).GetChild(0).gameObject));

                        // Title
                        instan.transform.GetChild(2).GetComponent<ItemText>().text.text = saveScript.textTitles[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().colorHx = saveScript.textTitlesColorHx[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().size = saveScript.textTitlesSize[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().lineUp = saveScript.textTitleslineUp[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().lineBottom = saveScript.textTitleslineBottom[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().numFont = saveScript.textTitlesNumFont[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleB = saveScript.textTitlesStyleB[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleI = saveScript.textTitlesStyleI[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleU = saveScript.textTitlesStyleU[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleS = saveScript.textTitlesStyleS[i];

                        instan.transform.GetChild(2).GetChild(1).GetComponent<TMP_InputField>().text = saveScript.textTitles[i];
                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textTitlesSize[i];

                        Color colorAyudanteTextTitle;
                        ColorUtility.TryParseHtmlString(("#" + saveScript.textTitlesColorHx[i]), out colorAyudanteTextTitle);
                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextTitle;

                        // Align Text
                        {
                            if (saveScript.textTitleslineUp[i] == 0)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textTitleslineUp[i] == 1)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textTitleslineUp[i] == 2)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                            }
                            if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                            }
                            if (saveScript.textTitleslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                            }

                            if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textTitleslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                            }
                            if (saveScript.textTitleslineBottom[i] == 2 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                            }
                            if (saveScript.textTitleslineBottom[i] == 3 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                            }
                        }

                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textTitlesNumFont[i]];

                        if (saveScript.textTitlesStyleB[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                        }
                        if (saveScript.textTitlesStyleI[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                        }
                        if (saveScript.textTitlesStyleU[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                        }
                        if (saveScript.textTitlesStyleS[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        }

                        // Descripcion
                        instan.transform.GetChild(3).GetComponent<ItemText>().text.text = saveScript.textDescrips[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().colorHx = saveScript.textDescripsColorHx[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().size = saveScript.textDescripsSize[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().lineUp = saveScript.textDescripslineUp[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().lineBottom = saveScript.textDescripslineBottom[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().numFont = saveScript.textDescripsNumFont[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().styleB = saveScript.textDescripsStyleB[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().styleI = saveScript.textDescripsStyleI[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().styleU = saveScript.textDescripsStyleU[i];
                        instan.transform.GetChild(3).GetComponent<ItemText>().styleS = saveScript.textDescripsStyleS[i];

                        instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = saveScript.textDescrips[i];
                        instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                        Color colorAyudanteTextDescrip;
                        ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                        instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                        // Align Text
                        {
                            if (saveScript.textDescripslineUp[i] == 0)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineUp[i] == 1)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineUp[i] == 2)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                            }

                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                            }
                            if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                            }
                        }

                        instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                        if (saveScript.textDescripsStyleB[i] == true)
                        {
                            instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                        }
                        if (saveScript.textDescripsStyleI[i] == true)
                        {
                            instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                        }
                        if (saveScript.textDescripsStyleU[i] == true)
                        {
                            instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                        }
                        if (saveScript.textDescripsStyleS[i] == true)
                        {
                            instan.transform.GetChild(3).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        }
                    }

                    if (saveScript.prefabTipe[i] == 3 || saveScript.prefabTipe[i] == 7 || saveScript.prefabTipe[i] == 11)
                    {

                        // Img
                        prefabsAct[i].transform.GetChild(1).GetComponent<ItemImage>().pathImg = saveScript.pathImgs[i];
                        StartCoroutine(LoadImage(saveScript.pathImgs[i], instan.transform.GetChild(1).GetChild(0).GetChild(0).gameObject));

                        // Descripcion
                        instan.transform.GetChild(2).GetComponent<ItemText>().text.text = saveScript.textDescrips[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().colorHx = saveScript.textDescripsColorHx[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().size = saveScript.textDescripsSize[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().lineUp = saveScript.textDescripslineUp[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().lineBottom = saveScript.textDescripslineBottom[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().numFont = saveScript.textDescripsNumFont[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleB = saveScript.textDescripsStyleB[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleI = saveScript.textDescripsStyleI[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleU = saveScript.textDescripsStyleU[i];
                        instan.transform.GetChild(2).GetComponent<ItemText>().styleS = saveScript.textDescripsStyleS[i];


                        instan.transform.GetChild(2).GetChild(1).GetComponent<TMP_InputField>().text = saveScript.textDescrips[i];
                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = saveScript.textDescripsSize[i];

                        Color colorAyudanteTextDescrip;
                        ColorUtility.TryParseHtmlString(("#" + saveScript.textDescripsColorHx[i]), out colorAyudanteTextDescrip);
                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().color = colorAyudanteTextDescrip;

                        // Align Text
                        {
                            if (saveScript.textDescripslineUp[i] == 0)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineUp[i] == 1)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineUp[i] == 2)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Top;
                            }
                            if (saveScript.textDescripslineBottom[i] == 0 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.TopRight;
                            }

                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Left;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
                            }
                            if (saveScript.textDescripslineBottom[i] == 1 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                            }

                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Left)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomLeft;
                            }
                            if (saveScript.textDescripslineBottom[i] == 2 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Center)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Bottom;
                            }
                            if (saveScript.textDescripslineBottom[i] == 3 && instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment == TextAlignmentOptions.Right)
                            {
                                instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.BottomRight;
                            }
                        }

                        instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().font = fontTexto[saveScript.textDescripsNumFont[i]];

                        if (saveScript.textDescripsStyleB[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Bold;
                        }
                        if (saveScript.textDescripsStyleI[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Italic;
                        }
                        if (saveScript.textDescripsStyleU[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle ^= FontStyles.Underline;
                        }
                        if (saveScript.textDescripsStyleS[i] == true)
                        {
                            instan.transform.GetChild(2).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Strikethrough;
                        }
                    }
                }


              
            }

            int pickColorNum = 0;

            //TextoTitulo
            for (int  j = 0; j < prefabsAct.Count; j++)
            {
                for (int l = 0; l < prefabsAct[j].transform.childCount; l++)
                {
                    if (prefabsAct[j].transform.GetChild(l).GetComponent<ItemText>() != null && prefabsAct[j].transform.GetChild(l).name == "Panel Text Title")
                    {
                        prefabsAct[j].transform.GetChild(l).GetComponent<ItemText>().NumPick = pickColorNum;
                        prefabsAct[j].transform.GetChild(l).GetComponent<ItemText>().controlScript = GetComponent<ControlPreparePdf>();

                        pickColorNum++;
                    }

                    if (prefabsAct[j].transform.GetChild(l).GetComponent<ItemText>() != null && prefabsAct[j].transform.GetChild(l).name == "Panel Text Description")
                    {
                        prefabsAct[j].transform.GetChild(l).GetComponent<ItemText>().NumPick = pickColorNum;
                        prefabsAct[j].transform.GetChild(l).GetComponent<ItemText>().controlScript = GetComponent<ControlPreparePdf>();
                        pickColorNum++;
                    }
                }
            }

        }

        for (int i = ConteinerpickColor.childCount - 1; i >= 1; i--)
        {
            Destroy(ConteinerpickColor.GetChild(i).gameObject);
        }
        //TextoTitulo
        for (int i = 0; i < prefabsAct.Count; i++)
        {
            for (int k = 0; k < prefabsAct[i].transform.childCount; k++)
            {
                if (prefabsAct[i].transform.GetChild(k).GetComponent<ItemText>() != null)
                {
                    prefabsAct[i].transform.GetChild(k).GetChild(3).gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator LoadImage(string path, GameObject prefInsta)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(path))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                var uwrTexture = DownloadHandlerTexture.GetContent(uwr);
                prefInsta.GetComponent<RawImage>().texture = uwrTexture;
            }
        }
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

        TextPag.text = "" + pagAct + "/" + numPag;
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

        TextPag.text = "" + pagAct + "/" + numPag;
    }
}
