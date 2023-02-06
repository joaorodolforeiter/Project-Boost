using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            ExitApplication();
        }
    }

    private static void ExitApplication()
    {
        Application.Quit();
    }
}