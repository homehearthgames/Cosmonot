using System.IO;
using UnityEngine;
using TMPro;
using System.Collections;

public class Screenshot : MonoBehaviour
{
    // The folder where the screenshots will be saved
    private string screenshotFolder = "Screenshots";

    // The original color of the text
    private Color originalColor;

    // Reference to the text for when a screenshot is taken
    [SerializeField] TextMeshProUGUI screenshotTakenText;

    // The duration to show the text, in seconds
    [SerializeField] float duration;

    private void Start() 
    {
        // Store the original color of the text
        originalColor = screenshotTakenText.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the F2 key is pressed
        if (Input.GetKeyDown(KeyCode.F2))
        {
            // Start a coroutine to fade the text out over the specified duration
            StartCoroutine(TakeScreenshot(duration));
        }
    }

    // A coroutine that fades the TextMeshProUGUI component out over the specified duration
    IEnumerator TakeScreenshot(float duration)
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
        Debug.Log("Screenshot Taken!");

        yield return null;

        // Enable the TextMeshProUGUI component
        screenshotTakenText.gameObject.SetActive(true);

        // Reset the text color to its original color
        screenshotTakenText.color = originalColor;

        // Get the current color of the text
        Color color = screenshotTakenText.color;

        // Calculate the alpha change per frame
        float alphaChangePerFrame = color.a / (duration * Time.deltaTime);

        // Fade the text out over the specified duration
        while (color.a > 0)
        {
            color.a -= alphaChangePerFrame;
            screenshotTakenText.color = color;
            yield return null;
        }

        // Disable the TextMeshProUGUI component
        screenshotTakenText.gameObject.SetActive(false);
    }

}
