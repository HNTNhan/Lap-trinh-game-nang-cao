using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject slider;
    public GameObject optionMenu;
    public GameObject[] difficulty;
    private int scene;

    public void Awake()
    {
        PlayerPrefs.SetString("Difficulty", "easy");
    }

    public void Update()
    {
        if (optionMenu.activeSelf)
        {
            difficulty = GameObject.FindGameObjectsWithTag("Difficulty");
            if (PlayerPrefs.GetString("Difficulty") == "easy") difficulty[0].GetComponent<Button>().Select();
            else if (PlayerPrefs.GetString("Difficulty") == "normal") difficulty[1].GetComponent<Button>().Select();
            else difficulty[2].GetComponent<Button>().Select();
        }
    }

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
        LoadScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scene);
    }

    public void LoadScene()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        EnemyData data1 = SaveSystem.LoadEnemy();
        PlayerPrefs.SetString("Difficulty", data1.difficulty);
        scene = data.scene;

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
 