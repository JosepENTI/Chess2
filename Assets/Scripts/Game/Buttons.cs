using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;
    public string sceneName;

    private void Start()
    {
        Screen.SetResolution(720, 1080, FullScreenMode.FullScreenWindow);
        sceneName = SceneManager.GetActiveScene().name;

        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;
        level5.interactable = false;
        level6.interactable = false;

        if (PlayerPrefs.GetInt("level_2") == 1)
        {
            level2.interactable = true;
        }

        if (PlayerPrefs.GetInt("level_3") == 1)
        {
            level3.interactable = true;
        }

        if (PlayerPrefs.GetInt("level_4") == 1)
        {
            level4.interactable = true;
        }

        if (PlayerPrefs.GetInt("level_5") == 1)
        {
            level5.interactable = true;
        }

        if (PlayerPrefs.GetInt("level_6") == 1)
        {
            level6.interactable = true;
        }

    }


    public void OnclickButton(string button) 
    {
        switch (button) 
        {
            case "return":
                SceneManager.LoadScene("Menu");
                break;
            case "retry":
                SceneManager.LoadScene(sceneName);
                break;
            case "next":
                GameManager.instance.SceneLoad(sceneName);
                break;
            case "selector":
                SceneManager.LoadScene("LvlSelector");
                break;
            case "pause":
                GameManager.instance.ActivePanelPause();
                GameManager.instance.ChangeState(Gamestate.Pause);
                break;
            case "resume":
                GameManager.instance.DesactivatePanelPause();
                GameManager.instance.ChangeState(Gamestate.PawnTurn);
                break;
            case "level1":
                SceneManager.LoadScene("Tutorial_1");
                break;
            case "settings":
                SceneManager.LoadScene("Settings");
                break;
            case "level2":
                SceneManager.LoadScene("level2");
                break;
            case "level3":
                SceneManager.LoadScene("level3");
                break;
            case "level4":
                SceneManager.LoadScene("level4");
                break;
            case "level5":
                SceneManager.LoadScene("level5");
                break;
            case "level6":
                SceneManager.LoadScene("level6");
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
