using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    

    private void Start()
    {
        Screen.SetResolution(1080, 1920, FullScreenMode.FullScreenWindow);
    }


    public void OnclickButton(string button) 
    {
        switch (button) 
        {
            case "return":
                SceneManager.LoadScene("Menu");
                break;
            case "play":
                SceneManager.LoadScene("Tutorial_1");
                break;
            case "exit":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
               Application.Quit();
#endif

                break;

        }
    
    
    }

   

   
}
