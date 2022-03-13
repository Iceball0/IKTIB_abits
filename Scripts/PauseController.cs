using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject Pause_menu;
    public GameObject Control_Canvas;

    public void makePause()
    {
        Time.timeScale = 0;
        Pause_menu.SetActive(true);
        Control_Canvas.SetActive(false);
    }

    public void makePlay()
    {
        Time.timeScale = 1f;
        Pause_menu.SetActive(false);
        Control_Canvas.SetActive(true);
    }

    public void goHome()
    {
        SceneManager.LoadScene(0);
    }
}
