using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIcontrol : MonoBehaviour
{
    private static UIcontrol instance;
    public static UIcontrol Instance => instance;
    public Transform PlayerOffSet;
    public Transform Player;
    public GameObject GameOverUI;
    public GameObject GameWinUI;
    public GameObject MainMenu;
    public GameObject MiniMap;

    private void Start()
    {
        instance = this;
        MainMenu.SetActive(true);
        MiniMap.SetActive(false);
        ThirdPersonController.Instance.playerSpeed = 0f;
    }
    private void Update()
    {
        if (PlayerBehavior.Instance.PlayerHP <= 0)
        {
            GameOver();
        }
    }
    //---------------------------------------------------------
    private void GameOver()
    {
        // 
        GameOverUI.SetActive(true);
        MiniMap.SetActive(false);
        ThirdPersonController.Instance.playerSpeed = 0f;
    }
    //---------------------------------------------------------
    public void LoadGame()
    {
        SceneManager.LoadScene("ThuongScene");
        MiniMap.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenuUI()
    {
        SceneManager.LoadScene("ThuongScene");
        MainMenu.SetActive(true);
        MiniMap.SetActive(false);
    }
    public void PlayGame()
    {
        MiniMap.SetActive(true);
        MainMenu.SetActive(false);
        ThirdPersonController.Instance.playerSpeed = 8.5f;
    }
}
