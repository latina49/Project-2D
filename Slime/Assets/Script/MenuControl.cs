using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public Image HuongDan;
    public void HuongDanOnOff()
    {
        if (!HuongDan.gameObject.activeSelf)
            HuongDan.gameObject.SetActive(true);
        else
            HuongDan.gameObject.SetActive(false);
    }
    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }
    public void OnHomeClick()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1; 
    }

    public void ClickQuit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
