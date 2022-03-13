using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject infoCanvas;
    public GameObject closeCanvas;
    public GameObject shopCanvas;
    
    public void Info()
    {
        infoCanvas.SetActive(true);
        closeCanvas.SetActive(true);
    }

    public void Shop()
    {
        shopCanvas.SetActive(true);
        closeCanvas.SetActive(true);
    }

    public void Close()
    {
        infoCanvas.SetActive(false);
        closeCanvas.SetActive(false);
        shopCanvas.SetActive(false);
    }
    
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
