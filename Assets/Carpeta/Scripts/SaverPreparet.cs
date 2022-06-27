//using AnotherFileBrowser.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;
using UnityEditor;
using SimpleFileBrowser;



public class SaverPreparet : MonoBehaviour
{
    public ControlPreparePdf controlSAVE;

    // Numero de Partida
    public string partidaI = "";
    public int partidaNum;

    //AutoCargar al empezar
    public bool CargarAlIniciar = true;
    [SerializeField] private bool AdminTeclas;

    [SerializeField] private string fileNameInput;

    // Variables
    //Hoja
    public List<string> colorLeft = new List<string>();
    public List<int> sizeLeft = new List<int>();
    //
    public List<int> prefabTipe = new List<int>();
    //Imagenes
    public List<string> pathImgs = new List<string>();

    //TextoTitulo
    public List<string> textTitles = new List<string>();
    public List<int> textTitlesSize = new List<int>();
    public List<string> textTitlesColorHx = new List<string>();
    public List<int> textTitleslineUp = new List<int>();
    public List<int> textTitleslineBottom = new List<int>();
    public List<int> textTitlesNumFont = new List<int>();
    public List<bool> textTitlesStyleB = new List<bool>();
    public List<bool> textTitlesStyleI = new List<bool>();
    public List<bool> textTitlesStyleU = new List<bool>();
    public List<bool> textTitlesStyleS = new List<bool>();

    //TextoDescrip
    public List<string> textDescrips = new List<string>();
    public List<int> textDescripsSize = new List<int>();
    public List<string> textDescripsColorHx = new List<string>();
    public List<int> textDescripslineUp = new List<int>();
    public List<int> textDescripslineBottom = new List<int>();
    public List<int> textDescripsNumFont = new List<int>();
    public List<bool> textDescripsStyleB = new List<bool>();
    public List<bool> textDescripsStyleI = new List<bool>();
    public List<bool> textDescripsStyleU = new List<bool>();
    public List<bool> textDescripsStyleS = new List<bool>();

    // Path
    public string path;

    // Numero de Partida
    public string pathExtra;

    //Exportar
    public GameObject textoGuardado;

    void Awake()
    {
        //sprint(Application.persistentDataPath); // Indica donde se van a guardar los datos
        if (CargarAlIniciar == true)
        {
            Cargar(); // Cargar Partida
        }

    }

    // G para guardar / C para cargar / B para borrar partida
    void Update()
    {

        if (fileNameInput != "")
        {
            partidaI = fileNameInput;
        }
        else if (fileNameInput == "")
        {
            while (Directory.Exists(PlayerPrefs.GetString("pathSave") + "/" + partidaI + ".cPDF"))
            {
                partidaNum += 1;
                partidaI = partidaNum.ToString();
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && AdminTeclas == true)
        {
            Guardar(); // Guardar Partida
        }

        if (Input.GetKeyDown(KeyCode.C) && AdminTeclas == true)
        {
            Cargar(); // Cargar Partida
        }

    }

        public void stringInputNameFile (string input)
        {
            fileNameInput = input;
        }

    // Sistema de guardado
    public void Guardar()
    {
        Debug.Log("" + partidaI);

        // Texture a texture2D
        /* for (int i = 0; i < rawImages.Count; i++)
         {
             if (rawImages[i] != null)
             {
                 texture2Ds.Add(rawImages[i].ToTexture2D());
             }
         }
        */

        /* string path;
         path = EditorUtility.OpenFolderPanel("Select Directory", PlayerPrefs.GetString("pathSave"), "");
         pathExtra = path;
         PlayerPrefs.SetString("pathSave", pathExtra);

     if (pathExtra == "")
         return;
        */

        FileBrowser.SetFilters(false, new FileBrowser.Filter("folders", "folder"));

        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowFolderSave());

    } 

    private void guardado()
    {
        textoGuardado.SetActive(false);
    }

    // Sistema de cargar
    public void Cargar()
    {
        /*string path;
        path = EditorUtility.OpenFilePanel("Load File .cPDF", PlayerPrefs.GetString("pathSave"), "cPDF");
        if (!File.Exists(path)) return;
        pathExtra = path;
        PlayerPrefs.SetString("pathSave", pathExtra);*/
        
        FileBrowser.SetFilters(false, new FileBrowser.Filter(".cpdf", ".cPDF"));
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        StartCoroutine(ShowFileLoad());
    }
    IEnumerator ShowFileLoad()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
       
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            /* // Read the bytes of the first file via FileBrowserHelpers
             // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
             byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

             // Or, copy the first file to persistentDataPath
             string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
             FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
             path = destinationPath;*/

            path = FileBrowser.Result[0];

           //if (File.Exists(path)) // Si hay un archivo de guardado "/Datos.cPDF"
           // {
                BinaryFormatter fb = new BinaryFormatter(); //Ayudante
                FileStream Expediente = File.OpenRead(path); // lee el archivo
                DatosPlayer Datos = new DatosPlayer(); //Datos sera la clase que descomprimiremos

                Datos = fb.Deserialize(Expediente) as DatosPlayer; // Nuestros datos se convertiran en los datos del archivo

                // Reinicia
                colorLeft.Clear();
                sizeLeft.Clear();
                prefabTipe.Clear();
                pathImgs.Clear();

                //TextoTitulo
                textTitles.Clear();
                textTitlesSize.Clear();
                textTitlesColorHx.Clear();
                textTitleslineUp.Clear();
                textTitleslineBottom.Clear();
                textTitlesNumFont.Clear();
                textTitlesStyleB.Clear();
                textTitlesStyleI.Clear();
                textTitlesStyleU.Clear();
                textTitlesStyleS.Clear();

                //TextoDescrip
                textDescrips.Clear();
                textDescripsSize.Clear();
                textDescripsColorHx.Clear();
                textDescripslineUp.Clear();
                textDescripslineBottom.Clear();
                textDescripsNumFont.Clear();
                textDescripsStyleB.Clear();
                textDescripsStyleI.Clear();
                textDescripsStyleU.Clear();
                textDescripsStyleS.Clear();

                //Cargamos los datos en el player//Hoja
                colorLeft = Datos.colorLeft;
                sizeLeft = Datos.sizeLeft;
                prefabTipe = Datos.prefabTipe;
                //Imagenes
                pathImgs = Datos.pathImgs;

                //TextoTitulo
                textTitles = Datos.textTitles;
                textTitlesSize = Datos.textTitlesSize;
                textTitlesColorHx = Datos.textTitlesColorHx;
                textTitleslineUp = Datos.textTitleslineUp;
                textTitleslineBottom = Datos.textTitleslineBottom;
                textTitlesNumFont = Datos.textTitlesNumFont;
                textTitlesStyleB = Datos.textTitlesStyleB;
                textTitlesStyleI = Datos.textTitlesStyleI;
                textTitlesStyleU = Datos.textTitlesStyleU;
                textTitlesStyleS = Datos.textTitlesStyleS;

                //TextoDescrip
                textDescrips = Datos.textDescrips;
                textDescripsSize = Datos.textDescripsSize;
                textDescripsColorHx = Datos.textDescripsColorHx;
                textDescripslineUp = Datos.textDescripslineUp;
                textDescripslineBottom = Datos.textDescripslineBottom;
                textDescripsNumFont = Datos.textDescripsNumFont;
                textDescripsStyleB = Datos.textDescripsStyleB;
                textDescripsStyleI = Datos.textDescripsStyleI;
                textDescripsStyleU = Datos.textDescripsStyleU;
                textDescripsStyleS = Datos.textDescripsStyleS;

                Debug.Log("Cargue la partida"); // Confirmacio
                controlSAVE.LoadFullSave();
        }
    }

    IEnumerator ShowFolderSave()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            /* // Read the bytes of the first file via FileBrowserHelpers
             // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
             byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

             // Or, copy the first file to persistentDataPath
             string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
             FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
             path = destinationPath;*/

            path = FileBrowser.Result[0];

            Debug.Log(path);

            if (fileNameInput == "")
            {
                for (int i = 0; i < 100; i++)
                {
                    if (File.Exists(path + "/" + partidaI + ".cpdf"))
                    {
                        partidaNum += 1;
                        partidaI = partidaNum.ToString();
                    }
                }
            }
            else
            {
                partidaI = fileNameInput;
            }

            // Preparar el serializado

            // Crear Archivo
            BinaryFormatter fb = new BinaryFormatter(); // Ayudante
            FileStream Expediente = File.Create(path + "/" + partidaI + ".cpdf"); // Crea el archivos de datos "Datos.cPDF"
            DatosPlayer Datos = new DatosPlayer(); // "Datos" sera la clase que serializaremos

            // Estos son los datos que entaran dentro de "Datos" / la clase que serializamos 
            //byte[] bytes = texture2Ds.GetRawTextureData();
            //Datos.bytes[0] = texture2Ds[0].GetRawTextureData();

            //Hoja
            Datos.colorLeft = colorLeft;
            Datos.sizeLeft = sizeLeft;
            Datos.prefabTipe = prefabTipe;
            //Imagenes
            Datos.pathImgs = pathImgs;

            //TextoTitulo
            Datos.textTitles = textTitles;
            Datos.textTitlesSize = textTitlesSize;
            Datos.textTitlesColorHx = textTitlesColorHx;
            Datos.textTitleslineUp = textTitleslineUp;
            Datos.textTitleslineBottom = textTitleslineBottom;
            Datos.textTitlesNumFont = textTitlesNumFont;
            Datos.textTitlesStyleB = textTitlesStyleB;
            Datos.textTitlesStyleI = textTitlesStyleI;
            Datos.textTitlesStyleU = textTitlesStyleU;
            Datos.textTitlesStyleS = textTitlesStyleS;

            //TextoDescrip
            Datos.textDescrips = textDescrips;
            Datos.textDescripsSize = textDescripsSize;
            Datos.textDescripsColorHx = textDescripsColorHx;
            Datos.textDescripslineUp = textDescripslineUp;
            Datos.textDescripslineBottom = textDescripslineBottom;
            Datos.textDescripsNumFont = textDescripsNumFont;
            Datos.textDescripsStyleB = textDescripsStyleB;
            Datos.textDescripsStyleI = textDescripsStyleI;
            Datos.textDescripsStyleU = textDescripsStyleU;
            Datos.textDescripsStyleS = textDescripsStyleS;

            // El ayudante serializara los archivos
            fb.Serialize(Expediente, Datos);
            Expediente.Close();
            Debug.Log("Los Datos se Guardaron"); //Avisa que los datos han sido guardados

            textoGuardado.GetComponent<TextMeshProUGUI>().text = "Save File: " + "' " + partidaI + " '";
            textoGuardado.SetActive(true);
            Invoke("guardado", 3);
            /* // Reinicia
             colorLeft.Clear();
             colorLeft.Add(null);
             pathImgs.Clear();
             textTitles.Clear();
             textDescrips.Clear();*/
        }


    }

}

[Serializable()] // Nuestras variables estan listas para ser serializadas
class DatosPlayer : System.Object
{
    // Dia

    // Variables
    //Hoja
    public List<string> colorLeft = new List<string>();
    public List<int> sizeLeft = new List<int>();
    //
    public List<int> prefabTipe = new List<int>();
    //Imagenes
    public List<string> pathImgs = new List<string>();
    //TextoTitulo
    public List<string> textTitles = new List<string>();
    public List<int> textTitlesSize = new List<int>();
    public List<string> textTitlesColorHx = new List<string>();
    public List<int> textTitleslineUp = new List<int>();
    public List<int> textTitleslineBottom = new List<int>();
    public List<int> textTitlesNumFont = new List<int>();
    public List<bool> textTitlesStyleB = new List<bool>();
    public List<bool> textTitlesStyleI = new List<bool>();
    public List<bool> textTitlesStyleU = new List<bool>();
    public List<bool> textTitlesStyleS = new List<bool>();

    //TextoDescrip
    public List<string> textDescrips = new List<string>();
    public List<int> textDescripsSize = new List<int>();
    public List<string> textDescripsColorHx = new List<string>();
    public List<int> textDescripslineUp = new List<int>();
    public List<int> textDescripslineBottom = new List<int>();
    public List<int> textDescripsNumFont = new List<int>();
    public List<bool> textDescripsStyleB = new List<bool>();
    public List<bool> textDescripsStyleI = new List<bool>();
    public List<bool> textDescripsStyleU = new List<bool>();
    public List<bool> textDescripsStyleS = new List<bool>();
}

/*
byte[] bytes = test.GetRawTextureData();
test2 = new Texture2D(test.width, test.height);
test2.LoadRawTextureData(bytes);
test2.Apply();
*/




// Texture a Texture 2D
/*
public static class TextureExtentions
{
    public static Texture2D ToTexture2D(this Texture texture)
    {
        return Texture2D.CreateExternalTexture(
            texture.width,
            texture.height,
            TextureFormat.RGB24,
            false, false,
            texture.GetNativeTexturePtr());
    }
}
*/