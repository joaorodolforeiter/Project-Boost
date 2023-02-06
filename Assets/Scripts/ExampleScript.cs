using UnityEngine;

// Generate a screenshot and save it to disk with the name SomeLevel.png.

public class ExampleScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("SomeLevel.png");
    }
}