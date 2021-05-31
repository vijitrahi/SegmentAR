using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;
using System.Threading;

public class CaptureImage2 : MonoBehaviour
{

    //Set your screenshot resolutions
    public int count = 0;
    public bool count2 = false;
    public int captureWidth = 1100;
    public int captureHeight = 1100;
    // configure with raw, jpg, png, or ppm (simple raw format)
    public enum Format { JPG};
    public Format format = Format.JPG;
    // folder to write output (defaults to data path)
    private string outputFolder;
    //private bool isProcessing = true;
    // private variables needed for screenshot
    private Rect rect;
    private RenderTexture renderTexture;
    private Texture2D screenShot;

    public static Vector3[] position = new Vector3[99];
    public static Vector3[] angles = new Vector3[99];

    public  Vector3[] positions = new Vector3[99];
    public  Vector3[] angless = new Vector3[99];
    public Camera cam1;


    // Start is called before the first frame update
    void Start()
    {
        cam1 = UnityEngine.Camera.main;
        outputFolder = Application.persistentDataPath + "/Screenshots/";
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Debug.Log("Save Path will be : " + outputFolder);
        }
        //TakeScreenShot();
    }

    private string CreateFileName(int width, int height)
    {
        //timestamp to append to the screenshot filename
        string timestamp = count.ToString();
        // use width, height, and timestamp for unique file 
        var filename = string.Format("{0}/{1}.{2}", outputFolder, timestamp, format.ToString().ToLower());
        // return filename
        return filename;
    }

    private void CaptureScreenshot()
    {
        //isProcessing = true;
        // create screenshot objects
        if (renderTexture == null)
        {
            // creates off-screen render texture to be rendered into
            rect = new Rect(0, 0, captureWidth, captureHeight);
            renderTexture = new RenderTexture(captureWidth, captureHeight, 24);
            screenShot = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        }
        // get main camera and render its output into the off-screen render texture created above
        Camera camera = UnityEngine.Camera.main;
        camera.targetTexture = renderTexture;
        camera.Render();
        // mark the render texture as active and read the current pixel data into the Texture2D
        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);
        // reset the textures and remove the render texture from the Camera since were done reading the screen data
        camera.targetTexture = null;
        RenderTexture.active = null;
        // get our filename
        string filename = CreateFileName((int)rect.width, (int)rect.height);
        // get file header/data bytes for the specified image format
        byte[] fileHeader = null;
        byte[] fileData = null;
        //Set the format and encode based on it
        
        fileData = screenShot.EncodeToJPG();
        // create new thread to offload the saving from the main thread
        new System.Threading.Thread(() =>
        {
            var file = System.IO.File.Create(filename);
            if (fileHeader != null)
            {
                file.Write(fileHeader, 0, fileHeader.Length);
            }
            file.Write(fileData, 0, fileData.Length);
            file.Close();
            Debug.Log(string.Format("Screenshot Saved {0}, size {1}", filename, fileData.Length));
            //isProcessing = false;
        }).Start();
        //Cleanup
        Destroy(renderTexture);
        renderTexture = null;
        screenShot = null;
    }

    public void TakeScreenShot()
    {
        //if (isProcessing)
        //   {
        count++;

        position[count] = cam1.transform.position;
        angles[count] = cam1.transform.eulerAngles;

        positions[count] = position[count];
        angless[count] = angles[count];
        Ray ray1 = cam1.ViewportPointToRay(new Vector3(0.00f, 0.00f, 0));
        Debug.DrawRay(ray1.origin, ray1.direction * 10.0f, Color.green, 500.0f, false);
        Ray ray2 = cam1.ViewportPointToRay(new Vector3(1.0f, 1.0f, 0));
        Debug.DrawRay(ray2.origin, ray2.direction * 10.0f, Color.green, 500.0f, false);
        Ray ray3 = cam1.ViewportPointToRay(new Vector3(1.0f, 0.00f, 0));
        Debug.DrawRay(ray3.origin, ray3.direction * 10.0f, Color.green, 500.0f, false);
        Ray ray4 = cam1.ViewportPointToRay(new Vector3(0.00f, 1.0f, 0));
        Debug.DrawRay(ray4.origin, ray4.direction * 10.0f, Color.green, 500.0f, false);
        CaptureScreenshot();
      //  }
       // else
       // {
       //     Debug.Log("Currently Processing");
       // }
    }

    // Update is called once per frame
    void Update()
    { 
        //GameObject.Find("Camera2").transform.position = new Vector3(1.0F, 2.0F, 3.0F);
        //GameObject.Find("Camera2").transform.rotation = Quaternion.identity;
        if(count2 == true)
        {
            TakeScreenShot();
            count2 = false;
        }
    }
}