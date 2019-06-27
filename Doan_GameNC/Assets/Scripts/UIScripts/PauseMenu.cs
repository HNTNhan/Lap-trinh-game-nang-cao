using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public GameObject optionMenu;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenu;
    public GameObject CMvcam;
    public GameObject player;
    public GameObject slider;
    public GameObject[] allEnemy;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
                
            }
            else
            {
                Pause();
            }
        }
        if(player.GetComponent<Animator>().GetBool("Die") == true)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        gameOverMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        CMvcam.SetActive(false);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        CMvcam.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPause = false;
        CMvcam.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Load()
    {
        //SceneManager.LoadScene(1);
        player.GetComponent<Animator>().Rebind();
        PlayerPrefs.SetString("Load", "true");
        PlayerPrefs.SetString("Load1", "true");
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPause = false;
        CMvcam.SetActive(true);
        pauseMenuUI.SetActive(false);
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Volume()
    {
        if (slider.GetComponent<Slider>().value != player.GetComponentInParent<AudioSource>().volume)
        {
            player.GetComponentInParent<AudioSource>().volume = slider.GetComponent<Slider>().value;
            player.GetComponentInParent<Player>().SetVolume(slider.GetComponent<Slider>().value);
            allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            for (int j = 0; j < allEnemy.Length; j++)
            {
                allEnemy[j].GetComponent<AudioSource>().volume = slider.GetComponent<Slider>().value;
            }
        }
    }

    public void QuitGame()
    {
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPause = false;
        SceneManager.LoadScene("Menu");
    }

}
