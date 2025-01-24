using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public static void Stop()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public static void Quit()
    {
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
