using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Sfs2X.Entities.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

public class CreatPDFandImage : MonoBehaviour
{
    string path = null;

    // Screnn
    // 4k = 3840 x 2160   1080p = 1920 x 1080
    public int captureWidth = 1920;
    public int captureHeight = 1080;

    // optional game object to hide during screenshots (usually your scene canvas hud)
    public GameObject hideGameObject;

    // optimize for many screenshots will not destroy any objects so future screenshots will be fast
    public bool optimizeForManyScreenshots = true;

    // configure with raw, jpg, png, or ppm (simple raw format)
    public enum Format { RAW, JPG, PNG, PPM };
    public Format format = Format.PPM;

    // folder to write output (defaults to data path)
    public string PathFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

    public int numImagen;

    // private vars for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;
    [SerializeField] private int counter = 0; // image #

    // commands
    private bool captureScreenshot = false;
    private bool captureVideo = false;

    private string filename;

    void Start()
    {
        path = Application.dataPath + "/Ticket.pdf";

    }

    public void CaptureScreenshot()
    {
        Debug.Log("Cree Imagen");
        Directory.CreateDirectory(PathFolder + "/Planillas Guardadas");

        numImagen += 1;

        captureScreenshot = false;

        // hide optional game object if set
        if (hideGameObject != null) hideGameObject.SetActive(false);

        // create screenshot objects if needed
        if (renderTexture == null)
        {
            // creates off-screen render texture that can rendered into
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }

        // get main camera and manually render scene into rt
        Camera camera = this.GetComponent<Camera>(); // NOTE: added because there was no reference to camera in original script; must add this script to Camera
        camera.targetTexture = renderTexture;
        camera.Render();

        // read pixels will read from the currently active render texture so make our offscreen 
        // render texture active and then read the pixels
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);

        // reset active camera texture and render texture
        camera.targetTexture = null;
        RenderTexture.active = null;

        // get our unique filename
        // string filename = uniqueFilename((int)rect.width, (int)rect.height), numImagen;

        // pull in our file header/data bytes for the specified image format (has to be done from main thread)
        byte[] fileHeader = null;
        byte[] fileData = null;
        if (format == Format.RAW)
        {
            fileData = screenShot.GetRawTextureData();
        }
        else if (format == Format.PNG)
        {
            fileData = screenShot.EncodeToPNG();
        }
        else if (format == Format.JPG)
        {
            fileData = screenShot.EncodeToJPG();
        }
        else // ppm
        {
            // create a file header for ppm formatted file
            string headerStr = string.Format("P6\n{0} {1}\n255\n", rect.width, rect.height);
            fileHeader = System.Text.Encoding.ASCII.GetBytes(headerStr);
            fileData = screenShot.GetRawTextureData();
        }

        // create new thread to save the image to file (only operation that can be done in background)
        new System.Threading.Thread(() =>
        {
            while (File.Exists(PathFolder + "/Planillas Guardadas/ImagenesHechas" + "_" + numImagen + ".JPG"))
            {
                numImagen += 1;
            }

            // Crear PDF
            GenerateFile(fileData);

            // create file and write optional header with image bytes
            var f = File.Create(PathFolder + "/Planillas Guardadas/ImagenesHechas" + "_" + numImagen + ".JPG");
            if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
            f.Write(fileData, 0, fileData.Length);
            f.Close();
            //Debug.Log(string.Format(filename, fileData.Length));
        }).Start();

        // unhide optional game object if set
        if (hideGameObject != null) hideGameObject.SetActive(true);

        // cleanup if needed
        if (optimizeForManyScreenshots == false)
        {
            Destroy(renderTexture);
            renderTexture = null;
            screenShot = null;
        }

    }





    public void GenerateFile(byte[] pngArray) {
        if (File.Exists(path))
            File.Delete(path);
        using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
        {
            var document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            var writer = PdfWriter.GetInstance(document, fileStream);

            document.Open();

            document.NewPage();

            var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            /*Paragraph p = new Paragraph(string.Format("Ticket Id : {0}", 12345)); //iSFSObject.GetUtfString("TICKET_ID"
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);

            document.Add(new Paragraph("Hello World!"));

            p = new Paragraph(string.Format("Bet Number : {0}     BetAmount : {1}", 1, 100));
            p.Alignment = Element.ALIGN_CENTER;
            document.Add(p);
            */

            //Add Cover Page 1


            MemoryStream ms = new MemoryStream(pngArray);

            Document doc = new Document(PageSize.A4); // Crear un documento A4
        
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ms); // Crear una instancia de imagen
                                                                                 // Limita la imagen para que no exceda el rango A4
            if ((image.Height > PageSize.A4.Height) || (image.Width > PageSize.A4.Width))
            {
                image.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
            }
            // Alineación central
            image.Alignment = Element.ALIGN_MIDDLE;

            document.Add(image);

            document.Close();
            writer.Close();
        } 



        //StreamWriter writer = new StreamWriter(path, false);
        //writer.WriteLine(string.Format("Ticket Id : {0}",iSFSObject.GetUtfString("TICKET_ID")));
        //var betting = iSFSObject.GetSFSArray("BET_DETAILS");
        //for (int i = 0; i< betting.Count;i++)
        //    writer.WriteLine(string.Format("Bet Number : {0}     BetAmount : {1}", betting.GetSFSObject(i).GetUtfString("BET_NUM"), betting.GetSFSObject(i).GetDouble("BET_AMOUNT")));
        //writer.Close();

        PrintFiles();
    }

    void PrintFiles()
    {
        Debug.Log(path);
        if (path == null)
            return;

        if (File.Exists(path))
        {
            Debug.Log("file found");
            //var startInfo = new System.Diagnostics.ProcessStartInfo(path);
            //int i = 0;
            //foreach (string verb in startInfo.Verbs)
            //{
            //    // Display the possible verbs.
            //    Debug.Log(string.Format("  {0}. {1}", i.ToString(), verb));
            //    i++;
            //}
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
        process.StartInfo.FileName = path;
        //process.StartInfo.Verb = "print";

        process.Start();
        //process.WaitForExit();

    }
}
