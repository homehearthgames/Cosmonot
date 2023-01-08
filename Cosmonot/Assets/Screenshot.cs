using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    // The folder where the screenshots will be saved
    private string screenshotFolder = "Screenshots";

    // Update is called once per frame
    void Update()
    {
        // Check if the F2 key is pressed
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Get the path to the screenshot folder
            string folderPath = Path.Combine(Application.dataPath, screenshotFolder);

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Generate a unique file name for the screenshot
            string fileName = string.Format("Screenshot_{0:yyyy-MM-dd_hh-mm-ss-tt}.png", System.DateTime.Now);

            // Get the full file path for the screenshot
            string filePath = Path.Combine(folderPath, fileName);

            // Take the screenshot and save it to the file
            ScreenCapture.CaptureScreenshot(filePath);
        }
    }
}
