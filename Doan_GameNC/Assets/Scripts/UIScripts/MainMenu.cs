using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject slider;

    public void NewGame()
    {
        PlayerPrefs.SetFloat("Volume", slider.GetComponent<Slider>().value);
        PlayerPrefs.SetString("Load", "false");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetFloat("Volume", slider.GetComponent<Slider>().value);
        PlayerPrefs.SetString("Load", "true");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Difficulty(string level)
    {
        PlayerPrefs.SetString("Difficulty", level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
 